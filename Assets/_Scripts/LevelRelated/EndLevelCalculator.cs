using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Picker.Managers;


namespace Picker.Level
{
    public class EndLevelCalculator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _collectedObjects = new List<GameObject>();

        [SerializeField] private int objectAmountRequirement = 20;
        [SerializeField] private GameObject movablePiece;

        private LevelPiece _levelPiece;

        private void Awake()
        {
            movablePiece = transform.GetChild(0).gameObject;
            _levelPiece = GetComponentInParent<LevelPiece>();


        }

        private void Start()
        {
            objectAmountRequirement = _levelPiece.levelCompleteCount;
            _collectedObjects.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Co_CheckResults());

            }
            else
            {
                _collectedObjects.Add(other.gameObject);
                _levelPiece.UpdateObjectCounter();
            }
        }


        private IEnumerator Co_CheckResults()
        {
            GameManager.Instance.ChangeState(GameState.GameCheckingResults);

            yield return new WaitForSeconds(1);

            if (_collectedObjects.Count > objectAmountRequirement)
            {
                LevelWon();
            }
            else
            {
                LevelLost();
            }
        }
        private void LevelWon()
        {
            for (int i = 0; i < _collectedObjects.Count; i++)
            {
                PoolManager.Instance.SpawnParticle(_collectedObjects[i].transform.position);
                PoolManager.Instance.ReleaseAllPrefabPools();
                _collectedObjects[i].gameObject.SetActive(false);
            }
            LeanTween.move(movablePiece, new Vector3(movablePiece.transform.position.x, 0, movablePiece.transform.position.z), 1.5f).setEase(LeanTweenType.easeOutElastic);
            GameManager.Instance.ChangeState(GameState.GameWon);
        }
        private void LevelLost()
        {
            GameManager.Instance.ChangeState(GameState.GameLost);
            //UI aktive et, restart?
        }
    }
}

