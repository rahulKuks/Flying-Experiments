using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;


public class Gaze: MonoBehaviour
{
    [SerializeField] SteamVR_TrackedObject hmd;
	[SerializeField] float length;
	[SerializeField] float activationTime;

	float timeWaited;
	LineRenderer rayCastLineRenderer;
	Renderer selectedRenderer;
	GameObject selectedGameObject;

	bool selectionFlag;

	
	// Use this for initialization
	void Start () 
	{
		rayCastLineRenderer = GetComponent<LineRenderer>();
		timeWaited = 0;
		selectionFlag = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
       
		Ray gazeRay = new Ray (hmd.transform.position, hmd.transform.forward);
		Debug.DrawRay (hmd.transform.position, hmd.transform.forward, Color.red);

		//Set line position
		rayCastLineRenderer.SetPosition (0, hmd.transform.position);
		rayCastLineRenderer.SetPosition (1, (hmd.transform.forward*length));


		//Check raycast hit
		RaycastHit hit;
		if (Physics.Raycast (gazeRay, out hit)) 
		{
			if (hit.collider.gameObject.tag == "Locomotion_Anchor" && !selectionFlag) 
			{
				selectionFlag = true;
			} 
			else 
			{
				selectionFlag = false;
				timeWaited = 0;
			}
		}


		//If anchor selected, keep track of time and chang color
		if (selectionFlag) 
		{
			timeWaited += Time.deltaTime;

			if (timeWaited >= activationTime) 
			{
				selectedRenderer = hit.collider.gameObject.GetComponent<Renderer> ();
				selectedRenderer.material.color = Color.black;
			}
		}
		
	}

}
