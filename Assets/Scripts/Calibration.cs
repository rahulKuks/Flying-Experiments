using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{

    [SerializeField] private Camera head;
    [SerializeField] SteamVR_TrackedObject left_trackedObj;
    [SerializeField] SteamVR_TrackedObject right_trackedObj;

    private SteamVR_Controller.Device leftConroller_Device, rightController_Device;

    public Transform normal, reclined;

  // Use this for initialization
    void Start()
    {
        leftConroller_Device = SteamVR_Controller.Input((int)left_trackedObj.index);
        rightController_Device = SteamVR_Controller.Input((int)right_trackedObj.index);
    }

    void Update() 
    {
        if (leftConroller_Device == null || rightController_Device == null)
        {
            Debug.LogError("Null controllers");
            return;
        }

        if (leftConroller_Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            normal = Camera.main.transform;
        }
        if (rightController_Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            reclined = Camera.main.transform;
        }
    }
}
