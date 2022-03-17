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
        public InstantiatedLevel(GameObject levelGameObject, int levelNumber)
        {
            instantiatedLevelGameObject = levelGameObject;
            instantiatedLevelNumber = levelNumber;
        }
        public GameObject instantiatedLevelGameObject;
        public int instantiatedLevelNumber;
    }

    public class LevelManager : MonoBehaviour
    {
        [Tooltip("The list for individual level prefabs. Add created levels to this array.")]
        public GameObject[] levelPieces;

       [HideInInspector] public int currentLevel = 1;

        private Vector3 _spawnPosition;

        private List<InstantiatedLevel> _instantiatedLevel = new();

        private float _currentSpawnPoint = 0;

        private int _latestSpawnedLevel = 0;
        private int _spawnedLevelIndex;
        private int _amountOfLevelPrefabs;

        private const int InitialSpawnAmount = 3;
        private const int LevelLength = 120;

        private void Awake()
        {
            ResetGameOnLevelPieceChange();
            _currentSpawnPoint = 35;
            _spawnPosition = new Vector3(0, 0, 35);
        }
        /// <summary>
        ///  Starts the level from level 1 when the length of levelPieces changes. Remove if inconvenient.
        /// </summary>
        private void ResetGameOnLevelPieceChange()
        {
            _amountOfLevelPrefabs = levelPieces.Length;
            if (_amountOfLevelPrefabs < 3)
                throw new ArgumentException("The array length of levelPieces can't be less than 3.");
            if (_amountOfLevelPrefabs != PlayerPrefs.GetInt("_amountOfLevelPrefabs"))
            {
                PlayerPrefs.SetInt("_amountOfLevelPrefabs", _amountOfLevelPrefabs);
                PlayerPrefs.SetInt("currentLevel", 1); //To start level from 1.
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
            if (_latestSpawnedLevel < levelPieces.Length)
            {
                GameObject tempGO = (GameObject)Instantiate(levelPieces[_latestSpawnedLevel], _spawnPosition, transform.rotation);
                _instantiatedLevel.Add(new InstantiatedLevel(tempGO, _latestSpawnedLevel));
                _currentSpawnPoint += LevelLength;
                _spawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
                _spawnedLevelIndex = _latestSpawnedLevel;
            }
            else
            {
                int temp = UnityEngine.Random.Range(0, levelPieces.Length);
                // To prevent same level spawning twice in a row.
                if (_spawnedLevelIndex == temp)
                {
                    temp++;
                    if (temp > levelPieces.Length - 1)
                        temp = 0;
                }
                GameObject tempGO = (GameObject)Instantiate(levelPieces[temp], _spawnPosition, transform.rotation);
                _instantiatedLevel.Add(new InstantiatedLevel(tempGO, temp));
                _currentSpawnPoint += LevelLength;
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
            GameObject tempGO = (GameObject)Instantiate(levelPieces[levelIndex], _spawnPosition, transform.rotation);
            _instantiatedLevel.Add(new InstantiatedLevel(tempGO, levelIndex));
            _currentSpawnPoint += LevelLength;
            _spawnPosition = new Vector3(0, 0, transform.position.z + _currentSpawnPoint);
            _latestSpawnedLevel++;
        }
        public void DisableOldestPiece()
        {
            if (_instantiatedLevel.Count > InitialSpawnAmount)
            {
                Destroy(_instantiatedLevel[0].instantiatedLevelGameObject);
                _instantiatedLevel.RemoveAt(0);
            }
        }
        public void CurrentLevelFinished()
        {
            currentLevel++;
        }
        #region Save - Load - Restart Level
        public void SaveLevels()
        {
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            PlayerPrefs.SetInt("0", _instantiatedLevel[0].instantiatedLevelNumber);
            PlayerPrefs.SetInt("1", _instantiatedLevel[1].instantiatedLevelNumber);
            PlayerPrefs.SetInt("2", _instantiatedLevel[2].instantiatedLevelNumber);
        }
        public void LoadLevels()
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
            _latestSpawnedLevel = currentLevel - 1;
            if (currentLevel == 1)
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

