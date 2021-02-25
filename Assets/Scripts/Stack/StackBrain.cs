using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBrain : MonoBehaviour
{
    public PlayerBrain playerBrain;
    public float stepSize;
    
    public GameObject CollectedStackPrefab;
    public GameObject CollectStackEffect;

    public List<GameObject> CollectedList;

    private void Start()
    {
        playerBrain = GetComponentInParent<PlayerBrain>();
    }

    public Vector3 GetSpawnPoint()
    {
        return playerBrain.PlayerBodyTransform.position + (Vector3.down * stepSize);
    }
}
