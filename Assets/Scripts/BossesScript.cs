using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossesScript : MonoBehaviour
{
    public float speed = 3f;
    public bool chase = false;
 
    private GameObject player;

    public float walkStopRate = 0.05f;

    public DetectionZone attackZone;

    public DetectionZone attackZonePhase2;

    Rigidbody2D rb;

    Animator animator;

    Damageable damageable;

    SpriteRenderer spriteRenderer;

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

    public bool IsPhase2
    {
        get
        {
            return animator.GetBool(AnimationString.isPhase2);
        }
        private set
        {
            animator.SetBool(AnimationString.isPhase2, value);
        }
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private bool isChecked = false;

    public GameObject SQCC;
    public GameObject SQCK;
    public GameObject SQND;

    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; }
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                Chase();
            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
            }
        }

        if (damageable.health < damageable.MaxHealth/2 && isChecked == false)
        {
            IsPhase2 = true;
            speed = 6f;
            spriteRenderer.color = HexToColor("#FF92BF");
            isChecked = true;
            Vector2 spawPoint = transform.position;
            GameObject quaiSCC = Instantiate(SQCC, spawPoint, Quaternion.identity);
            GameObject quaiSCK = Instantiate(SQCK, new Vector2(spawPoint.x + 3, spawPoint.y), Quaternion.identity);
            GameObject quaiSND = Instantiate(SQND, new Vector2(spawPoint.x - 3, spawPoint.y), Quaternion.identity);
        }

        Flip();
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (HasTarget) CanMove = false; else CanMove = true;
        if (atkCd > 0)
        {
            atkCd -= Time.deltaTime;
        }
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
    }
    private void Chase()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }
    
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
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
