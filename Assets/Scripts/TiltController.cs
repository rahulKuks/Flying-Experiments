using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{

  public Quaternion referenceRot = Quaternion.identity;
  public Vector3 referenceForward = Vector3.zero;
  public KeyboardController kc;
  public Calibration calibration;

  public Vector3 euler = Vector3.zero;
  public float angle = 0f;

  void Start()
  {
      calibration = GetComponent<Calibration>();
      referenceRot = calibration.getCalibratedRotation();
      referenceForward = calibration.getCalibratedForward();
      kc = GetComponent<KeyboardController>();
      kc.enabled = true;
      calibration = GetComponent<Calibration>();
      calibration.enabled = false;
  }

  void Update()
  {
        angle = Quaternion.Angle(referenceRot, transform.rotation);
        Vector3 cross = Vector3.Cross(referenceForward, transform.forward);
        if (cross.z < 0) angle = -angle;
  }
}
