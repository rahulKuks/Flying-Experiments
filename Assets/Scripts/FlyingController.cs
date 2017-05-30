using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    [SerializeField] Camera head;
    [SerializeField] SteamVR_TrackedObject left_trackedObj;
    [SerializeField] SteamVR_TrackedObject right_trackedObj;
    [SerializeField] float speed;

    SteamVR_Controller.Device leftConroller_Device, rightController_Device;
    Vector3 direction, leftDirection, rightDirection;
    bool flying;

    // Use this for initialization
    void Start ()
    {
        leftConroller_Device = SteamVR_Controller.Input((int)left_trackedObj.index);
        rightController_Device = SteamVR_Controller.Input((int)right_trackedObj.index);
        flying = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(leftConroller_Device.GetPress(SteamVR_Controller.ButtonMask.Trigger) ||rightController_Device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            flying = !flying;
        }

        if(flying)
        {
            leftDirection = left_trackedObj.transform.position - head.transform.position;
            rightDirection = right_trackedObj.transform.position - head.transform.position;
            direction = leftDirection + rightDirection;
            direction.Normalize();
        }
        
	}

    /// <summary>
    /// Move player distance
    /// </summary>
    private void MovePlayer()
    {
        float distance = Time.deltaTime * speed;
        transform.position = transform.position + (distance * direction);
    }
}
