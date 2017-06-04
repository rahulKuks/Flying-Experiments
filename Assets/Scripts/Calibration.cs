using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calibration : MonoBehaviour
{

  [SerializeField] SteamVR_TrackedObject left_trackedObj;
  [SerializeField] SteamVR_TrackedObject right_trackedObj;

  private SteamVR_Controller.Device leftConroller_Device, rightController_Device;

  public Vector3 reclinedVector;
  public Quaternion reclinedRot;
  private Quaternion normal;
  public KeyboardController kc;
  public TiltController tc;

  void Start()
  {
        kc = GetComponent<KeyboardController>();
        tc = GetComponent<TiltController>();
  }

  void Update()
  {
    leftConroller_Device = SteamVR_Controller.Input((int)left_trackedObj.index);
    rightController_Device = SteamVR_Controller.Input((int)right_trackedObj.index);

    if (leftConroller_Device == null || rightController_Device == null)
    {
      Debug.LogError("Null controllers");
      return;
    }

    // if (leftConroller_Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
    if (Input.GetKeyDown(KeyCode.Alpha1))
    {
      Debug.Log("Capture normal");
      normal = Camera.main.transform.rotation;
    }
    // if (rightController_Device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
    if (Input.GetKeyDown(KeyCode.Alpha2))
    {
      Debug.Log("Capture reclined");
      reclinedRot = transform.rotation;
      reclinedVector = transform.forward;
    }

    if (Input.GetKeyDown(KeyCode.Alpha3))
    {
        Debug.Log("Finish calibrations");
        kc.enabled = false;
        tc.enabled = true;
    }
  }

  public Quaternion getCalibratedRotation()
  {
      return reclinedRot;
  }

  public Vector3 getCalibratedForward()
  {
      return reclinedVector;
  }
}
