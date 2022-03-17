using UnityEngine;
using TMPro;

namespace Picker.Level
{
    public class LevelPiece : MonoBehaviour
    {
        public int levelCompleteCount;

        [SerializeField] private TextMeshProUGUI objectText;

        private int _collectedAmount;
        private void Start()
        {
            objectText.SetText(_collectedAmount + " / " + levelCompleteCount);
        }
        public void UpdateObjectCounter()
        {
            _collectedAmount++;
            objectText.SetText(_collectedAmount + " / " + levelCompleteCount);
        }
    }
}

