
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float MovementSpeed = 5;
    public float JumpForce = 5;
    private Rigidbody2D rb;
    public float jumpStartTime;
    private float jumpTime;
    private bool isJumping;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        var movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;

        if (Input.GetButtonDown("Jump") && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            isJumping = true;
            jumpTime = jumpStartTime;
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
        if (Input.GetButton("Jump") &&isJumping == true)
        {
            if (jumpTime > 0)
                {
                rb.velocity = Vector2.up * JumpForce;
                jumpTime -= Time.deltaTime;
            }
            else
            {
                isJumping =false;
            }

        }
        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }


}
