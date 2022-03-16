﻿using UnityEngine;

namespace Picker.Managers
{
    [RequireComponent(typeof(LevelManager),typeof(UIManager))]
    public class GameManager : Singleton<GameManager>
    {
        public GameState CurrentState { get; private set; }
        private LevelManager _levelManager;
        private UIManager _uiManager;

        private AudioSource _audioSource;
        protected override void Awake()
        {
            base.Awake();
            _audioSource = GetComponent<AudioSource>();
            _levelManager = GetComponent<LevelManager>();
            _uiManager = GetComponent<UIManager>();
        }
        private void Start()
        {
            ChangeState(GameState.GameAwaitingStart);
            _uiManager.SetCurrentLevel(_levelManager.CurrentLevel);
        }
        public void PlaySound()
        {
            _audioSource.Play();
        }
        #region GAME STATES AND STATE MACHINE
        public void ChangeState(GameState newState)
        {
            if (CurrentState == newState) return;

            CurrentState = newState;
            switch (newState)
            {
                case GameState.GameAwaitingStart:
                    GameAwaitingStartState();
                    break;
                case GameState.GameStarted:
                    GameStartedState();
                    break;
                case GameState.GameCheckingResults:
                    GameCheckingResultsState();
                    break;
                case GameState.GameWon:
                    GameWonState();
                    break;
                case GameState.GameLost:
                    GameLostState();
                    break;
                default:
                    Debug.LogError("STATE NOT FOUND");
                    break;
            }
        }
        private void GameAwaitingStartState()
        {
            _uiManager.GameAwaitingStart();
            _levelManager.LoadLevels();
        }
        private void GameStartedState()
        {
            _uiManager.GameStarted(); 
        }
        private void GameCheckingResultsState()
        {
            Debug.Log("game checking results");
        }
        private void GameWonState()
        {
            _uiManager.GameWon();
        }
        private void GameLostState()
        {
            _uiManager.GameLost();
        }
        #endregion

        #region LEVEL RELATED
        public void RestartLevel()
        {
            _levelManager.RestartLevel();
        }
        public void EnterNewLevel()
        {
            _levelManager.EnablePiece();
            _levelManager.DisableOldestPiece();
            _levelManager.CurrentLevelFinished();
            _levelManager.SaveLevels();
            _uiManager.SetCurrentLevel(_levelManager.CurrentLevel);
        }
        #endregion
    }
    public enum GameState
    {
        GamePreStart = 0,
        GameAwaitingStart = 1,
        GameStarted = 2,
        GameCheckingResults = 3,
        GameWon = 4,
        GameLost = 5,
    }

}
