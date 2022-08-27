using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Movement : MonoBehaviour

{
    public float MovementSpeed = 5;
    public float JumpForce = 5;
    public float HighJumpForce = 2;
    private Rigidbody2D rb;
    public float jumpStartTime;
    private float jumpTime;
    public bool isJumping = false;
    bool isTouchingFront;
    float inputHorizontal;
    private float inputVertical;

    public Transform wallGrabPoint;
    public bool canGrab, isGrabbing;
    public LayerMask whatisGrabbable;

    private bool isGrounded;
    public BoxCollider2D groundCheck;
    public LayerMask whatIsGround;
    public bool isTouchingGround = true;

    private float gravityStore;
    public float wallJumpTime = .2f;
    private float wallJumpCounter = 0;

    public UnityEvent OnLandEvent;
    public Animator animator;

    public float velocity = 5;

    public Transform lastRespawn;
    
    public class BoolEvent : UnityEvent<bool> { }

    

    //health 
    public int curHealth;
    public int maxHealth = 5;
    public float movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gravityStore = rb.gravityScale;
        //health 
        curHealth = maxHealth;

        GameEvents.current.PlayerDiedEvent += playerDeath; //add method to subscribe to the PlayerDeath event 
    }

    void FixedUpdate()
    {
        
    }

    private void Update()
    {

        isTouchingGround = groundCheck.IsTouchingLayers(whatIsGround);

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //move when based on the Axis Input
        movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        //when the player is moving, run animation plays
        animator.SetFloat("Speed", Mathf.Abs(movement));

        /*var movement = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

            animator.SetFloat("Speed", Mathf.Abs(movement)); //when the player is moving, run animation plays


            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                isJumping = true;
                jumpTime = jumpStartTime;
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            }

            if (Input.GetButton("Jump") && isJumping == true)
            {
                if (jumpTime > 0)
                {
                    rb.velocity = Vector2.up * JumpForce;
                    jumpTime -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }*/

        if (wallJumpCounter <= 0)
        {
            //handle wall jumping 
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGrabbable);

            //determine if the player is grabbing the wall currently
            isGrabbing = false;
            if (canGrab && !isGrounded)
            {
                isGrabbing = true;
            }

            //jump code-- allows you to jump once, or multiple if you're holding onto the wall
            if ((Input.GetButtonDown("Jump") && isTouchingGround) || (Input.GetButtonDown("Jump") && isJumping && isGrabbing))
            { //if you press jump, and you are not jumping OR if you press jump, you are jumping, and you're holding onto the wall
                isJumping = true;
                jumpTime = jumpStartTime;
                animator.SetBool("isJumping", true); //signal to animations that we're jumping
                rb.velocity = Vector2.up * JumpForce;
                print("jump");
            }

            //hold to jump code-- allows you to hold the jump button for the length of "JumpTime"
            if (Input.GetButton("Jump") && isJumping == true)
            {
                if(jumpTime > 0)
                {
                    rb.velocity = Vector2.up * HighJumpForce;
                    jumpTime -= Time.deltaTime;
                    print("high jump");
                }
                else
                {
                    isJumping = false;
                }
            }
            if (Input.GetButtonUp("Jump"))
            {
                isJumping = false;
            }

            //if you hit the ground, you regain your jump [BUG: if collide with a wall, it slows you slow down to 0, allowing you to wall jump off of any surface]

            if(Mathf.Abs(rb.velocity.y) == 0f && groundCheck.IsTouchingLayers(whatIsGround))
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }

            if (movement == 1)
            {
                transform.localScale = new Vector3(1.5f, 1.5f, 1f);
            }
            else if (movement == -1)
            {
                transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
            }

        }
    }



    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "respawn") //if collide with respawn square
        {
            lastRespawn = other.transform; //it's position becomes the player's respawn point
            print("respawn");
        }

        if (other.tag == "obs")
        {
            Debug.Log("Dead!");
            GameEvents.current.PlayerJustDied(); //trigger the event
        }
    }

    public void playerDeath()
    {
        transform.position = lastRespawn.position; //move to the respawn point
    }

    private void OnDestroy()
    {
        GameEvents.current.PlayerDiedEvent -= playerDeath; //unsubscribe
        print("destroy");
    }
}





