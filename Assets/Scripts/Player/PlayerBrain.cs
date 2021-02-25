using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBrain : MonoBehaviour
{
    [Header("Settings")] 
    public float jumpSize;
    public Transform PlayerBodyTransform;
    public StackBrain _stackBrain;
    
    
    /**
     * Events
     */
    public Action OnCollectStack;
    public Action OnLostStack;
    public Action OnGameOver;

    

    private void Start()
    {
        _stackBrain = GetComponentInChildren<StackBrain>();
        
        OnCollectStack += JumpPlayer;

        OnLostStack += LostMessage;
        OnLostStack += CheckGameOver;

        OnGameOver += GameOver;
    }

    public void JumpPlayer()
    {
        PlayerBodyTransform.position += Vector3.up * jumpSize; 
    }

    public void LostMessage()
    {
        Debug.Log("LostStack");
    }

    private void GameOver()
    {
        Time.timeScale = 0;
    }

    private void CheckGameOver()
    {
        if (!_stackBrain.IsHaveStack())
        {
            OnGameOver();
        }
    }
}
