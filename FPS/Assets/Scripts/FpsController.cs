using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SoldierWeaponManager))]
[RequireComponent(typeof(PickUpHelper))]
public class FpsController : MonoBehaviour
{
    [SerializeField]
    private Camera _fpsCamera;

    private SoldierWeaponManager _soldierWeaponManager;
    private PickUpHelper _pickUpHelper;

    void Awake()
    {
        _soldierWeaponManager = gameObject.GetComponent<SoldierWeaponManager>();
        _pickUpHelper = gameObject.GetComponent<PickUpHelper>();

        _soldierWeaponManager.Initialize(_fpsCamera.gameObject);
    }

    private void FixedUpdate()
    {
        if (_pickUpHelper.CheckGrab())
        {
            var item = _pickUpHelper.PickUp();
            var weapon = item.GetComponent<GunLikeWeapon>();
            if (weapon)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _soldierWeaponManager.AddGunLikeWeapon(weapon, UpdateWeaponUI);
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            _soldierWeaponManager.Shoot();
        }
    }


    private void UpdateWeaponUI()
    {
        Debug.Log("UpdateWeaponUI");
    }





    



    //[Serializable]
    //private class FpsInput
    //{






    //    [Tooltip("The name of the virtual axis mapped to rotate the camera around the y axis."),
    //     SerializeField]
    //    private string rotateX = "Mouse X";

    //    [Tooltip("The name of the virtual axis mapped to rotate the camera around the x axis."),
    //     SerializeField]
    //    private string rotateY = "Mouse Y";

    //    [Tooltip("The name of the virtual axis mapped to move the character back and forth."),
    //     SerializeField]
    //    private string move = "Horizontal";

    //    [Tooltip("The name of the virtual axis mapped to move the character left and right."),
    //     SerializeField]
    //    private string strafe = "Vertical";

    //    [Tooltip("The name of the virtual button mapped to run."),
    //     SerializeField]
    //    private string run = "Fire3";

    //    [Tooltip("The name of the virtual button mapped to jump."),
    //     SerializeField]
    //    private string jump = "Jump";

    //    /// Returns the value of the virtual axis mapped to rotate the camera around the y axis.
    //    public float RotateX
    //    {
    //        get { return Input.GetAxisRaw(rotateX); }
    //    }

    //    /// Returns the value of the virtual axis mapped to rotate the camera around the x axis.        
    //    public float RotateY
    //    {
    //        get { return Input.GetAxisRaw(rotateY); }
    //    }

    //    /// Returns the value of the virtual axis mapped to move the character back and forth.        
    //    public float Move
    //    {
    //        get { return Input.GetAxisRaw(move); }
    //    }

    //    /// Returns the value of the virtual axis mapped to move the character left and right.         
    //    public float Strafe
    //    {
    //        get { return Input.GetAxisRaw(strafe); }
    //    }

    //    /// Returns true while the virtual button mapped to run is held down.          
    //    public bool Run
    //    {
    //        get { return Input.GetButton(run); }
    //    }

    //    /// Returns true during the frame the user pressed down the virtual button mapped to jump.          
    //    public bool Jump
    //    {
    //        get { return Input.GetButtonDown(jump); }
    //    }
    //}
}


