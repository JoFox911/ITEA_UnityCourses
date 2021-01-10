using UnityEngine;


public class BotItemDetectionManager
{
    private PickUpHelper _pickUpHelper;

    public bool IsItemDetected => GrabableObject != null;
    public GameObject GrabableObject;

    public void Init(PickUpHelper pickUpHelper)
    {
        _pickUpHelper = pickUpHelper;
    }

    public void UpdateState()
    {
        if (_pickUpHelper != null && _pickUpHelper.CheckGrab())
        {
            GrabableObject = _pickUpHelper.GetPickUpObject();
        }
    }
}
