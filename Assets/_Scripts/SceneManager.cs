using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public GameObject[] LevelPieces;


    //@TODO: INITIALIZE YAPARKEN YANI 0DAN BAŞLARKEN ARIZALAR VAR, ÜZERİNE DÜŞ!


    public Vector3 SpawnOrigin;

    public Vector3 SpawnPosition;

    //[SerializeField]private List<GameObject> instantiatedLevels;

    [SerializeField]private List<GameObject> instantiatedLevels = new List<GameObject>();
    [SerializeField] private List<int> instantiatedLevelNumbers = new List<int>();

    private float currentSpawnPoint;

    [SerializeField]private int currentLevel = 1;


    private int latestSpawnedLevel = 0;

    private int previousSpawnedPiece;
    [SerializeField]private int currentPiece;
    [SerializeField]private int nextPiece;

    private int _initialSpawnAmount = 3;

    

    //spesifik bir bölüm açmak için method yaz, normal methodu ayarla uygun isimlendir


    protected override void Awake()
    {
        base.Awake();
        //CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        latestSpawnedLevel = PlayerPrefs.GetInt("latestSpawnedLevel");
        //latestSpawnedLevel = 0;
        Debug.Log(currentLevel);

    }
    private void Start()
    {
        if(PlayerPrefs.GetInt("1") != 0)
        {
            EnablePieceByNumber(PlayerPrefs.GetInt("0"));
            EnablePieceByNumber(PlayerPrefs.GetInt("1"));
            EnablePieceByNumber(PlayerPrefs.GetInt("2"));
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            EnablePiece(1);
        }
    }
    public void EnablePiece(int pieceSpawnAmount)
    {
        for(int i = 0; i < pieceSpawnAmount; i++)
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
                currentSpawnPoint += 120; //LevelPieces[temp].LevelPieceSizeZ;
                SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
            }
            latestSpawnedLevel++;
            PlayerPrefs.SetInt("latestSpawnedLevel", latestSpawnedLevel);
        }
    }
    public void EnablePieceByNumber(int levelIndex)
    {
        GameObject tempGO = (GameObject)Instantiate(LevelPieces[levelIndex], SpawnPosition, transform.rotation);
        instantiatedLevels.Add(tempGO);
        instantiatedLevelNumbers.Add(levelIndex);
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

            PlayerPrefs.SetInt("0", instantiatedLevelNumbers[0]);
            PlayerPrefs.SetInt("1", instantiatedLevelNumbers[1]);
            PlayerPrefs.SetInt("2", instantiatedLevelNumbers[2]);
        }
    }
    public void CurrentLevelFinished()
    {
        currentLevel++;
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
