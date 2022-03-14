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


    private float currentSpawnPoint;

    [SerializeField] private int currentLevel = 1;


    private int latestSpawnedLevel = 0; // @todo: + ile eklemek yerine son çıkan bölümün indexi olsun; aynı bölümün arka arkaya çıkmasına da engel olur.
    private readonly int _initialSpawnAmount = 3;

    protected override void Awake()
    {
        base.Awake();
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
        }
        else
        {
            int temp = Random.Range(0, LevelPieces.Length);

            GameObject tempGO = (GameObject)Instantiate(LevelPieces[temp], SpawnPosition, transform.rotation);
            instantiatedLevels.Add(tempGO);
            instantiatedLevelNumbers.Add(temp);

            instantiatedLevel.Add(new InstantiatedLevel(tempGO, temp));

            currentSpawnPoint += 120; //@TODO: MAGIC NUMBER KESİNLİKLE KALDIRMANIN BİR YOLUNU BUL
            SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
        }
        latestSpawnedLevel++;
        PlayerPrefs.SetInt("latestSpawnedLevel", latestSpawnedLevel);
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
    private void ResetScenePosition()
    {
        foreach (GameObject j in SceneManager.GetSceneAt(0).GetRootGameObjects())
        {
            j.transform.position = new Vector3(j.transform.position.x, j.transform.position.y, j.transform.position.z - 120);
        }
        //currentSpawnPoint -= 120;
    }
}
