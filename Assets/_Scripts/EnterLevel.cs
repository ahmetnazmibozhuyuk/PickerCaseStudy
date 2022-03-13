using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterLevel : MonoBehaviour
{
    private Collider _collider;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            Debug.Log(other.tag);
            LevelManager.Instance.EnablePiece();
            LevelManager.Instance.DisableOldestPiece();
            LevelManager.Instance.CurrentLevelFinished();
        }
    }
    //BU TRIGGERA GİRERKEN DEĞİL BUNDAN ÇIKARKEN BİR ÖNCEKİNİ TEMİZLE.
}
