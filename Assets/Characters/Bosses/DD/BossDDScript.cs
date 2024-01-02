using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static GroundEmeniesScript;

public class BossDDScript : MonoBehaviour
{
    public float speed = 5f;

    private GameObject player;

    Rigidbody2D rb;

    Animator animator;

    Damageable damageable;
    public GameObject door;


    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
    }

    public bool isChase
    {
        get
        {
            return animator.GetBool("isChase");
        }
        private set
        {
            animator.SetBool("isChase", value);
        }
    }

    public bool isPhase2
    {
        get
        {
            return animator.GetBool("isPhase2");
        }
        private set
        {
            animator.SetBool("isPhase2", value);
        }
    }

    private void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door");
    }
    // Start is called before the first frame update
    public AudioSource warningStart;
    public AudioSource bgMusic;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Meet());
        //warningStart.Play();
        bgMusic.Play();
    }
    private bool isChecked = false;
    public GameObject Dan;
    // Update is called once per frame
    SpriteRenderer spriteRenderer;
    void Update()
    {
        if (damageable.health < damageable.MaxHealth / 2 && isChecked == false)
        {
            isPhase2 = true;
            speed = 6f;
            spriteRenderer.color = HexToColor("#FF92BF");
            isChecked = true;
        }
        if (isChase)
        {
            Chase();
        }
        Flip();
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
    private WalkableDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            //transform.rotation = Quaternion.Euler(0, 180, 0);
            WalkDirection = WalkableDirection.Left;
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);

            WalkDirection = WalkableDirection.Right;

        }



    }
    //public GameObject projectilePrefab;
    private void LaunchPhase1()
    {
        GameObject projectie = Instantiate(Dan, new Vector2(player.transform.position.x, player.transform.position.y + 10), Dan.transform.rotation);
        Vector3 orgin = projectie.transform.localScale;
        int direction = transform.localScale.x > 0 ? 1 : -1;
        projectie.transform.localScale = new Vector3(
            orgin.x * direction,
            orgin.y,
            orgin.z);
    }
    private void LaunchPhase2()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject projectie = Instantiate(Dan, new Vector2(player.transform.position.x + i * 2, player.transform.position.y + 10), Dan.transform.rotation);
            Vector3 orgin = projectie.transform.localScale;
            int direction = transform.localScale.x > 0 ? 1 : -1;
            projectie.transform.localScale = new Vector3(
                orgin.x * direction,
                orgin.y,
                orgin.z);

        }
    }
    public void LaunchDan()
    {
        if (isPhase2)
        {
            LaunchPhase2();
        }
        else
        {
            LaunchPhase1();
        }
    }
    [SerializeField] private SimpleFlash flashEffect;
    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        flashEffect.Flash();

    }
    public Canvas Dialog;
    private float Duration = 5f;
    private void FixedUpdate()
    {
        if (!isAlive)
        {
            StartCoroutine(Death());
            Destroy(gameObject); 
        }
    }
    private IEnumerator Death()
    {
        // Freeze the screen
        //Time.timeScale = 0f;
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

        // Display the canvas notification
        Dialog.gameObject.SetActive(true);
        
        
        // Wait for a duration
        yield return new WaitForSecondsRealtime(Duration);

        // Unfreeze the screen
        //Time.timeScale = 1f;

        // Hide the canvas notification
        Dialog.gameObject.SetActive(false);
        // SceneManager.LoadScene(0);
    }
    public Canvas MeetDialog;
    //private float Duration = 5f;
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
        //warningStart.Stop();
    }
}
