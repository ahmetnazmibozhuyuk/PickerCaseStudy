using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Picker.Managers
{
    public struct InstantiatedLevel
    {
        public InstantiatedLevel(GameObject levellGameObject, int levelNumber)
        {
            instantiatedLevelGameObject = levellGameObject;
            instantiatedLevelNumber = levelNumber;
        }
        public GameObject instantiatedLevelGameObject;
        public int instantiatedLevelNumber;
    }
    public class LevelManager : MonoBehaviour
    {
        public GameObject[] LevelPieces;

        [HideInInspector] public int CurrentLevel = 1;

        private Vector3 SpawnPosition;

        private List<InstantiatedLevel> _instantiatedLevel = new List<InstantiatedLevel>();

        private float _currentSpawnPoint = 0;

        private int _latestSpawnedLevel = 0;
        private int _spawnedLevelIndex;

        private const int _initialSpawnAmount = 3;
        private const int _levelLength = 120;

        private void Awake()
        {
            _currentSpawnPoint = 35;
            SpawnPosition = new Vector3(0, 0, 35);

            //PlayerPrefs.SetInt("currentLevel", 1);
        }
        public void InitializeGame()
        {
            EnablePieceByNumber(0);
            EnablePieceByNumber(1);
            EnablePieceByNumber(2);
        }
        public void EnablePiece()
        {
            if (_latestSpawnedLevel < LevelPieces.Length)
            {
                GameObject tempGO = (GameObject)Instantiate(LevelPieces[_latestSpawnedLevel], SpawnPosition, transform.rotation);
                _instantiatedLevel.Add(new InstantiatedLevel(tempGO, _latestSpawnedLevel));
                _currentSpawnPoint += _levelLength;
                SpawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
                _spawnedLevelIndex = _latestSpawnedLevel;
            }
            else
            {
                int temp = Random.Range(0, LevelPieces.Length);
                // To prevent same level spawning twice in a row.
                if (_spawnedLevelIndex == temp)
                {
                    temp++;
                    if (temp > LevelPieces.Length - 1)
                        temp = 0;
                }
                GameObject tempGO = (GameObject)Instantiate(LevelPieces[temp], SpawnPosition, transform.rotation);
                _instantiatedLevel.Add(new InstantiatedLevel(tempGO, temp));
                _currentSpawnPoint += _levelLength;
                SpawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);

                _spawnedLevelIndex = temp;

            }
            _latestSpawnedLevel++;

            PlayerPrefs.SetInt("latestSpawnedLevel", _latestSpawnedLevel);

            //ResetScenePosition();
            Debug.Log("Currently active pieces are: " + 
                _instantiatedLevel[0].instantiatedLevelGameObject + ", " +
                _instantiatedLevel[1].instantiatedLevelGameObject + ", "+
                _instantiatedLevel[2].instantiatedLevelGameObject);

        }
        private void EnablePieceByNumber(int levelIndex)
        {
            GameObject tempGO = (GameObject)Instantiate(LevelPieces[levelIndex], SpawnPosition, transform.rotation);
            _instantiatedLevel.Add(new InstantiatedLevel(tempGO, levelIndex));
            _currentSpawnPoint += _levelLength;
            SpawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
            _latestSpawnedLevel++;

            //ResetScenePosition();
        }
        public void DisableOldestPiece()
        {
            if (_instantiatedLevel.Count > _initialSpawnAmount)
            {
                Destroy(_instantiatedLevel[0].instantiatedLevelGameObject);
                _instantiatedLevel.RemoveAt(0);
            }
        }
        public void CurrentLevelFinished()
        {
            CurrentLevel++;
        }

        #region Save - Load - Restart Level
        public void SaveLevels()
        {
            PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);
            PlayerPrefs.SetInt("0", _instantiatedLevel[0].instantiatedLevelNumber);
            PlayerPrefs.SetInt("1", _instantiatedLevel[1].instantiatedLevelNumber);
            PlayerPrefs.SetInt("2", _instantiatedLevel[2].instantiatedLevelNumber);
        }
        public void LoadLevels()
        {
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
            _latestSpawnedLevel = CurrentLevel - 1;
            if (CurrentLevel == 1)
            {
                EnablePieceByNumber(0);
                EnablePieceByNumber(1);
                EnablePieceByNumber(2);
            }
            else
            {
                EnablePieceByNumber(PlayerPrefs.GetInt("0"));
                EnablePieceByNumber(PlayerPrefs.GetInt("1"));
                EnablePieceByNumber(PlayerPrefs.GetInt("2"));
            }
        }
        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        #endregion

        //private void ResetScenePosition()
        //{
        //    foreach (GameObject i in SceneManager.GetSceneAt(0).GetRootGameObjects())
        //    {
        //        i.transform.position = new Vector3(i.transform.position.x, i.transform.position.y, i.transform.position.z - 120);
        //    }
        //    //SpawnPosition = new Vector3(SpawnPosition.x, SpawnPosition.y, SpawnPosition.z-120);
        //    //currentSpawnPoint -= 120;
        //}
    }
}

