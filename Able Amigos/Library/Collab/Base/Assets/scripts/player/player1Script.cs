using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player1Script : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Basic Movement")]
    public float speed;
    public float jumpForce;

    // jump variables
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    // grounded variables
    [Header("Ground/Jump")]
    bool isGrounded = true;
    public GameObject feet;
    public Transform isGroundedCheck;
    public float checkGroundRadius;
    public LayerMask groundLayer;
    public LayerMask otherPlayer;
    //Extra Jumping Vars
    public float hangTime  = 0.2f;
    private float hangCounter;
    public float jumpBufferLength = .1f;
    private float jumpBufferCount;

    // sprite variables + flip sprite
    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public Sprite[] spriteArray;
    public GameObject shootpoint;
    private int size = 0;
    [HideInInspector]
    public bool isFacingLeft;
    public bool spawnFacingLeft;
    private Vector2 facingLeft;

    // health vars
    [Header("Health")]
    public int health = 1;
    bool waitcall = false;

    // for growing
    [Header("Grow Variables")]
    public Transform isAboveCheck;
    public float checkAboveRadius;
    public bool canGrow = true;
    public static player1Script inst;
    int score = 0;

    // for pellet shooting
    [Header("Shooting")]
    public Transform shootPoint;
    float pelletspeed;
    public GameObject pellet;

    // fix until we can figure out another way to do this
    int count1 = 0;
    int count2 = 0;

    //maintaing size with platform stuff
    Vector3 playerScale;
    public Vector3 platformScale;

    [Header("Bar Sprites")]
    public Sprite empty;
    public Sprite oneBar;
    public Sprite twoBar;
    public Sprite threeBar;

    // particles
    public Particles particles;

    // animation
    public Animator anim;

    [Header("Sound Effects")]
    public AudioSource source;
    public AudioClip pelletShoot;
    public AudioClip growSound;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        if (inst == null)
        {
            inst = this;
        }

        // initialization
        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);
        if (!spawnFacingLeft)
        {
            transform.localScale = facingLeft;
            isFacingLeft = false;
        }

        //platform size tings
        playerScale = transform.localScale;
        if (GameObject.FindWithTag("platform") != null)
        {
            platformScale = GameObject.FindWithTag("platform").GetComponent<PlatformMove>().platformScale;
        }
        
    }

    void Update()
    {
        Move();
        Jump();
        betterJump();
        checkGround();
        checkTop();
        Grow();
        checkPop();
        //ReSize();
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.A) && !isFacingLeft || Input.GetKey(KeyCode.D) && !isFacingLeft)
        {
            isFacingLeft = true;
            Flip();
        }
         if (Input.GetKey(KeyCode.D) && isFacingLeft || Input.GetKey(KeyCode.G) && isFacingLeft)
        {
            isFacingLeft = false;
            Flip();
        }
        
    }

    void Move()
    {
        float x = 0;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.G))
        {
            x = 1;
        } else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            x = -1;
        }

        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        if (x == 0)
        {
            anim.SetBool("isWalking", false);
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            anim.SetBool("isWalking", true);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        // shoot pellet
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.T))
        {
            FirePellet();
            source.clip = pelletShoot;
            source.Play();

        }
    }

    void FirePellet()
    {
        if (score > 0 && !isFacingLeft)
        {
            count2 = 0;

            if (count1 == 0)
            {
                // rotate the shoot point
                Vector3 zRotate = new Vector3(0, 0, 0);
                shootPoint.transform.eulerAngles = zRotate;
                count1++;
            }

            GameObject p = Instantiate(pellet, shootPoint.position, shootPoint.rotation);
            p.GetComponent<Pellet>().spawner = this.gameObject;
            subtractScore();
        }
        else if (score > 0 && isFacingLeft)
        {
            count1 = 0;

            if (count2 == 0)
            {
                // rotate the shoot point
                Vector3 zRotate = new Vector3(0, 0, 180);
                shootPoint.transform.eulerAngles = zRotate;
            }

            GameObject p = Instantiate(pellet, shootPoint.position, shootPoint.rotation);
            p.GetComponent<Pellet>().spawner = this.gameObject;
            subtractScore();
        } else if (size > 0 && !isFacingLeft)
        {
            count2 = 0;

            if (count1 == 0)
            {
                // rotate the shoot point
                Vector3 zRotate = new Vector3(0, 0, 0);
                shootPoint.transform.eulerAngles = zRotate;
                count1++;
            }

            GameObject p = Instantiate(pellet, shootPoint.position, shootPoint.rotation);
            p.GetComponent<Pellet>().spawner = this.gameObject;
            ForceShrink();
        } else if (size > 0 && isFacingLeft)
        {
            count1 = 0;

            if (count2 == 0)
            {
                // rotate the shoot point
                Vector3 zRotate = new Vector3(0, 0, 180);
                shootPoint.transform.eulerAngles = zRotate;
            }

            GameObject p = Instantiate(pellet, shootPoint.position, shootPoint.rotation);
            p.GetComponent<Pellet>().spawner = this.gameObject;
            ForceShrink();
        }
    }

    void Jump()
    {
        // manage jump buffer
        if (Input.GetKeyDown("w") || Input.GetKeyDown(KeyCode.E))
        {
            jumpBufferCount = jumpBufferLength;
        }
        else
        {
            jumpBufferCount -= Time.deltaTime;
        }

        if (jumpBufferCount >= 0 &&  hangCounter > 0f)
        {
            anim.SetBool("isJump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // variable jump height
        if (Input.GetKeyUp("w") && rb.velocity.y > 0 || Input.GetKeyUp(KeyCode.E) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.02f);
            jumpBufferCount = 0;
        }
    }

    void betterJump()
    {
        if (rb.velocity.y < 0)  // if player is falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.UpArrow) || rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.E))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void checkGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, groundLayer);
        Collider2D playerCollider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, otherPlayer);

        if (collider != null)
        {
            isGrounded = true;
        }
        else if(playerCollider != null)
        {
            isGrounded = true;
        } 
        else 
        {
            isGrounded = false;
        }

        //for hang time
        if (isGrounded)
        {
            hangCounter = hangTime;
        }
        else
        {
            hangCounter -= Time.deltaTime;
        }

        // for jumping
        if (isGrounded == true && anim.GetBool("isJump") == true && rb.velocity.y < 0)
        {
            anim.SetBool("isJump", false);
        }
    }

    void checkTop()
    {
        Collider2D collider = Physics2D.OverlapCircle(isAboveCheck.position, checkAboveRadius, groundLayer);

        if (collider != null)
        {
            canGrow = false;
        }
        else
        {
            canGrow = true;
        }
    }

    void checkPop()
    {
        if (this.transform.position.y < -4)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            pop();
        }

        if (health == 0)
            pop();
    }

    void pop()
    {
        speed = 0;
        if (waitcall == false)
            StartCoroutine(wait());
        waitcall = true;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // grow the character
    public void Grow()
    {
        Vector2 m_Min;

        if (Input.GetKeyDown("f") && score > 0 && canGrow || Input.GetKeyDown(KeyCode.Alpha6) && score > 0 && canGrow)
        {
            subtractScore();
            size++;
            source.clip = growSound;
            source.Play();
            if (size > 2)
                size = 0;
            // chang the size of the box collider

            // change collider size to match the sprite
            switch (size)
            {
                case 0:
                    particles.Emmit();
                    anim.SetInteger("size", 0);
                    manualScore(3);
                    GetComponent<BoxCollider2D>().size = new Vector2(.62f, .48f);
                    GetComponent<CapsuleCollider2D>().size = new Vector2(.62f, .8f);
                    m_Min = GetComponent<CapsuleCollider2D>().bounds.min;
                    feet.transform.position = new Vector2(m_Min.x, m_Min.y);
                    break;
                case 1:
                    anim.SetInteger("size", 1);
                    GetComponent<BoxCollider2D>().size = new Vector2(.62f, 1f);
                    GetComponent<CapsuleCollider2D>().size = new Vector2(.62f, 1.3f);
                    m_Min = GetComponent<CapsuleCollider2D>().bounds.min;
                    feet.transform.position = new Vector2(m_Min.x, m_Min.y);
                    PlatformBreak();
                    break;
                case 2:
                    anim.SetInteger("size", 2);
                    GetComponent<BoxCollider2D>().size = new Vector2(.62f, 1.5f);
                    GetComponent<CapsuleCollider2D>().size = new Vector2(.62f, 1.8f);
                    m_Min = GetComponent<CapsuleCollider2D>().bounds.min;
                    feet.transform.position = new Vector2(m_Min.x, m_Min.y);
                    PlatformBreak();
                    break;
            }
            spriteRenderer.sprite = spriteArray[size];
        }
        else if (!canGrow && Input.GetKeyDown("f") || !canGrow && Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Can't grow!");
        }
    }

    void ForceShrink ()
    {
        Vector2 m_Min;
        size--;
        switch (size)
        {
            case 0:
                anim.SetInteger("size", 0);
                GetComponent<BoxCollider2D>().size = new Vector2(.62f, .5f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(.62f, .8f);
                m_Min = GetComponent<CapsuleCollider2D>().bounds.min;
                feet.transform.position = new Vector2(m_Min.x, m_Min.y);
                break;
            case 1:
                anim.SetInteger("size", 1);
                GetComponent<BoxCollider2D>().size = new Vector2(.62f, 1f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(.62f, 1.3f);
                m_Min = GetComponent<CapsuleCollider2D>().bounds.min;
                feet.transform.position = new Vector2(m_Min.x, m_Min.y);
                PlatformBreak();
                break;
            case 2:
                anim.SetInteger("size", 2);
                GetComponent<BoxCollider2D>().size = new Vector2(.62f, 1.5f);
                GetComponent<CapsuleCollider2D>().size = new Vector2(.62f, 1.8f);
                m_Min = GetComponent<CapsuleCollider2D>().bounds.min;
                feet.transform.position = new Vector2(m_Min.x, m_Min.y);
                PlatformBreak();
                break;
        }
        spriteRenderer.sprite = spriteArray[size];
    }

    private void Flip()
    {
        if (!isFacingLeft)
        {
            transform.localScale = facingLeft;
        }
        if (isFacingLeft)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }

    public void changeScore(int s)
    {
        score = s;
    }

    public void subtractScore()
    {
        score--;
        ScoreManager.instance.subScoreP1(1);
    }

    void manualScore(int s)
    {
        score = s;
        ScoreManager.instance.forceChange1(s);
    }

    /*void ReSize()
    {
        if(this.transform.parent != null && this.transform.parent == GameObject.FindWithTag("platform").transform)
        {
            //keep size constant
            transform.localScale = new Vector3(playerScale.x / platformScale.x , playerScale.y / platformScale.y, playerScale.z / platformScale.z);
        }
    }*/

    void PlatformBreak()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(this.transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.tag == "breakable")
            {
                Destroy(hitCollider.gameObject);
            }
        }
    }

}
