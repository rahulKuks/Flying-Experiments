using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour 
{
	[SerializeField] Color32 activationColor;
	[SerializeField] FlyingController flyingController;

	Color32 originalColor;


	float power = 10.0f;
	Renderer anchorRenderer;
	bool activated;

	//8 positions
	private Vector3[] directions =
	{
		new Vector3(1, -1, 1),
		new Vector3(-1, -1, 1),
		new Vector3(-1, -1, -1),
		new Vector3(1, -1, -1),
		new Vector3(1, 1, 1),
		new Vector3(-1, 1, 1),
		new Vector3(-1, 1, -1),
		new Vector3(1, 1, -1)
	};

	// Use this for initialization
	void Start () 
	{
		anchorRenderer = GetComponent<MeshRenderer> ();
		originalColor = anchorRenderer.material.color;
		activated = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void BreakCube()
	{
		// if we are not too small
		if (transform.localScale.x > 0.05f)
		{
			Color color = new Color(Random.value, Random.value, Random.value);
			// break this cube into 8 parts
			for (int i = 0; i < 8; i++)
			{
				GameObject smallerCube = Instantiate(gameObject) as GameObject;
				smallerCube.transform.parent = transform.parent;
				smallerCube.transform.name = "broken anchor";
				smallerCube.transform.localScale = 0.5f * transform.localScale;
				smallerCube.transform.position = transform.TransformPoint(directions[i] / 4);
				smallerCube.tag = "Untagged";

				Rigidbody rb = smallerCube.GetComponent<Rigidbody> ();
				rb.isKinematic = false;
				rb.useGravity = true;
				rb.AddForce(power * Random.insideUnitSphere, ForceMode.VelocityChange);
				smallerCube.GetComponent<Renderer> ().material.color = color;
			}
			Destroy(gameObject);
		}
	}

	public void Activate()
	{
		activated = true;

		//change color
		anchorRenderer.material.color = activationColor;

        //Start movement
        flyingController.StartMovement(this.transform.position);
	}

	public bool GetActivationStatus()
	{
		return activated;
	}

	void OnTriggerEnter(Collider col)
	{
		FlyingController fc = col.gameObject.GetComponent<FlyingController> ();
		if (fc != null) 
		{
			activated = false;
			anchorRenderer.material.color = originalColor;
		}
	}
		
}
