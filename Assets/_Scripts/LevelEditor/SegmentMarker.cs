using Picker.PlayerControl;
using UnityEngine;
public class SegmentMarker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Controller>())
            Destroy(transform.parent.gameObject);
    }
}


//segment.transform.position = _currentPosition;
//segment.AddComponent<Segment>();
//segment.GetComponent<Segment>().trackController = this;