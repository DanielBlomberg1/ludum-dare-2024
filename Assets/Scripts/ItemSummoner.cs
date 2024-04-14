using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSummoner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _summonPrefabs = new List<GameObject>();

    private GameObject _selectedSummon = null;
    private GameObject _previewObject = null;

    private void Update()
    {
        HandleInput();

        if (_previewObject != null)
        {
            var mousePosition = GetMouseWordPosition();

            _previewObject.transform.position =  mousePosition;
        }
    }

    private void HandleInput()
    {
        if (GameManager.Instance.CurrentState != GameState.Play)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeSelectedSummon(_summonPrefabs[0]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeSelectedSummon(_summonPrefabs[1]);
        }
        else if (Input.GetKey(KeyCode.Alpha3))
        {
            ChangeSelectedSummon(_summonPrefabs[2]);
        }
        else if (Input.GetKey(KeyCode.Alpha4))
        {
            ChangeSelectedSummon(_summonPrefabs[3]);
        }
        else if (Input.GetKey(KeyCode.Alpha5))
        {
            ChangeSelectedSummon(_summonPrefabs[4]);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            PlaceSummon();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            ChangeSelectedSummon(null);
        }
    }

    private void ChangeSelectedSummon(GameObject selectedSummon)
    {
        _selectedSummon = selectedSummon;

        ClearPreview();

        if (_selectedSummon != null)
        {
            CreatePreview(selectedSummon);
        }
    }

    private void CreatePreview(GameObject gameObject)
    {
        var previewObject = new GameObject();
        var spriteComponent = previewObject.AddComponent<SpriteRenderer>();
        spriteComponent.sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        spriteComponent.color = new Color(Color.green.r, Color.green.g, Color.green.b, 0.4f);

        _previewObject = previewObject;
    }

    private void ClearPreview()
    {
        if (_previewObject != null)
        {
            Destroy(_previewObject);
            _previewObject = null;
        }
    }

    private void PlaceSummon()
    {
        if (_selectedSummon == null)
        {
            return;
        }

        GameObject.Instantiate(_selectedSummon, GetMouseWordPosition(), Quaternion.identity);

        ChangeSelectedSummon(null);
    }

    private Vector3 GetMouseWordPosition()
    {
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        mouseWorldPosition.z = 0f;

        return mouseWorldPosition;
    }
}
