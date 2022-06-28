using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhead : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyheadCheck>())
        {
            Destroy(transform.parent.gameObject);
        }

    }
}
