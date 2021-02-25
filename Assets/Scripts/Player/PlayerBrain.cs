﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    [Header("Settings")] 
    public float jumpSize;
    public Transform PlayerBodyTransform;
    
    
    /**
     * Events
     */
    public Action OnCollectStack;
    public Action OnLostStack;

    

    private void Start()
    {
        OnCollectStack += JumpPlayer;
    }

    public void JumpPlayer()
    {
        PlayerBodyTransform.position += Vector3.up * jumpSize; 
    }
}