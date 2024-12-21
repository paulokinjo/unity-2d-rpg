using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    [Header("Flash FX")]
    [SerializeField] private Material _hitMat;
    [SerializeField] private float _flashDuration = .2f;

    private SpriteRenderer _spriteRenderer;
    private Material _originalMat;

    private void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _originalMat = _spriteRenderer.material;
    }

    private IEnumerator FlashFX()
    {
        _spriteRenderer.material = _hitMat;

        yield return new WaitForSeconds(_flashDuration);
    
        _spriteRenderer.material = _originalMat;
    }
}
