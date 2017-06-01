using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;


public class Gaze: MonoBehaviour
{
    [SerializeField] SteamVR_TrackedObject hmd;
	[SerializeField] float activationTime;
	[SerializeField] float maxSpeed;
	[SerializeField] float acceleration;

	float timeWaited;
	float speed=0f;
	LineRenderer rayCastLineRenderer;
	Renderer selectedRenderer;
	Anchor selectedAnchor;
	FlyingController flyingController;

	bool anchorFlag, movementFlag;

	Vector3 destinationPosition, gazeRayOrigin;
	const float RAY_ORIGIN_OFFSET = 2f;
	const float LINERENDERER_LENGTH = 100f;
	
	// Use this for initialization
	void Start () 
	{
		rayCastLineRenderer = GetComponent<LineRenderer>();
		timeWaited = 0;
		anchorFlag = false;
		movementFlag = false;
		flyingController = GetComponent<FlyingController> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		gazeRayOrigin = hmd.transform.position + hmd.transform.forward * RAY_ORIGIN_OFFSET;  
		Ray gazeRay = new Ray (hmd.transform.position, hmd.transform.forward);
		Debug.DrawRay (hmd.transform.position, hmd.transform.forward, Color.red);

		//Set line position
		rayCastLineRenderer.SetPosition (0, gazeRayOrigin);
		rayCastLineRenderer.SetPosition (1, (hmd.transform.forward*LINERENDERER_LENGTH));


		//Check raycast hit
		RaycastHit hit;
        if (Physics.Raycast(gazeRay, out hit))
        {
			Debug.Log ("Ray hit: " + hit.collider.gameObject.name);
			if (hit.collider.gameObject.tag == "Locomotion_Anchor" && !anchorFlag && !hit.collider.gameObject.GetComponent<Anchor>().GetActivationStatus()) 
			{
				anchorFlag = true;
				selectedAnchor = hit.collider.gameObject.GetComponent<Anchor>();
				timeWaited = 0;
				Debug.Log ("Anchor found");
			} 
			else if (hit.collider.gameObject.tag == "Locomotion_Anchor") 
			{
				timeWaited += Time.deltaTime;
				if (timeWaited >= activationTime && !selectedAnchor.GetActivationStatus()) 
				{
                    Debug.Log("Activating Anchor");
					selectedAnchor.Activate ();
				}
			}
            else 
            {
                anchorFlag = false;
                Debug.Log("Anchor lost");
                timeWaited = 0;
            }
        }
        else
        {
            Debug.Log("Raycast failed");
            anchorFlag = false;
            timeWaited = 0f;
        }


		/*//If anchor selected, keep track of time and chang color
		if (selectionFlag) 
		{
            Debug.Log("tracking time");
			timeWaited += Time.deltaTime;
			if (timeWaited >= activationTime) 
			{
				//break the gameObject
				destinationPosition = selectedGameObject.transform.position;
				selectedGameObject.GetComponent<Anchor>().BreakCube();
                Debug.Log("Activated!");

                //start movement
				movementFlag = true;
                //selectionFlag = false;

				//selectedRenderer =selectedGameObject.GetComponent<MeshRenderer> ();
				//selectedRenderer.material.color = Color.black;
			}
		}

		if (movementFlag) 
		{
            flyingController.MovePlayer(destinationPosition);

            if(!flyingController.IsFlying())
            {
                Debug.Log("Reached destination");
                movementFlag = false;
                flyingController.ResetSpeed();
            }
		}*/
		
	}

}
