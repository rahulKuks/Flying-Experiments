﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingController : MonoBehaviour
{
    [SerializeField] Camera head;
    [SerializeField] SteamVR_TrackedObject left_trackedObj;
    [SerializeField] SteamVR_TrackedObject right_trackedObj;
    [SerializeField] float maxSpeed;
	[SerializeField] float acceleration;


    SteamVR_Controller.Device leftConroller_Device, rightController_Device;
    Vector3 direction, leftDirection, rightDirection;
    bool flying;
	float speed = 0f;

    // Use this for initialization
    void Start ()
    {
        flying = false;
	}
	
	// Update is called once per frame
	/*void Update ()
    {
        leftConroller_Device = SteamVR_Controller.Input((int)left_trackedObj.index);
        rightController_Device = SteamVR_Controller.Input((int)right_trackedObj.index);

        if (leftConroller_Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) || rightController_Device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            flying = !flying;
        }

        if (flying)
        {
            leftDirection = left_trackedObj.transform.position - head.transform.position;
            rightDirection = right_trackedObj.transform.position - head.transform.position;
            direction = leftDirection + rightDirection;
            direction.Normalize();
            MovePlayer();
            }
    }*/

    /// <summary>
    /// Move player distance
    /// </summary>
	private void MovePlayer(Vector3 destination)
    {
		if (speed < maxSpeed) {
			speed = speed + acceleration * Time.fixedDeltaTime;
		} 
		else 
		{
			speed = speed - acceleration * Time.deltaTime;
		}

		direction = destination - head.transform.position;
		direction.Normalize ();

		transform.position = transform.position + speed * Time.deltaTime * direction;
    }
}
