using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QSScript : MonoBehaviour
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
    public int minCoin = 90;
    public int maxCoin = 150;

    private bool _hasTarget = false;
    public GameObject door;
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
        door = GameObject.FindGameObjectWithTag("Door");
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Meet());
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
            speed = 5f;
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
    private void FixedUpdate()
    {
        if(!isAlive)
        {
            Death();
            Destroy(gameObject);
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
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject x in enemies)
        {
            Destroy(x);
        }
        damageable.DropCoin(minCoin,maxCoin);
        BoxCollider2D boxCollider = door.GetComponent<BoxCollider2D>();
        SpriteRenderer spriteRenderer = door.GetComponent<SpriteRenderer>();

        if (boxCollider != null)
        {
            // Toggle the enabled state of the BoxCollider2D
            boxCollider.enabled = true;
        }
        spriteRenderer.color = new Color(
                spriteRenderer.color.r,
                spriteRenderer.color.g,
                spriteRenderer.color.b,
                255 // invert the alpha value
            );
        StartCoroutine(DeathSpeech());
    }
    public Canvas MeetDialog;
    public Canvas EndDialog;
    private float Duration = 5f;
    private IEnumerator Meet()
    {
        // Freeze the screen
        Time.timeScale = 0f;

        // Display the canvas notification
        MeetDialog.gameObject.SetActive(true);

        // Wait for a duration
        yield return new WaitForSecondsRealtime(Duration);

        // Unfreeze the screen
        Time.timeScale = 1f;

        // Hide the canvas notification
        MeetDialog.gameObject.SetActive(false);
    }
    private IEnumerator DeathSpeech()
    {
        // Freeze the screen
        //Time.timeScale = 0f;

        // Display the canvas notification
        EndDialog.gameObject.SetActive(true);

        // Wait for a duration
        yield return new WaitForSecondsRealtime(Duration);

        // Unfreeze the screen
        //Time.timeScale = 1f;

        // Hide the canvas notification
        EndDialog.gameObject.SetActive(false);
        //SceneManager.LoadScene(0);
    }
}
