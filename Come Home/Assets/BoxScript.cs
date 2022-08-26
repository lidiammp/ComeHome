using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    public Transform resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = GetComponent<Transform>();
        GameEvents.current.PlayerDiedEvent += reset; //add method to subscribe to the PlayerDeath event 
        resetPosition.position = gameObject.transform.position;
    }

    public void reset()
    {
        print("box reset");
        gameObject.transform.position = resetPosition.position;
    }
}
