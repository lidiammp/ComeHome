
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 5;
    public float JumpForce = 5;
    private Rigidbody2D rb;

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
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }
    }
}
