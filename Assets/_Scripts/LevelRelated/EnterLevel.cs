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
                GameManager.Instance.EnterNewLevel();
            }
        }
    }
}

