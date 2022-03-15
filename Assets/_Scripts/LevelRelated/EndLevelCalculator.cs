using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

using Picker.Managers;


namespace Picker.Level
{
    public class EndLevelCalculator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _collectedObjects = new List<GameObject>();

        [SerializeField] private int objectAmountRequirement = 20;
        [SerializeField] private GameObject movablePiece;

        [SerializeField] private GameObject particleEffect;

        [SerializeField] private ParticleSystem parSys;

        private LevelPiece _levelPiece;


        private ObjectPool<ParticleSystem> _particlePool;

        private Stack<ParticleSystem> particlePoolStack = new Stack<ParticleSystem>();


        private void Awake()
        {
            movablePiece = transform.GetChild(0).gameObject;
            _levelPiece = GetComponentInParent<LevelPiece>();

            _particlePool = new ObjectPool<ParticleSystem>(() =>
            {
                return Instantiate(parSys);
            }, parSys =>
            {
                parSys.gameObject.SetActive(true);
            }, parSys =>
            {
                parSys.gameObject.SetActive(false);
            }, parSys =>
             {
                 Destroy(parSys.gameObject);
             });
        }

        private void Start()
        {
            objectAmountRequirement = _levelPiece.levelCompleteCount;
            _collectedObjects.Clear();



            //StartCoroutine(Deneme2());
        }
        private IEnumerator Co_ClearParticleStack()
        {

            yield return new WaitForSeconds(2);
            for (int i = 0; i < particlePoolStack.Count; i++)
            {
                _particlePool.Release(particlePoolStack.Peek());
                particlePoolStack.Pop();
            }
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
            for (int i = 0; i < _collectedObjects.Count; i++)
            {
                PoolManager.Instance.SpawnParticle(_collectedObjects[i].transform.position);
                _collectedObjects[i].gameObject.SetActive(false);
                StartCoroutine(Co_ClearParticleStack());
            }
            LeanTween.move(movablePiece, new Vector3(movablePiece.transform.position.x, 0, movablePiece.transform.position.z), 2).setEase(LeanTweenType.easeOutElastic);
        }
        private void LevelLost()
        {
            GameManager.Instance.ChangeState(GameState.GameLost);
            //UI aktive et, restart?
        }
    }
}

