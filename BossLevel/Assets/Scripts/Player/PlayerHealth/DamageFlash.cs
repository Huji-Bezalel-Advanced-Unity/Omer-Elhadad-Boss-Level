using System;
using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    private static readonly int FlashColor = Shader.PropertyToID("_FlashColor");
    private static readonly int FlashAmount = Shader.PropertyToID("_FlashAmount");
    [SerializeField] private Color flashColor = Color.white; // Color to flash when damaged
    [SerializeField] private float flashDuration = 0.1f; // Duration of the flash effect
    private SpriteRenderer _spriteRenderer;
    private Color _originalColor;
    private Material _originalMaterial;
    private Coroutine _damageFlashCoroutine;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
            _originalMaterial = _spriteRenderer.material;
        }
    }

    private void OnEnable()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _originalColor; // Reset color when enabled
        }
        
        if (_originalMaterial != null)
        {
            _originalMaterial.SetFloat(FlashAmount, 0f); // Reset flash amount
        }
    }

    public void CallDamageFlash()
    {
        _damageFlashCoroutine = StartCoroutine(DamageFlasher());
    }

    private IEnumerator DamageFlasher()
    {
        _originalMaterial.SetColor(FlashColor, flashColor);
        float currentFlashAmount = 0f;
        float elapsedTime = 0f;
        
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            
            // Interpolate the color to create a flash effect
            currentFlashAmount = Mathf.Lerp(1f, 0f, elapsedTime / flashDuration);
            _originalMaterial.SetFloat(FlashAmount, currentFlashAmount);
            
            yield return null;
        }
    }
}
