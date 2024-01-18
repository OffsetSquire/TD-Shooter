using UnityEngine;

public class MaskController : MonoBehaviour
{
    public Transform flashlight;

    void Update()
    {
        // Set the position of the mask to match the position of the flashlight
        transform.position = flashlight.position;
    }
}
