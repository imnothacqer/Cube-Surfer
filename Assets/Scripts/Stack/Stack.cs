﻿using System;
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

        if (other.gameObject.CompareTag("Ground"))
        {
            _stackBrain.TrailEffect.SetActive(true);
            _stackBrain.TrailEffect.transform.position = transform.position + (Vector3.down * 0.2f);
            _stackBrain.TrailEffect.transform.parent = transform;
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _stackBrain.TrailEffect.GetComponent<TrailRenderer>().Clear();
            _stackBrain.TrailEffect.transform.parent = null;
            _stackBrain.TrailEffect.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pool"))
        {
            _stackBrain.TrailEffect.transform.parent = null;
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
