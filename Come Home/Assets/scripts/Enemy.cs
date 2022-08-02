using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{

     [SerializeField] float moveSpeed = 2f;

    public float Hitpoints;
    public float MaxHitPoints = 5;
    public int Respawn;


    Rigidbody2D myRigidbody;
    BoxCollider2D myBoxCollider;

    void Start()
    {
        Hitpoints = MaxHitPoints;
        myRigidbody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        if (IsFacingRight())
        {
            // Move Right
            myRigidbody.velocity = new Vector2(moveSpeed, 0f);

        }
        else
        {
            // Move left
            myRigidbody.velocity = new Vector2(-moveSpeed, 0f);
        }
    }
    private bool IsFacingRight()
    {
        return transform.localScale.x > Mathf.Epsilon;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Turn
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), transform.localScale.y);
    }


}


