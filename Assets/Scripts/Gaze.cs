using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;


public class Gaze: MonoBehaviour
{
	Transform hmdTransform;
    [SerializeField] SteamVR_TrackedObject hmd;
	
	// Use this for initialization
	void Start () 
	{
        hmdTransform = null;
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(hmdTransform == null)
        {
            Debug.Log("hmd transform is null");
            SteamVR_TrackedObject[] trackedObjects = FindObjectsOfType<SteamVR_TrackedObject>();
            Debug.Log(trackedObjects.ToString());
            foreach (SteamVR_TrackedObject myObject in trackedObjects)
            {
                if (myObject.index == SteamVR_TrackedObject.EIndex.Hmd)
                {
                    hmdTransform = myObject.transform;
                    Debug.Log("hmd is: " + myObject.gameObject.name);
                    break;
                }
            }
        }
		//if (hmdTransform != null) 
		//{
			Ray gazeRay = new Ray (hmd.transform.position, hmd.transform.forward);
			Debug.DrawRay (hmd.transform.position, hmd.transform.forward, Color.red);

			RaycastHit hit;
			if (Physics.Raycast (gazeRay, out hit)) 
			{
				Debug.Log ("Raycast hit!");
				Debug.Log("Object: " + hit.collider.gameObject.name);
			}
		//}

		
	}

}
