using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    public GameObject[] LevelPieces;


    public Vector3 SpawnOrigin;

    public Vector3 SpawnPosition;

    //[SerializeField]private List<GameObject> instantiatedLevels;

    [SerializeField]private List<GameObject> instantiatedLevels = new List<GameObject>();

    private float currentSpawnPoint;

    private int currentLevel;

    public int CurrentLevel = 1;
    private int latestSpawnedLevel = 0;

    private int previousSpawnedPiece;
    private int currentPiece;
    private int nextPiece;
    protected override void Awake()
    {
        base.Awake();
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel");
        //latestSpawnedLevel = PlayerPrefs.GetInt("latestSpawnedLevel");
        latestSpawnedLevel = 0;
        Debug.Log(CurrentLevel);
    }
    private void Start()
    {
        EnablePiece(3);

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

                currentSpawnPoint += 120;// LevelPieces[latestSpawnedLevel].LevelPieceSizeZ;
                SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
            }
            else
            {
                int temp = Random.Range(0, LevelPieces.Length);

                GameObject tempGO = (GameObject)Instantiate(LevelPieces[temp], SpawnPosition, transform.rotation);
                instantiatedLevels.Add(tempGO);
                currentSpawnPoint += 120; //LevelPieces[temp].LevelPieceSizeZ;
                SpawnPosition = new Vector3(0, 0, transform.position.z + currentSpawnPoint);
            }
            latestSpawnedLevel++;
            PlayerPrefs.SetInt("latestSpawnedLevel", latestSpawnedLevel);
        }
    }
    public void DisableOldestPiece()
    {
        if (instantiatedLevels.Count > 3)
        {
            Destroy(instantiatedLevels[0]);
            instantiatedLevels.RemoveAt(0);
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
