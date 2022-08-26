using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action PlayerDiedEvent;
    public void PlayerJustDied() //checks if the event is null before invoking it
    {
        if (PlayerDiedEvent != null)
        {
            PlayerDiedEvent();
        }
    }
     
}
