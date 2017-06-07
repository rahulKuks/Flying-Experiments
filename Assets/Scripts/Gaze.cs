using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Valve.VR;


public class Gaze: MonoBehaviour
{
    [SerializeField] SteamVR_TrackedObject hmd;
    [SerializeField] Camera eye;
	[SerializeField] float activationTime;
    [SerializeField] float rayLength;
    [SerializeField] float rayWidth;


    float timeWaited;
	LineRenderer rayCastLineRenderer;
	Renderer selectedRenderer;
	Anchor selectedAnchor;

	bool anchorFlag;

	Vector3 destinationPosition, gazeRayOrigin;
	const float RAY_ORIGIN_OFFSET = 2f;
	
	// Use this for initialization
	void Start () 
	{
		rayCastLineRenderer = GetComponent<LineRenderer>();
		timeWaited = 0;
		anchorFlag = false;

        
        rayCastLineRenderer.startWidth = rayWidth;
        rayCastLineRenderer.endWidth = rayWidth;
    }
	
	// Update is called once per frame
	void Update () 
	{
        /*gazeRayOrigin = hmd.transform.position + hmd.transform.forward * RAY_ORIGIN_OFFSET;  
        Ray gazeRay = new Ray (gazeRayOrigin, hmd.transform.forward);
		Debug.DrawRay (gazeRayOrigin, hmd.transform.forward, Color.red);

		//Set line position
		rayCastLineRenderer.SetPosition (0, gazeRayOrigin);
		rayCastLineRenderer.SetPosition (1, (hmd.transform.forward*rayLength));*/


		//Check raycast hit
		RaycastHit hit;
        //if (Physics.Raycast(gazeRay, out hit))
        if(ShootRay(eye.transform.position + eye.transform.forward*RAY_ORIGIN_OFFSET, out hit))
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
                    selectedAnchor.Activate(this.gameObject.GetComponent<FlyingController>());
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

        
	}

    private bool ShootRay(Vector3 rayOrigin, out RaycastHit hit)
    {
        Ray gazeRay = new Ray(rayOrigin, hmd.transform.forward);

        //Set line position
        rayCastLineRenderer.SetPosition(0, rayOrigin);
        rayCastLineRenderer.SetPosition(1, (eye.transform.position + eye.transform.forward * rayLength));

        if(Physics.Raycast(gazeRay, out hit))
        {
            return true;
        }
        return false;
    }

}
