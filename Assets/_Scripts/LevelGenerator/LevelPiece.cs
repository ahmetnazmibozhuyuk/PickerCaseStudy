using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Level Piece Data")]
public class LevelPiece : ScriptableObject
{
    public int LevelPieceNumber;

    public Vector2 LevelPieceSize = new Vector2(5,25);
    public GameObject[] LevelPiecePrefabs;
}
