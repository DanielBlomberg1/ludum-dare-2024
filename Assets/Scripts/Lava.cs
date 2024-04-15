using System;
using System.Collections;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Lava : MonoBehaviour
{
    public enum LavaState
    {
        Lava,
        Obsidian
    }

    [SerializeField] private ParticleSystem _particleSystem;
    
    [SerializeField] private float _transformationTime = 0.5f;
    [SerializeField] private float _scrollSpeed;

    private StudioEventEmitter _obsidianSound;
    
    private LavaState _lavaState;

    private SpriteRenderer _lavaRenderer;

    private BoxCollider2D _lavaCollider;

    private Light2D _light;

    private float _obsidianification;
    private float _scrollState;

    private void Start()
    {
        _lavaCollider = GetComponent<BoxCollider2D>();
        _lavaRenderer = GetComponent<SpriteRenderer>();
        _obsidianSound = GetComponent<StudioEventEmitter>();

        _light = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        _scrollState += Time.deltaTime * (1 - _obsidianification) * _scrollSpeed;
        
        _lavaRenderer.material.SetFloat("_ScrollState", _scrollState);
    }

    public void ChangeToObsidian()
    {
        _lavaCollider.isTrigger = false;
        
        _lavaState = LavaState.Obsidian;

        _lavaCollider.size = Vector2.one;
        
        _particleSystem.Play();

        Destroy(_light);
        
        _obsidianSound.Play();

        StartCoroutine(ObsidianTransformation());
    }

    private IEnumerator ObsidianTransformation()
    {
        while (_obsidianification < 1f)
        {
            _obsidianification += Time.deltaTime / _transformationTime;
            
            _lavaRenderer.material.SetFloat("_Obsidianification", _obsidianification);
        
            yield return new WaitForEndOfFrame();
        }
        
        _particleSystem.Stop();
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Cat"))
        {
            if (_lavaState == LavaState.Lava)
            {
                coll.GetComponent<CatScript>().Murder();
            }
        }
        if(coll.CompareTag("Water"))
        {
            ChangeToObsidian();
        }
    }
}
