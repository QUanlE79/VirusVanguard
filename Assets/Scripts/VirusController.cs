using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class VirusController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    float horizontal;
    float vertical;
    public float speed = 5f;
     Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 moveInput;
    [SerializeField]
    private bool _isMoving=false;
    public bool IsMoving { get {
            return _isMoving;
        } 
        private set { 
            _isMoving= value;
            animator.SetBool("isMoving", value);
        } 
    }

    // Start is called before the first frame update
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
         
       
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveInput.x*speed, rb2d.velocity.y);
        if (!Mathf.Approximately(moveInput.x, 0.0f) || !Mathf.Approximately(0.0f, moveInput.y))
        {
            lookDirection.Set(moveInput.x, moveInput.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("LookX", lookDirection.x);

    }
    public void onMove(InputAction.CallbackContext context)
    {
        moveInput=context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
    }
}
