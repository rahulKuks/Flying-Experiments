using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiltcontroller : MonoBehaviour {

    public Vector3 referenceForward = Vector3.zero;
    public Quaternion referenceRot = Quaternion.identity;

    private KeyboardController kc;
    private Calibration c;

    [SerializeField] private float angle = 0f;
    [SerializeField] private float threshold = 5f;

    void Start () {
        c = GetComponent<Calibration>();
        referenceRot = c.getCalibratedRotation();
        referenceForward = c.getCalibratedForward();
        c.enabled = false;
        kc = GetComponent<KeyboardController>();
        kc.enabled = false;
        InvokeRepeating("PrintAngle", 2.0f, 1.0f);
    }

    void Update () {
        angle = Quaternion.Angle(referenceRot, Camera.main.transform.rotation);
        Vector3 cross = Vector3.Cross(referenceForward, Camera.main.transform.forward);
        if (cross.z < 0) angle = -angle;
        if (Mathf.Abs(angle) > threshold)
        {
            transform.position += (angle / 10) * Time.deltaTime * Camera.main.transform.forward;
        }
    }

    private void PrintAngle()
    {
        Debug.Log(angle);
    }
}