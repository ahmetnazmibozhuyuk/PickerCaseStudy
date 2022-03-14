using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiece : MonoBehaviour
{
    public int levelCompleteCount;

    private int _collectedAmount;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateObjectCounter()
    {
        _collectedAmount++;
        Debug.Log(_collectedAmount + " / " + levelCompleteCount);
    }
}
