using System;
using System.Collections;
using UnityEngine;

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
    
    private LavaState _lavaState;

    private SpriteRenderer _lavaRenderer;

    private Collider2D _lavaCollider;

    private float _obsidianification;
    private float _scrollState;

    private void Start()
    {
        _lavaCollider = GetComponent<Collider2D>();
        _lavaRenderer = GetComponent<SpriteRenderer>();
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
        
        _particleSystem.Play();
        
        // PLAY SOUND

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
                Destroy(coll.gameObject);
            }
        }
    }
}
