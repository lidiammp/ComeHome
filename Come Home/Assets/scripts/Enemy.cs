using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hitpoints;
    public float MaxHitPoints = 5; 

    void Start()
    {
        Hitpoints = MaxHitPoints;
    }

    
    }


