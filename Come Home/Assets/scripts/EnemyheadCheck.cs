using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyheadCheck : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print(rb.velocity);
        print("collided with: ");
        if (collision.tag == "enemy")
        {
            print("enemy");
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * 0f);

            //Destroy(collision.GetComponentInParent(gameObject).gameObject);
            GameEvents.current.EnemyJustDied();
        }
    }

}
