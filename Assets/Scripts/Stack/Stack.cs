using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEngine;

public class Stack : MonoBehaviour
{
    private StackBrain _stackBrain;
    private void Start()
    {
        _stackBrain = GetComponentInParent<StackBrain>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.tag = "Untagged";
            _stackBrain.playerBrain.OnCollectStack();
            
            Destroy(other.gameObject);
            
            GameObject _collected = Instantiate(_stackBrain.CollectedStackPrefab);
            _collected.transform.position = _stackBrain.GetSpawnPoint();
            _collected.transform.parent = _stackBrain.transform;
        }
    }
}
