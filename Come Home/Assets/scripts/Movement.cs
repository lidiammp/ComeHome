using UnityEngine;
using UnityEngine.Events;
public class Movement : MonoBehaviour

{
    public float MovementSpeed = 5;
    public float JumpForce = 5;
    private Rigidbody2D rb;
    public float jumpStartTime;
    private float jumpTime;
    private bool isJumping;
    bool isTouchingFront;
    float inputHorizontal;
    private float inputVertical;

    public Transform wallGrabPoint;
    private bool canGrab, isGrabbing;
    public LayerMask whatisGrabbable;
    private bool isGrounded;
    private float gravityStore;
    public float wallJumpTime = .2f;
    private float wallJumpCounter;

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
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");
    }

    private void Update()
    {
        if (wallJumpCounter <= 0)
        {

            movement = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

            animator.SetFloat("Speed", Mathf.Abs(movement)); //when the player is moving, run animation plays


            if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                isJumping = true;
                animator.SetBool("isJumping", true); //signal to animations that we're jumping
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
                    animator.SetBool("isJumping", false);
                }

            }
            //handle wall jumping 
            canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGrabbable);

            isGrabbing = false;
            if (canGrab && !isGrounded)
            {
                if ((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
                {
                    isGrabbing = true;
                }
            }
            if (isGrabbing)
            {
                rb.gravityScale = 0f;
                rb.velocity = Vector2.zero;
                if (Input.GetButtonDown("Jump"))
                {
                    wallJumpCounter = wallJumpTime;

                    rb.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * MovementSpeed, JumpForce);
                    rb.gravityScale = gravityStore;
                    isGrabbing = false;
                }
            }
            else
            {
                rb.gravityScale = gravityStore;
            }
        }
        else
        {
            wallJumpCounter -= Time.deltaTime;
        }

       //flips player direction around based on movement
        if (movement == 1)
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        } else if (movement == -1)
            {
            transform.localScale = new Vector3(-1.5f, 1.5f, 1f);
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





