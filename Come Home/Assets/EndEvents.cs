using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndEvents : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            StartCoroutine(Wait());
            
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(5);
        SceneManager.LoadScene("Main Menu");
    }

}
