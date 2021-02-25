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

            /**
             * Spawn Effect
             */
            GameObject _effect = Instantiate(
                _stackBrain.CollectStackEffect,
                other.transform.position,
                Quaternion.identity
            );
            Destroy(_effect, 2f);
            
            Destroy(other.gameObject);
            
            GameObject _collected = Instantiate(_stackBrain.CollectedStackPrefab);
            _collected.transform.position = _stackBrain.GetSpawnPoint();
            _collected.transform.parent = _stackBrain.transform;
            _stackBrain.CollectedList.Add(_collected);
            
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            _stackBrain.playerBrain.OnLostStack();

            _stackBrain.CollectedList.Remove(gameObject);
            
            Stack _stack = GetComponent<Stack>();
            Rigidbody _rb = GetComponent<Rigidbody>();

            _rb.constraints = RigidbodyConstraints.None;
            transform.parent = null;
            Destroy(_stack);
            Destroy(gameObject, 2f);

        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pool"))
        {
            DestroyMe();
        }

        if (other.gameObject.CompareTag("Magnet"))
        {
            Destroy(other.gameObject);
            _stackBrain.SetMagnet();
        }

        if (other.gameObject.CompareTag("Diamond"))
        {
            GameObject _effect = Instantiate(_stackBrain.CollectDiamondEffect, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(_effect, 2f);
        }
    }

    private void DestroyMe()
    {
        _stackBrain.CollectedList.Remove(gameObject);
        Destroy(gameObject);
    }
}
