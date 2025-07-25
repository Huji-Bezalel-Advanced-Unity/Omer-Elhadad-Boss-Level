using System;
using UnityEngine;

public class PoolableLargeProjectile : MonoBehaviour, IPoolable
{
    #region Animator Hashes
    private static readonly int Hit = Animator.StringToHash("Hit");
    #endregion

    #region Projectile Settings
    [SerializeField] private float projectileSpeed;
    [SerializeField] private AudioSource projectileAudioSource;
    [SerializeField] private AudioClip projectileHitClip;
    #endregion
    public float ProjectileDamage { get; private set; } = 2f;
    
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    public Vector2 Direction { get; set; } = Vector2.right;

    private void OnEnable()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            bool facingRight = player.transform.localScale.x >= 0;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (facingRight ? 1 : -1);
            transform.localScale = scale;
            Direction = facingRight ? Vector2.right : Vector2.left;
        }

        if (_rigidbody2D)
        {
            _rigidbody2D.linearVelocity = Direction.normalized * projectileSpeed;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy Hitbox")) return;
        _rigidbody2D.linearVelocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0f;
        _animator.SetTrigger(Hit);

        // Play hit sound if assigned
        if (projectileAudioSource != null && projectileHitClip != null)
        {
            projectileAudioSource.clip = projectileHitClip;
            projectileAudioSource.Play();
        }
    }

    public void OnDeathEvent()
    {
        LargeProjectilePool.Instance.Return(this);
    }

    public void Reset()
    {
        if (_rigidbody2D)
        {
            _rigidbody2D.linearVelocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }

        if (_animator)
        {
            _animator.ResetTrigger(Hit);
        }
        
        if (projectileAudioSource)
        {
            projectileAudioSource.clip = null;
        }
        
        gameObject.SetActive(false);
    }
}