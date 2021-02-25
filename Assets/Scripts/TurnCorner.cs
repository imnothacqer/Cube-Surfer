using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TurnCorner : MonoBehaviour
{
    public Vector3 newRotation;
    
    private PlayerMovement _playerMovement;
    private bool isHitted;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Collected"))
        {
            if (isHitted)
            {
                return;
            }

            isHitted = true;
            StackBrain _stackBrain = other.gameObject.GetComponentInParent<StackBrain>();
            _playerMovement = _stackBrain.GetComponentInParent<PlayerMovement>();
            _playerMovement.canSlide = false;
            Transform playerTransform = _playerMovement.gameObject.transform;
            playerTransform.DOLocalRotate(newRotation, 1f);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _playerMovement.canSlide = true;
    }
}

