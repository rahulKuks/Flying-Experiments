using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class Gaze: MonoBehaviour
{
	Transform hmdTransform;

	
	// Use this for initialization
	void Start () 
	{
		SteamVR_TrackedObject[] trackedObjects = FindObjectsOfType<SteamVR_TrackedObject>();
		foreach (SteamVR_TrackedObject myObject in trackedObjects)
		{
			if (myObject.index == SteamVR_TrackedObject.EIndex.Hmd)
			{
				hmdTransform = myObject.transform;
				Debug.Log (myObject.gameObject.name);
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (hmdTransform != null) 
		{
			Ray gazeRay = new Ray (hmdTransform.position, hmdTransform.forward);
			Debug.DrawRay (hmdTransform.position, hmdTransform.forward, Color.red);

			RaycastHit hit;
			if (Physics.Raycast (gazeRay, out hit)) 
			{
				Debug.Log ("Raycast hit!");
				Debug.Log("Object: " + hit.collider.gameObject.name);
			}
		}

		
	}

}
