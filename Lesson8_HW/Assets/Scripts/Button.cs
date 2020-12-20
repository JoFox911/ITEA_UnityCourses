using UnityEngine;

public class Button : MonoBehaviour
{    
    void OnCollisionEnter(Collision collision)
    {
        // react on collisions with any object
        GameEvents.ButtonPressedEvent();
    }
}
