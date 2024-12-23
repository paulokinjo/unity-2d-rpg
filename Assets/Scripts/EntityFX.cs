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

    private void RedColorBlink()
    {
        if (_spriteRenderer.color != Color.white)
        {
            _spriteRenderer.color = Color.white;
        }
        else
        {
            _spriteRenderer.color = Color.red;
        }
    }

    private void CancelRedBlink()
    {
        CancelInvoke();
        _spriteRenderer.color = Color.white;
    }
}
