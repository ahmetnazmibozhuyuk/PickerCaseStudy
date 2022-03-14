using System.Collections;
using System.Collections.Generic;
using UnityEngine;



// PARTICLE EFFECTLER İÇİN DE POOL KULLANABİLİRSİN; ARAŞTIR, DENE!


public class EndLevelCalculator : MonoBehaviour
{
    [SerializeField] private List<GameObject> __collectedObjects = new List<GameObject>();

    [SerializeField] private int _objectAmountRequirement = 20;
    [SerializeField] private GameObject movablePiece;

    private LevelPiece _levelPiece;

    private void Awake()
    {
        movablePiece = transform.GetChild(0).gameObject;
        _levelPiece = GetComponentInParent<LevelPiece>();
    }
    private void Start()
    {
        _objectAmountRequirement = _levelPiece.levelCompleteCount;
        __collectedObjects.Clear();
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
            _levelPiece.UpdateObjectCounter();
        }
    }
    private IEnumerator Co_CheckResults()
    {
        GameManager.Instance.ChangeState(GameState.GameCheckingResults);

        yield return new WaitForSeconds(1);

        if (__collectedObjects.Count > _objectAmountRequirement)
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
        // objeleri particle effectle patlat
        for (int i = 0; i < __collectedObjects.Count; i++)
        {
            // patlama particle at _collectedObjects[i].transform.position;
            Destroy(__collectedObjects[i].gameObject);
            //__collectedObjects.RemoveAt(0);
        }
        LeanTween.move(movablePiece, new Vector3(movablePiece.transform.position.x, 0, movablePiece.transform.position.z), 1).setEase(LeanTweenType.easeOutElastic);
    }
    private void LevelLost()
    {
        GameManager.Instance.ChangeState(GameState.GameLost);
        //UI aktive et, restart?
    }
}
