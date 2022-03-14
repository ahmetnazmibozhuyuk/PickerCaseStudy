using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
public class LevelManager : Singleton<LevelManager>
{
    public GameObject[] LevelPieces;

    private Vector3 SpawnPosition;

    //[SerializeField]private List<GameObject> instantiatedLevels;

    [SerializeField] private List<GameObject> instantiatedLevels = new List<GameObject>();
    [SerializeField] private List<int> instantiatedLevelNumbers = new List<int>();

    private List<InstantiatedLevel> instantiatedLevel = new List<InstantiatedLevel>();
    //@todo: bu ikisini birleştir


    private float currentSpawnPoint = 0;

    [SerializeField] private int currentLevel = 1;


    private int latestSpawnedLevel = 0;
    private readonly int _initialSpawnAmount = 3;

    private int _spawnedLevelIndex;

    protected override void Awake()
    {
        base.Awake();
        currentSpawnPoint = 35;
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
        if (latestSpawnedLevel < LevelPieces.Length)
        {
            GameObject tempGO = (GameObject)Instantiate(LevelPieces[latestSpawnedLevel], SpawnPosition, transform.rotation);
            instantiatedLevels.Add(tempGO);
            instantiatedLevelNumbers.Add(latestSpawnedLevel);
            currentSpawnPoint += 120;// @TODO: MAGIC NUMBER KESİNLİKLE KALDIRMANIN BİR YOLUNU BUL
            SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
            _spawnedLevelIndex = latestSpawnedLevel;
        }
        else
        {
            int temp = Random.Range(0, LevelPieces.Length);
            // To prevent same level spawning twice.
            if (_spawnedLevelIndex == temp)
            {
                temp++;
                if (temp > LevelPieces.Length - 1) 
                    temp = 0;
            }

            GameObject tempGO = (GameObject)Instantiate(LevelPieces[temp], SpawnPosition, transform.rotation);
            instantiatedLevels.Add(tempGO);
            instantiatedLevelNumbers.Add(temp);

            instantiatedLevel.Add(new InstantiatedLevel(tempGO, temp));

            currentSpawnPoint += 120; //@TODO: MAGIC NUMBER KESİNLİKLE KALDIRMANIN BİR YOLUNU BUL
            SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);

            _spawnedLevelIndex = temp;

        }
        latestSpawnedLevel++;

        PlayerPrefs.SetInt("latestSpawnedLevel", latestSpawnedLevel);

        //ResetScenePosition();


    }
    private void EnablePieceByNumber(int levelIndex)
    {
        GameObject tempGO = (GameObject)Instantiate(LevelPieces[levelIndex], SpawnPosition, transform.rotation);
        instantiatedLevels.Add(tempGO);
        instantiatedLevelNumbers.Add(levelIndex);

        instantiatedLevel.Add(new InstantiatedLevel(tempGO, levelIndex));



        currentSpawnPoint += 120;//@TODO: MAGIC NUMBER KESİNLİKLE KALDIRMANIN BİR YOLUNU BUL
        SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);

        latestSpawnedLevel++;

        //ResetScenePosition();
    }
    public void DisableOldestPiece()
    {
        if (instantiatedLevels.Count > _initialSpawnAmount)
        {
            Destroy(instantiatedLevels[0]);
            instantiatedLevels.RemoveAt(0);
            instantiatedLevelNumbers.RemoveAt(0);
        }
    }
    public void CurrentLevelFinished()
    {
        currentLevel++;

    }
    public void SaveLevels()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetInt("0", instantiatedLevelNumbers[0]);
        PlayerPrefs.SetInt("1", instantiatedLevelNumbers[1]);
        PlayerPrefs.SetInt("2", instantiatedLevelNumbers[2]);
    }
    public void LoadLevels()
    {
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        latestSpawnedLevel = currentLevel - 1;
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
