using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelCalculator : MonoBehaviour
{
    [SerializeField]private List<GameObject> __collectedObjects = new List<GameObject>();

    [SerializeField] private int objectAmountRequirement = 20;
    [SerializeField] private GameObject movablePiece;

    private void Start()
    {
        if(movablePiece == null)
        {
            movablePiece = transform.GetChild(0).gameObject;
        }
        Debug.Log("lossy scale = "+transform.lossyScale);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Co_CheckResults());

        }
        else
        {
            __collectedObjects.Add(other.gameObject);
            Debug.Log(__collectedObjects.Count);
        }
    }
    private IEnumerator Co_CheckResults()
    {
        GameManager.Instance.ChangeState(GameState.GameCheckingResults);

        yield return new WaitForSeconds(1);

        if (__collectedObjects.Count > objectAmountRequirement)
        {
            LevelWon();
            yield return new WaitForSeconds(1);
            GameManager.Instance.ChangeState(GameState.GameStarted);
        }
        else
        {
            LevelLost();
        }
    }
    private void LevelWon()
    {
        //platformu düzelt, objeleri particle effectle patlat
        for(int i = 0;i < __collectedObjects.Count; i++)
        {
            Destroy(__collectedObjects[i].gameObject);
            //__collectedObjects.RemoveAt(0);
        }
        LeanTween.move(movablePiece, new Vector3(movablePiece.transform.position.x, 0, movablePiece.transform.position.z), 1).setEase(LeanTweenType.easeOutQuad);
    }
    private void LevelLost()
    {
        GameManager.Instance.ChangeState(GameState.GameLost);
        //UI aktive et, restart?
    }
}
