using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    public event Action PlayerDiedEvent;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public void PlayerJustDied() //checks if the event is null before invoking it
    {
        PlayerDiedEvent?.Invoke();
    }
     
}
