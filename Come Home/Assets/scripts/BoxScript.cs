using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoxScript : MonoBehaviour
{
    public Vector2 resetPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.PlayerDiedEvent += reset; //add method to subscribe to the PlayerDeath event 
        resetPosition = gameObject.transform.position;
    }

    public void reset()
    {
        print("box reset");
        gameObject.transform.position = resetPosition;
    }

    private void OnDestroy()
    {
        GameEvents.current.PlayerDiedEvent -= reset; //unsubscribe
    }
}
