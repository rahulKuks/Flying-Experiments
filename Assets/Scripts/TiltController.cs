using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiltController : MonoBehaviour
{

  public Quaternion referenceRot = Quaternion.identity;
  public KeyboardController kc;
  public Calibration calibration;

  public Vector3 euler = Vector3.zero;
  public float angle = 0f;

  void Start()
  {
      referenceRot = GetComponent<Calibration>().getCalibratedPosition();
      kc = GetComponent<KeyboardController>();
      kc.enabled = true;
      calibration = GetComponent<Calibration>();
      calibration.enabled = false;
  }

  void Update()
  {
      if (referenceRot != Quaternion.identity)
      {
          angle = Quaternion.Angle(referenceRot, transform.rotation);
      }
  }
}
