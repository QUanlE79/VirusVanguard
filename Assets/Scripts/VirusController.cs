using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]
public class VirusController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 5f;
    public float jumpImpulse = 8f;

    Animator animator;
    Damageable damageable;
    Vector2 moveInput;
    TouchingDirection touchingDirection;

    public float CurSpeed { 
        get {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    return 5;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
          
            
        } 
        private set { 
            
        } 
    }
    [SerializeField]
    private bool _isMoving=false;
    public bool IsMoving { get {
            return _isMoving;
        } 
        private set { 
            _isMoving= value;
            animator.SetBool(AnimationString.isMoving, value);
        } 
    }
    [SerializeField]
    private bool _isFacingRight=true;
    public bool IsFacingRight { get { return _isFacingRight; }
        private set { 
            if(_isFacingRight !=value) {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight= value;
        }
    }
    public bool CanMove { get
        {
            return animator.GetBool(AnimationString.canMove);
        } }
    public bool isAlive { get
        {
            return animator.GetBool(AnimationString.isAlive);
        } }

    

    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection= GetComponent<TouchingDirection>();
        damageable= GetComponent<Damageable>();


    }
    private void FixedUpdate()
    {
        
        if(!damageable.LockVelocity)
            rb2d.velocity = new Vector2(moveInput.x*CurSpeed, rb2d.velocity.y);

        animator.SetFloat(AnimationString.LookY, rb2d.velocity.y);

    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (isAlive)
        {
            
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);

        }
        else
        {
            IsMoving = false;
        }
      
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight= true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    public void onJump(InputAction.CallbackContext context)
    {
       if(context.started && touchingDirection.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationString.jump);
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpImpulse);
            
        }
       
    }
    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.fire);
        }
    }
    public void onHit(int damage, Vector2 knockback)
    {
        rb2d.velocity = new Vector2(knockback.x, rb2d.velocity.y + knockback.y);
        
    }
}
