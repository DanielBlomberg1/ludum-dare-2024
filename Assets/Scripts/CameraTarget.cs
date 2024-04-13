using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTarget : MonoBehaviour
{
    [Serializable] private class Bounds
    {
        public float left = -10f;
        public float right = 10f;
        public float up = 10f;
        public float down = -10f;
    }
    
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private Bounds _bounds;

    private bool _isDragging;

    private Vector3 _origin;
    
    private float boundsDamping = 5f;


    public void OnDrag(InputAction.CallbackContext context)
    {
        if (context.started) _origin = GetMousePosition;
        
        _isDragging = context.started || context.performed;
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

    private void CheckBounds()
    {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawLine(new Vector3(_bounds.left, _bounds.down), new Vector3(_bounds.left, _bounds.up)); // LEFT LINE
        Gizmos.DrawLine(new Vector3(_bounds.right, _bounds.down), new Vector3(_bounds.right, _bounds.up)); // RIGHT LINE
        Gizmos.DrawLine(new Vector3(_bounds.left, _bounds.up), new Vector3(_bounds.right, _bounds.up)); // UP LINE
        Gizmos.DrawLine(new Vector3(_bounds.left, _bounds.down), new Vector3(_bounds.right, _bounds.down)); // DOWN LINE
    }
}
