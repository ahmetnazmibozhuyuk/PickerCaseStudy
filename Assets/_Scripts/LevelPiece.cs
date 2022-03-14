using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelPiece : MonoBehaviour
{
    public int levelCompleteCount;

    private int _collectedAmount;

    [SerializeField] private TextMeshProUGUI objectText;

    // Start is called before the first frame update
    void Start()
    {
        objectText.SetText(_collectedAmount + " / " + levelCompleteCount);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateObjectCounter()
    {
        _collectedAmount++;
        objectText.SetText(_collectedAmount + " / " + levelCompleteCount);
    }
}
