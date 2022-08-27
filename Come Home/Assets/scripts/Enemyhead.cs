using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhead : MonoBehaviour
{
    private void Start()
    {
        GameEvents.current.EnemyDiedEvent += enemyDeath;
    }

    public void enemyDeath()
    {
        Destroy(transform.parent.gameObject);
    }

    private void OnDestroy()
    {
        GameEvents.current.EnemyDiedEvent -= enemyDeath;
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "playerFeet")
    //    {
    //        //collision.attachedRigidbody.position = new Vector2(1, 2);
    //        Destroy(transform.parent.gameObject);
    //    }

    //}
}
