using UnityEngine;
using TMPro;

namespace Picker.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private RectTransform gamePanel;

        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField] private GameObject tapToPlayTextObject;
        [SerializeField] private GameObject tapToPlayObject;

        [SerializeField] private GameObject youWonTextObject;

        [SerializeField] private GameObject youLostTextObject;
        [SerializeField] private GameObject restartButtonObject;

        public void SetCurrentLevel(int currentLevel)
        {
            levelText.SetText("Level: " + currentLevel);
        }
        public void GameAwaitingStart()
        {
            tapToPlayObject.SetActive(true);
            tapToPlayTextObject.SetActive(true);
        }
        public void GameStarted()
        {
            LeanTween.alpha(gamePanel, 0, 0.3f);
            youWonTextObject.SetActive(false);
            tapToPlayObject.SetActive(false);
            tapToPlayTextObject.SetActive(false);
            youLostTextObject.SetActive(false);
            restartButtonObject.SetActive(false);
        }
        public void GameWon()
        {
            LeanTween.alpha(gamePanel, 0.9f, 0.3f);
            youWonTextObject.SetActive(true);
            tapToPlayObject.SetActive(true);
            tapToPlayTextObject.SetActive(true);

        }
        public void GameLost()
        {
            LeanTween.alpha(gamePanel, 0.9f, 0.3f);
            youLostTextObject.SetActive(true);
            restartButtonObject.SetActive(true);
        }
    }
}
