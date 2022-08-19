using UnityEngine;
using UnityEngine.Events;
public class Movement : MonoBehaviour

{
    public float MovementSpeed = 5;
    public float JumpForce = 5;
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

    }

    void FixedUpdate()
    {
        
    }

    private void Update()
    {

        isTouchingGround = groundCheck.IsTouchingLayers(whatIsGround); //[NOT WORKING] Always shows false even when it is overlapping the "Ground" layer

        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
        //move when based on the Axis Input
        movement = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        //when the player is moving, run animation plays
        animator.SetFloat("Speed", Mathf.Abs(movement));

        if (wallJumpCounter <= 0)
        {
            //if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f) //if button jumped and you're in the air
            //{
            //    isJumping = true;
            //    animator.SetBool("isJumping", true); //signal to animations that we're jumping

            //    jumpTime = jumpStartTime;
            //    rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            //}

            //if (Input.GetButton("Jump") && isJumping == true)
            //{
            //    if (jumpTime > 0)
            //    {
            //        rb.velocity = Vector2.up * JumpForce;
            //        jumpTime -= Time.deltaTime;
            //    }
            //    else
            //    {
            //        isJumping = false;
            //        animator.SetBool("isJumping", false);
            //    }

            //}

            //handle wall jumping 
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGrabbable);

            //determine if the player is grabbing the wall currently
            isGrabbing = false;
            if (canGrab && !isGrounded)
            {
                isGrabbing = true;
                /*if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
                {
                    isGrabbing = true;
                }*/
            }

            //jump code-- allows you to jump once, or multiple if you're holding onto the wall
            if ((Input.GetButtonDown("Jump") && !isJumping) || (Input.GetButtonDown("Jump") && isJumping && isGrabbing))
            { //if you press jump, and you are not jumping OR if you press jump, you are jumping, and you're holding onto the wall
                isJumping = true;
                animator.SetBool("isJumping", true); //signal to animations that we're jumping
                rb.velocity = Vector2.up * JumpForce;
            }

            //if you hit the ground, you regain your jump [BUG: if collide with a wall, it slows you slow down to 0, allowing you to wall jump off of any surface]

            if(Mathf.Abs(rb.velocity.y) == 0f && groundCheck.IsTouchingLayers(whatIsGround))
            {
                isJumping = false;
                animator.SetBool("isJumping", false);
            }



            //    if (isGrabbing)
            //    {
            //        rb.gravityScale = 0f;
            //        rb.velocity = Vector2.zero;
            //        if (Input.GetButtonDown("Jump"))
            //        {
            //            wallJumpCounter = wallJumpTime;

            //            rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * MovementSpeed, JumpForce);
            //            rb.gravityScale = gravityStore;
            //            isGrabbing = false;
            //        }
            //    }
            //    else
            //    {
            //        rb.gravityScale = gravityStore;
            //    }
            //}
            //else
            //{
            //    wallJumpCounter -= Time.deltaTime;
            //}

            //flips player direction based on the player's left/right inputs
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
        if (other.tag == "obs")
        {
            Debug.Log("Dead!");
            Application.LoadLevel(1);
        }
    }
}





