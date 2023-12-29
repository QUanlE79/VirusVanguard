using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GroundEmeniesScript;
using UnityEngine.SceneManagement;


public class BossTDScript : MonoBehaviour
{
    public float speed = 5f;

    private GameObject player;

    Rigidbody2D rb;

    Animator animator;

    Damageable damageable;

    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
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

    }
    // Start is called before the first frame update
    public AudioSource BgMusic;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Meet());
        blueBook.transform.localScale = new Vector3(1,1,1);
        redBook.transform.localScale = new Vector3(1, 1, 1);
        blueBook.GetComponent<SkillScipt>().damage = 10;
        redBook.GetComponent<SkillScipt>().damage = 10;
        theHand.transform.localScale = new Vector3(2, 2, 2);
        theRock.transform.localScale = new Vector3(2, 2, 2);
        BgMusic.Play();
    }
    private bool isChecked = false;
    public GameObject redBook;
    public GameObject blueBook;
    public GameObject theHand;
    public GameObject theRock;
    // Update is called once per frame
    SpriteRenderer spriteRenderer;
    void Update()
    {
        if (damageable.health < damageable.MaxHealth / 2 && isChecked == false)
        {
            isPhase2 = true;
            spriteRenderer.color = HexToColor("#FF92BF");
            isChecked = true;
            blueBook.transform.localScale *= 2f;
            redBook.transform.localScale *= 2f;
            blueBook.GetComponent<SkillScipt>().damage *= 2;
            redBook.GetComponent<SkillScipt>().damage *= 2;
            theHand.transform.localScale *= 2f;
            theRock.transform.localScale *= 2f;
        }
        
        Flip();
    }
    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString(hex, out color);
        return color;
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
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            //transform.rotation = Quaternion.Euler(0, 0, 0);

            
            WalkDirection = WalkableDirection.Left;

        }



    }
    public Transform rockLaunchPoint;
    private void LaunchRock()
    {
        
        GameObject projectie = Instantiate(theRock, rockLaunchPoint.position, theRock.transform.rotation);
        Vector3 orgin = projectie.transform.localScale;
        int direction = transform.localScale.x > 0 ? 1 : -1;
        projectie.transform.localScale = new Vector3(
            orgin.x * direction,
            orgin.y,
            orgin.z);
    }
    public Transform redBookLaunchPoint;
    public Transform blueBookLaunchPoint;
    private void LaunchRedBook()
    {
        GameObject projectie = Instantiate(redBook, redBookLaunchPoint.position, redBook.transform.rotation);
        Vector3 orgin = projectie.transform.localScale;
        int direction = transform.localScale.x > 0 ? 1 : -1;
        projectie.transform.localScale = new Vector3(
            orgin.x * direction,
            orgin.y,
            orgin.z);
    }
    public void LaunchBlueBook()
    {
        GameObject projectie = Instantiate(blueBook, blueBookLaunchPoint.position, blueBook.transform.rotation);
        Vector3 orgin = projectie.transform.localScale;
        int direction = transform.localScale.x > 0 ? 1 : -1;
        projectie.transform.localScale = new Vector3(
            orgin.x * direction,
            orgin.y,
            orgin.z);
    }
    public void LauchBook()
    {
        int randomInteger = Random.Range(0, 2);
        if (randomInteger == 1)
        {
            LaunchBlueBook();
        }
        else
        {
            LaunchRedBook();   
        }
    }
    public void LauchHand()
    {
        GameObject projectie = Instantiate(theHand, new Vector2(player.transform.position.x, player.transform.position.y), theHand.transform.rotation);
        Vector3 orgin = projectie.transform.localScale;
        int direction = transform.localScale.x > 0 ? 1 : -1;
        projectie.transform.localScale = new Vector3(
            orgin.x * direction,
            orgin.y,
            orgin.z);
    }
    [SerializeField] private SimpleFlash flashEffect;
    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
        flashEffect.Flash();

    }
    // void Death()
    // {
    //     Destroy(gameObject);
    // }
    public Canvas MeetDialog;
    private float Duration = 5f;

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
    }
    public Canvas Dialog;
    private bool deathHandled = false;
    public AudioSource WinningMusic;
    private IEnumerator Death()
    {
        if (!deathHandled)
        {

            GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
            foreach (GameObject projectile in projectiles)
            {
                Destroy(projectile);
            }
            Dialog.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(Duration);
            Dialog.gameObject.SetActive(false);
            BgMusic.Stop();
            WinningMusic.Play();
            deathHandled = true; 
        }
        StartCoroutine(ShowCredit());

    }
    public Canvas Credit;
    public Transform EndCredit;
    private IEnumerator ShowCredit()
    {

        float elapsedTime = 0f;
        float creditDuration = 20f;

        while (elapsedTime < creditDuration)
        {
            Credit.transform.position = Vector2.MoveTowards(Credit.transform.position, EndCredit.position, 0.02f * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Destroy(player);
        SceneManager.LoadScene(0);
    }

    private void FixedUpdate()
    {
        if (isAlive)
        {
            return;
        }
        
        StartCoroutine(Death());
        
    }
}
