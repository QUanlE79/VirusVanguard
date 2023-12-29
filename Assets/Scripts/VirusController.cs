using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(PlayerDamageable))]
public class VirusController : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 5f;
    public float jumpImpulse = 8f;
    public float rollImpulse = 30f;
    private bool canDash = true;
    public bool isDashing;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public TrailRenderer tr;
    Animator animator;
    PlayerDamageable damageable;
    Vector2 moveInput;
    TouchingDirection touchingDirection;
    public GameObject pauseMenu;
    public GameObject optionDialog;
    public AudioSource bowCharge;
    public AudioSource bowRelease;
    public AudioSource footStep1;
    public AudioSource DashAudio;
    public AudioSource doubleJump;
    public AudioSource Jump;
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
        
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        FileManager.LoadEquipmentAtStart();
        PlayerDamageableData loadedData = FileManager.LoadPlayerDamageableData();
        if (loadedData != null)
        {
            // Apply the loaded data to the playerDamageable instance
            damageable.LoadPlayerDamageableData(loadedData);
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            
            Vector2 direction = new Vector2(transform.localScale.x, 0f); 
            RaycastHit2D hit = Physics2D.Raycast(rb2d.position, direction, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                NPC character = hit.collider.GetComponent<NPC>();

                if (character != null)
                {
                    character.DisplayDialog();
                }
            }

            RaycastHit2D hitMedkit = Physics2D.Raycast(rb2d.position, direction, 1.5f, LayerMask.GetMask("Medkit"));

            if (hitMedkit.collider != null)
            {
                MedkitScript mk = hitMedkit.collider.GetComponent<MedkitScript>();

                if (mk != null)
                {
                    mk.RecoverHP();
                }
            }
        }
        if(virtualCamera == null)
        {
            virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();

            // Check if the Virtual Camera is found
            if (virtualCamera != null)
            {
                // Adjust settings or follow the player as needed
                virtualCamera.Follow = transform;
            }
        }
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
            Jump.Play();
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
        if (context.started && !EventSystem.current.IsPointerOverGameObject())
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
        if (context.performed && canDash && CanMove)
        {
            DashAudio.Play();
            
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
            if (!optionDialog.activeInHierarchy) {
                MyGameManager.Instance.PauseGame();
                pauseMenu.SetActive(true);
            }
            
        }
    }
    public void onChargeBow()
    {
        bowCharge.Play();
    }
    public void onReleaseBow()
    {
        bowRelease.Play();
    }
    public void FootStepPlay()
    {
        footStep1.Play();
        InvokeRepeating("StopFootStep", 0.5f, 0);
    }
    public void StopFootStep()
    {
        footStep1.Stop();
    }
    public void OnSecondJump()
    {
        doubleJump.Play();
    }
   
}
