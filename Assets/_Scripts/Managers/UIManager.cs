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

        private float _gamePanelFadeTime = 0.3f;
        private float _gamePanelAlpha = 0.9f;

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
            LeanTween.alpha(gamePanel, 0, _gamePanelFadeTime);
            youWonTextObject.SetActive(false);
            tapToPlayObject.SetActive(false);
            tapToPlayTextObject.SetActive(false);
            youLostTextObject.SetActive(false);
            restartButtonObject.SetActive(false);
        }
        public void GameWon()
        {
            LeanTween.alpha(gamePanel, _gamePanelAlpha, _gamePanelFadeTime);
            youWonTextObject.SetActive(true);
            tapToPlayObject.SetActive(true);
            tapToPlayTextObject.SetActive(true);
        }
        public void GameLost()
        {
            LeanTween.alpha(gamePanel, _gamePanelAlpha, _gamePanelFadeTime);
            youLostTextObject.SetActive(true);
            restartButtonObject.SetActive(true);
        }
    }
}
