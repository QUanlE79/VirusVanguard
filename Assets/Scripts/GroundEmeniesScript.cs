using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class GroundEmeniesScript : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float walkStopRate = 0.05f;

    public DetectionZone attackZone;
    public DetectionZone cliffDetectionZone;

    Rigidbody2D rb;
    TouchingDirection touchingDirection;
    Animator animator;
    Damageable damageable;
    public enum WalkableDirection { Right, Left }
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public int minCoin = 1;
    public int maxCoin = 10;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                } else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    private bool _hasTarget = false;

    public bool HasTarget {
        get { return _hasTarget; }
        private set {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }
    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationString.canMove);
        }
        private set
        {
            animator.SetBool(AnimationString.canMove, value);
        }
    }

    public float atkCd {
        get
        {
            return animator.GetFloat(AnimationString.atkCd);
        }
        private set {
            animator.SetFloat(AnimationString.atkCd, Mathf.Max(value,0));
        } 
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirection = GetComponent<TouchingDirection>();
        animator = GetComponent<Animator>();
        damageable= GetComponent<Damageable>();
    }
    // Update is called once per frame
    void Update()
    {
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (HasTarget) CanMove = false; else CanMove = true;
        if (atkCd > 0)
        {
            atkCd -= Time.deltaTime;
        }
        
    }
    private void FixedUpdate()
    {
        if(touchingDirection.IsOnWall && touchingDirection.IsGrounded || cliffDetectionZone.detectedColliders.Count == 0)
        {
            FlipDirection();
        }
        if(!damageable.LockVelocity)
        {
            if (CanMove)
            {
                rb.velocity = new Vector2(walkSpeed * walkDirectionVector.x, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }
        if (!isAlive)
        {
            
            InvokeRepeating("Death", 0.7f, 0);
        }
        
        
    }
    private void FlipDirection()
    {
        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }else
        {
            Debug.LogError("Error Direction");
        }
    }
    [SerializeField] private SimpleFlash flashEffect;
    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        flashEffect.Flash();
        
    }
    void Death()
    {
        damageable.DropCoin(minCoin, maxCoin);
        Destroy(gameObject);
    }

}
