using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHelper : MonoBehaviour
{
    [SerializeField]
    private float _castRadius = 0.5f;

    private GameObject _grabableObject;

    private RaycastHit hit;

    public bool CheckGrab()
    {
        if (Physics.SphereCast(transform.position, _castRadius, transform.forward, out hit, _castRadius))
        {
            if (hit.transform.CompareTag("CanGrab")) 
            {
                _grabableObject = hit.transform.gameObject;
                return true;
            }
        }
        return false;
    }

    public GameObject PickUp()
    {
        return _grabableObject;
    }
}
