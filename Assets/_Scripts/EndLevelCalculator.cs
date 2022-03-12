using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCalculator : MonoBehaviour
{
    private List<GameObject> __collectedObjects = new List<GameObject>();

    private void Start()
    {
        Debug.Log("lossy scale = "+transform.lossyScale);
    }
    private void OnTriggerEnter(Collider other)
    {
        __collectedObjects.Add(other.gameObject);
        Debug.Log(__collectedObjects.Count);
    }
}
