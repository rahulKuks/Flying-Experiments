using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour 
{

	float power = 10.0f;

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
	void Start () {
		
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
}
