using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitLevel : MonoBehaviour
{
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            Debug.Log(other.tag);
            SceneManager.Instance.DisableOldestPiece();
            GameManager.Instance.ChangeState(GameState.GameCheckingResults);
        }
    }
}
