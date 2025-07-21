using UnityEngine;

public class PoolableZombie : MonoBehaviour, IPoolable
{
    
    [Header("Poolable Zombie Settings")]
    [SerializeField] private GameObject healthManager;
    
    private Vector3 _originalPosition;
    private Material _originalMaterial;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _originalPosition = transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer != null)
        {
            _originalMaterial = _spriteRenderer.material;
        }
    }
    
    private void OnEnable()
    {
        healthManager.SetActive(true);
    }
 
    public void OnDeathEvent()
    {
        ZombiePool.Instance.Return(this);
    }

    public void Reset()
    {
        // Reset local scale and position to original
        transform.localScale = Vector3.one;
        transform.position = _originalPosition;
        // Reset health manager if it has a method to reset
        healthManager.SetActive(true);

        // Reset material to original
        if (_spriteRenderer != null && _originalMaterial != null)
        {
            _spriteRenderer.material = _originalMaterial;
        }
    }
}