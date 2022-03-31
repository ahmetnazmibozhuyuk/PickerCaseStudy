using UnityEngine;

namespace Picker
{
    public class Segment : MonoBehaviour
    {
        public TrackController trackController;
        private void OnDestroy()
        {
            if (trackController)
                trackController.LoadNextSegment();
        }
    }
}
