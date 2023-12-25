using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(PlayerDamageable))]
public class VirusController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 5f;
    public float jumpImpulse = 8f;
    public float rollImpulse = 30f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public TrailRenderer tr;
    Animator animator;
    PlayerDamageable damageable;
    Vector2 moveInput;
    TouchingDirection touchingDirection;
    public GameObject pauseMenu;



    public float CurSpeed { 
        get {
            if (CanMove)
            {
                if (IsMoving && !touchingDirection.IsOnWall)
                {
                    if(touchingDirection.IsGrounded)
                    {
                        return 5;
                    }
                    else
                    {
                        return 3;
                    }
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
    internal static object instance;

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

    
    public bool canDoubleJump { get
        {
            return animator.GetBool(AnimationString.canDoubleJump);
        } }
    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection= GetComponent<TouchingDirection>();
        damageable= GetComponent<PlayerDamageable>();
        



    }

    private void FixedUpdate()
    {
       
        if (!damageable.LockVelocity && !isDashing)
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
        
        if (context.started && touchingDirection.IsGrounded && CanMove )
        {
                
                animator.SetTrigger(AnimationString.jump);
                animator.SetBool(AnimationString.canDoubleJump, true);
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpImpulse);
                
        }
        else if(context.started && !touchingDirection.IsGrounded && CanMove)
        {
            
            if (canDoubleJump)
            {
                
                animator.SetTrigger(AnimationString.jump);
                animator.SetBool(AnimationString.canDoubleJump, false);
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpImpulse);
                
            }
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
        if(!isDashing)
        {
            rb2d.velocity = new Vector2(knockback.x, rb2d.velocity.y + knockback.y);
        }
            
        
    }
    public void onDash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash)
        {
            
            animator.SetTrigger(AnimationString.roll);
            StartCoroutine(Dash());
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb2d.gravityScale;
        rb2d.gravityScale = 0f;
        rb2d.velocity = new Vector2(transform.localScale.x * rollImpulse, 0f);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb2d.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    public void onPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
            MyGameManager.Instance.PauseGame();
            pauseMenu.SetActive(true);
            
        }
    }
}
