using UnityEngine;
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

    

    //health 
    public int curHealth;
    public int maxHealth = 5;

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
        inputVertical = Input.GetAxisRaw("Verticle");

    }

    private void Update()
    {
        if (wallJumpCounter <= 0)
        {

            var movement = Input.GetAxisRaw("Horizontal");
            transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;



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

       if (rb.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        } else if (rb.velocity.x < 0)
            {
            transform.localScale = new Vector3(-1, 1, 1f);
        }

    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "obs")
        {
            Debug.Log("Dead!");
            Application.LoadLevel(0);
        }
    }
}





