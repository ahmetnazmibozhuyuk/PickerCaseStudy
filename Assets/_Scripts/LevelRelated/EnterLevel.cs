using UnityEngine;
using Picker.Managers;

namespace Picker.Level
{
    public class EnterLevel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                LevelManager.Instance.EnablePiece();
                LevelManager.Instance.DisableOldestPiece();
                LevelManager.Instance.CurrentLevelFinished();
                LevelManager.Instance.SaveLevels();
            }
        }
    }
}

