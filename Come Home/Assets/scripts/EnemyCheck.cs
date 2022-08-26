using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnemyCheck : MonoBehaviour
{
    public int Respawn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>())
        {
            Destroy(transform.parent.gameObject);
            //broadcast event of Player Death

        }
    }
}
