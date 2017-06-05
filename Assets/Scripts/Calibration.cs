using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{

    [SerializeField] private Camera head;
    [SerializeField] SteamVR_TrackedObject left_trackedObj;
    [SerializeField] SteamVR_TrackedObject right_trackedObj;
    [SerializeField] SteamVR_TrackedObject chair_trackedObj;

    private SteamVR_Controller.Device leftConroller_Device, rightController_Device, chair_Tracker;

    public Vector3 reclinedVec = Vector3.zero;
    public Quaternion reclinedRot = Quaternion.identity;
    public KeyboardController kc;
    public Tiltcontroller tc;
    private int state = 0;

    void Start()
    {
        kc = GetComponent<KeyboardController>();
        tc = GetComponent<Tiltcontroller>();
    }

    void Update() 
    {
        leftConroller_Device = SteamVR_Controller.Input((int)left_trackedObj.index);
        rightController_Device = SteamVR_Controller.Input((int)right_trackedObj.index);
        chair_Tracker = SteamVR_Controller.Input((int)chair_trackedObj.index);

        if (leftConroller_Device == null || rightController_Device == null)
        {
            Debug.LogError("Null controllers");
            return;
        }
        
        if (TriggerPressedUp())
        {
            switch(state)
            {
                case 0:
                    Debug.Log("Capture reclined.");
                    reclinedRot = Camera.main.transform.rotation;
                    reclinedVec = Camera.main.transform.forward;
                    state = 1;
                    break;
                case 1:
                    Debug.Log("Finished calibrations.");
                    kc.enabled = false;
                    tc.enabled = true;

                    transform.rotation = reclinedRot;
                    break;
            }
        }           
    }

    public Quaternion getCalibratedRotation()
    {
        return reclinedRot;
    }

    public Vector3 getCalibratedForward()
    {
        return reclinedVec;
    }

    private bool TriggerPressedUp()
    {
        return leftConroller_Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger) || rightController_Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger);
    }

}