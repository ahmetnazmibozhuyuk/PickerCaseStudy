using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Picker.Managers
{
    /// <summary>
    ///  Holds prefab and index data of instantiated levels.
    /// </summary>
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
        [Tooltip("The list for individual level prefabs. Add created levels to this array.")]
        public GameObject[] LevelPieces;

        [HideInInspector] public int CurrentLevel = 1;

        private Vector3 _spawnPosition;

        private List<InstantiatedLevel> _instantiatedLevel = new();

        private float _currentSpawnPoint = 0;

        private int _latestSpawnedLevel = 0;
        private int _spawnedLevelIndex;
        private int _amountOfLevelPrefabs;

        private const int _initialSpawnAmount = 3;
        private const int _levelLength = 120;

        private void Awake()
        {
            ResetGameOnLevelPieceChange();
            _currentSpawnPoint = 35;
            _spawnPosition = new Vector3(0, 0, 35);
        }
        /// <summary>
        ///  Starts the level from level 1 when the length of LevelPieces changes. Remove if inconvenient.
        /// </summary>
        private void ResetGameOnLevelPieceChange()
        {
            _amountOfLevelPrefabs = LevelPieces.Length;
            if (_amountOfLevelPrefabs < 3)
                throw new ArgumentException("The array length of LevelPieces can't be less than 3.");
            if (_amountOfLevelPrefabs != PlayerPrefs.GetInt("_amountOfLevelPrefabs"))
            {
                PlayerPrefs.SetInt("_amountOfLevelPrefabs", _amountOfLevelPrefabs);
                PlayerPrefs.SetInt("CurrentLevel", 1); //To start level from 1.
            }
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
                GameObject tempGO = (GameObject)Instantiate(LevelPieces[_latestSpawnedLevel], _spawnPosition, transform.rotation);
                _instantiatedLevel.Add(new InstantiatedLevel(tempGO, _latestSpawnedLevel));
                _currentSpawnPoint += _levelLength;
                _spawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
                _spawnedLevelIndex = _latestSpawnedLevel;
            }
            else
            {
                int temp = UnityEngine.Random.Range(0, LevelPieces.Length);
                // To prevent same level spawning twice in a row.
                if (_spawnedLevelIndex == temp)
                {
                    temp++;
                    if (temp > LevelPieces.Length - 1)
                        temp = 0;
                }
                GameObject tempGO = (GameObject)Instantiate(LevelPieces[temp], _spawnPosition, transform.rotation);
                _instantiatedLevel.Add(new InstantiatedLevel(tempGO, temp));
                _currentSpawnPoint += _levelLength;
                _spawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
                _spawnedLevelIndex = temp;
            }
            _latestSpawnedLevel++;
            PlayerPrefs.SetInt("latestSpawnedLevel", _latestSpawnedLevel);
            Debug.Log("Currently active pieces are: " +
                _instantiatedLevel[0].instantiatedLevelGameObject + ", " +
                _instantiatedLevel[1].instantiatedLevelGameObject + ", " +
                _instantiatedLevel[2].instantiatedLevelGameObject);
        }
        private void EnablePieceByNumber(int levelIndex)
        {
            GameObject tempGO = (GameObject)Instantiate(LevelPieces[levelIndex], _spawnPosition, transform.rotation);
            _instantiatedLevel.Add(new InstantiatedLevel(tempGO, levelIndex));
            _currentSpawnPoint += _levelLength;
            _spawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
            _latestSpawnedLevel++;
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
    }
}

