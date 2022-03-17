using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Picker.Managers;

namespace Picker.Level
{
    public class EndLevelCalculator : MonoBehaviour
    {
        [Tooltip("The object that fixes the road when the level is clear.")]
        [SerializeField] private GameObject movablePiece;

        private int _objectAmountRequirement = 20;

        private List<GameObject> _collectedObjects = new();

        private LevelPiece _levelPiece;

        private const float CalculateTimer = 2.0f;

        private void Awake()
        {
            if (movablePiece == null) movablePiece = transform.GetChild(0).gameObject;
            _levelPiece = GetComponentInParent<LevelPiece>();
        }
        private void Start()
        {
            _objectAmountRequirement = _levelPiece.levelCompleteCount;
            _collectedObjects.Clear();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
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
            GameManager.instance.ChangeState(GameState.GameCheckingResults);
            yield return new WaitForSeconds(CalculateTimer);
            if (_collectedObjects.Count >= _objectAmountRequirement)
                LevelWon();
            else
                LevelLost();
        }
        private void LevelWon()
        {
            for (int i = 0; i < _collectedObjects.Count; i++)
            {
                PoolManager.instance.SpawnParticle(_collectedObjects[i].transform.position);
                PoolManager.instance.ReleaseAllPrefabPools();
                _collectedObjects[i].gameObject.SetActive(false);
            }
            LeanTween.move(movablePiece, new Vector3(movablePiece.transform.position.x, 0, movablePiece.transform.position.z), 1.5f).setEase(LeanTweenType.easeOutElastic);
            GameManager.instance.ChangeState(GameState.GameWon);
        }
        private void LevelLost()
        {
            GameManager.instance.ChangeState(GameState.GameLost);
        }
    }
}

