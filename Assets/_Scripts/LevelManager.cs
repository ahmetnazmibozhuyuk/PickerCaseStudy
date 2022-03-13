using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    //@TODO: INITIALIZE YAPARKEN YANI 0DAN BAŞLARKEN ARIZALAR VAR, ÜZERİNE DÜŞ!


    public Vector3 SpawnOrigin;

    public Vector3 SpawnPosition;

    //[SerializeField]private List<GameObject> instantiatedLevels;

    [SerializeField] private List<GameObject> instantiatedLevels = new List<GameObject>();
    [SerializeField] private List<int> instantiatedLevelNumbers = new List<int>();



    private List<InstantiatedLevel> instantiatedLevel = new List<InstantiatedLevel>();
    //@todo: bu ikisini birleştir


    private float currentSpawnPoint;

    [SerializeField] private int currentLevel = 1;


    private int latestSpawnedLevel = 0; // @todo: + ile eklemek yerine son çıkan bölümün indexi olsun; aynı bölümün arka arkaya çıkmasına da engel olur.


    private int _initialSpawnAmount = 3;



    //spesifik bir bölüm açmak için method yaz, normal methodu ayarla uygun isimlendir


    protected override void Awake()
    {
        base.Awake();

        //PlayerPrefs.SetInt("currentLevel", 1);
        //latestSpawnedLevel = 0;


    }
    private void Start()
    {
        //InitializeGame();

    }
    public void InitializeGame()
    {
        //latestSpawnedLevel = PlayerPrefs.GetInt("latestSpawnedLevel");
        //if (PlayerPrefs.GetInt("0") != 0)
        //{

        //}
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
            currentSpawnPoint += 120;// LevelPieces[latestSpawnedLevel].LevelPieceSizeZ;
            SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
        }
        else
        {
            int temp = Random.Range(0, LevelPieces.Length);

            GameObject tempGO = (GameObject)Instantiate(LevelPieces[temp], SpawnPosition, transform.rotation);
            instantiatedLevels.Add(tempGO);
            instantiatedLevelNumbers.Add(temp);

            instantiatedLevel.Add(new InstantiatedLevel(tempGO, temp));

            currentSpawnPoint += 120; //LevelPieces[temp].LevelPieceSizeZ;
            SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
        }
        latestSpawnedLevel++;
        PlayerPrefs.SetInt("latestSpawnedLevel", latestSpawnedLevel);

    }
    public void EnablePieceByNumber(int levelIndex)
    {
        GameObject tempGO = (GameObject)Instantiate(LevelPieces[levelIndex], SpawnPosition, transform.rotation);
        instantiatedLevels.Add(tempGO);
        instantiatedLevelNumbers.Add(levelIndex);

        instantiatedLevel.Add(new InstantiatedLevel(tempGO, levelIndex));

        currentSpawnPoint += 120;// LevelPieces[latestSpawnedLevel].LevelPieceSizeZ;
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

            //instantiatedLevel.RemoveAt(0);



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

        //switch (currentLevel)
        //{
        //    case 0:
        //        EnablePieceByNumber(0);
        //        EnablePieceByNumber(1);
        //        EnablePieceByNumber(2);
        //        break;
        //    case 1:
        //        EnablePieceByNumber(1);
        //        EnablePieceByNumber(2);
        //        EnablePieceByNumber(3);
        //        break;
        //    case 2:
        //        EnablePieceByNumber(1);
        //        EnablePieceByNumber(2);
        //        EnablePieceByNumber(3);
        //        break;
        //}
        if(currentLevel == 1)
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


    //private void InstentiatePieces()
    //{
    //    GameObject temp;
    //    for(int i = 0; i < LevelPieces.Length; i++)
    //    {
    //        temp = (GameObject)Instantiate(LevelPieces[i].LevelPiecePrefab, SpawnPosition, transform.rotation);
    //        instantiatedLevels.Add(temp);
    //    }
    //}

}
