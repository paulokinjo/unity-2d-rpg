using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] private Transform _attackCheck;
    [SerializeField] private float _attackCheckRadius;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private Transform _wallCheck;
    [SerializeField] private float _wallCheckDistance;
    [SerializeField] private LayerMask _whatIsGround;

    [Header("Knockback info")]
    [SerializeField] private Vector2 _knockbackDirection;
    [SerializeField] private float _knockbackDuration;

    private bool _facingRight = true;

    public bool IsKnocked { get; protected set; }

    public Rigidbody2D Rigidbody2D { get; private set; }

    public Animator Animator { get; private set; }

    public EntityFX EntityFX { get; private set; }

    public int FacingDirection { get; private set; } = 1;

    public Transform AttackCheck => _attackCheck;

    public float AttackCheckRadius => _attackCheckRadius;

    public bool IsGroundDetected => Physics2D.Raycast(_groundCheck.position, Vector2.down, _groundCheckDistance, _whatIsGround);

    public bool IsWallDetected => Physics2D.Raycast(_wallCheck.position, Vector2.right * FacingDirection, _wallCheckDistance, _whatIsGround);

    public void SetVelocity(float x, float y)
    {
        Rigidbody2D.SetVelocity(x, y);
        FlipController(x);
    }

    public Transform WallCheck => _wallCheck;

    public virtual void Flip()
    {
        FacingDirection *= -1;
        _facingRight = !_facingRight;

        transform.Rotate(0, 180, 0);
    }

    public virtual void Damage()
    {
        Debug.Log($"{gameObject.name} was damaged!");
        EntityFX.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }

    public void FlipController(float x)
    {
        if (x > 0 && !_facingRight)
        {
            Flip();
        }
        else if (x < 0 && _facingRight)
        {
            Flip();
        }
    }

    protected abstract void Awake();

    protected virtual void Start()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        EntityFX = GetComponent<EntityFX>();
    }

    protected abstract void Update();

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(_groundCheck.position, new Vector3(_groundCheck.position.x, _groundCheck.position.y - _groundCheckDistance));
        Gizmos.DrawLine(_wallCheck.position, new Vector3(_wallCheck.position.x + _wallCheckDistance, _wallCheck.position.y));

        Gizmos.DrawWireSphere(_attackCheck.position, _attackCheckRadius);
    }

    protected virtual IEnumerator HitKnockback()
    {
        IsKnocked = true;

        Rigidbody2D.velocity = new Vector2(_knockbackDirection.x * -FacingDirection, _knockbackDirection.y);

        yield return new WaitForSeconds(_knockbackDuration);

        IsKnocked = false;
    }
}
