using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraTarget : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    
    private CameraBounds _bounds;

    private bool _isDragging;

    private Vector3 _origin;
    
    private float boundsDamping = 5f;

    private void Start()
    {
        SceneManager.sceneLoaded += (arg0, mode) =>
        {
            if (CameraBounds.Instance)
            {
                _bounds = CameraBounds.Instance;
            }

            else
            {
                Debug.Log("CameraBounds object missing from scene");
            }
        };
    }

    private void Update()
    {
        CheckBounds();

        if (_isDragging)
        {
            Vector3 difference = GetMousePosition - transform.position;
            transform.position = _origin - difference;
        }
    }
    
    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started) _origin = GetMousePosition;
        
        _isDragging = context.started || context.performed;
    }

    private void CheckBounds()
    {
        if (!_bounds) return;
        
        float cameraSize = _mainCamera.orthographicSize;
        
        float leftDiff = _bounds.left + cameraSize - transform.position.x;
        float rightDiff = transform.position.x - (_bounds.right - cameraSize);
        float downDiff = _bounds.down + cameraSize - transform.position.y;
        float upDiff = transform.position.y - (_bounds.up - cameraSize);
        
        if (leftDiff > 0) transform.Translate(Vector3.right * (leftDiff * boundsDamping * Time.deltaTime));
        if (rightDiff > 0) transform.Translate(Vector3.left * (rightDiff * boundsDamping * Time.deltaTime));
        if (downDiff > 0) transform.Translate(Vector3.up * (downDiff * boundsDamping * Time.deltaTime));
        if (upDiff > 0) transform.Translate(Vector3.down * (upDiff * boundsDamping * Time.deltaTime));
    }

    private Vector3 GetMousePosition => _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    
}
