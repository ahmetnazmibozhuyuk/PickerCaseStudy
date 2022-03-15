using UnityEngine;
using TMPro;
namespace Picker.Level
{
    public class LevelPiece : MonoBehaviour
    {
        public int levelCompleteCount;

        private int _collectedAmount;

        [SerializeField] private TextMeshProUGUI objectText;

        void Start()
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

