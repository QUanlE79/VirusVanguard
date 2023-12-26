using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemies : MonoBehaviour
{
    public float speed = 2f;
    public bool chase = false;
    public Transform spawPoint;
    //private Vector3 startPoint;
    private GameObject player;

    public float walkStopRate = 0.05f;

    public DetectionZone attackZone;

    Rigidbody2D rb;

    Animator animator;

    Damageable damageable;


    private bool _hasTarget = false;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
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

    public float atkCd
    {
        get
        {
            return animator.GetFloat(AnimationString.atkCd);
        }
        private set
        {
            animator.SetFloat(AnimationString.atkCd, Mathf.Max(value, 0));
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //startPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; }
        if (chase == true)
        {
            Chase();
        }
        else
        {
            ReturnSpawPoint();
        }
        
        Flip();

        HasTarget = attackZone.detectedColliders.Count > 0;
        if (HasTarget) CanMove = false; else CanMove = true;
        if (atkCd > 0)
        {
            atkCd -= Time.deltaTime;
        }
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }
    private void ReturnSpawPoint()
    {
        transform.position = Vector2.MoveTowards(transform.position, spawPoint.position, speed * Time.deltaTime);
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
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
        Destroy(gameObject);
    }
}
