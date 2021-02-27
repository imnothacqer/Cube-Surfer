using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class StackBrain : MonoBehaviour
{
    [Header("General Settings")]
    public PlayerBrain playerBrain;
    public float stepSize;

    [Header("Camera Group Referance")] 
    public CinemachineTargetGroup _targetGroup;
    
    [Header("Referances")]
    public GameObject CollectedStackPrefab;
    public GameObject CollectStackEffect;
    public GameObject CollectDiamondEffect;

    [Header("Magnet Settings")]
    public bool isHaveMagnet;
    public float magnetDuration;
    public float magnetRange;
    private GameObject magnetStack;
    public GameObject MagnetPrefab;

    public List<GameObject> CollectedList;

    private void Start()
    {
        playerBrain = GetComponentInParent<PlayerBrain>();
    }

    private void Update()
    {
        magnetStack = CollectedList[0];
        if (isHaveMagnet)
        {
            Collider[] _inRange = Physics.OverlapSphere(magnetStack.transform.position, magnetRange);

            foreach (Collider _collectable in _inRange)
            {
                if (_collectable.gameObject.CompareTag("Collectable"))
                {
                    _collectable.gameObject.tag = "Untagged";
                    
                    _collectable.transform.DOMove(magnetStack.transform.position, 0.5f).OnComplete(() =>
                    {
                        CollectStack(_collectable.gameObject);
                    });
                }
            }
        }
    }

    public Vector3 GetSpawnPoint()
    {
        return playerBrain.PlayerBodyTransform.position + (Vector3.down * stepSize);
    }
    
    public void SetMagnet()
    {
        if (isHaveMagnet)
        {
            return;
        }
        StartCoroutine(MagnetTimer());
        GameObject _magnet = Instantiate(MagnetPrefab);
        _magnet.transform.position = magnetStack.transform.position + (magnetStack.transform.forward * -1f);
        _magnet.transform.parent = magnetStack.transform;
        Destroy(_magnet, magnetDuration);
    }

    private IEnumerator MagnetTimer()
    {
        isHaveMagnet = true;
        yield return new WaitForSeconds(magnetDuration);
        isHaveMagnet = false;
    }

    private void CollectStack(GameObject _collectable)
    {
        _collectable.gameObject.tag = "Untagged";
        playerBrain.OnCollectStack();
        GameObject _effect = Instantiate(
            CollectStackEffect,
            _collectable.transform.position,
            Quaternion.identity
        );
        Destroy(_effect, 2f);
            
        Destroy(_collectable.gameObject);
            
        GameObject _collected = Instantiate(CollectedStackPrefab);
        _collected.transform.position = GetSpawnPoint();
        _collected.transform.parent = transform;
        CollectedList.Add(_collected);
    }

    public bool IsHaveStack()
    {
        if (CollectedList.Count > 0)
        {
            return true;
        }
        return false;
    }
}
