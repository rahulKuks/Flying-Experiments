using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

	public float speed = 1.0f;

	void Update () {
		Vector3 position = this.transform.position;
		if (Input.GetKey(KeyCode.W)) 
		{
				position.z += speed;
				this.transform.position = position;
		}
		if (Input.GetKey(KeyCode.S)) 
		{
				position.z -= speed;
				this.transform.position = position;
		} 
		if (Input.GetKey(KeyCode.A)) 
		{
				position.x -= speed;
				this.transform.position = position;
		} 
		if (Input.GetKey(KeyCode.D)) 
		{
				position.x += speed;
				this.transform.position = position;
		} 
		if (Input.GetKey(KeyCode.Q)) 
		{
				position.y -= speed;
				this.transform.position = position;
		} 
		if (Input.GetKey(KeyCode.E)) 
		{
				position.y += speed;
				this.transform.position = position;
		}
	}
}
