using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour {

	public float speed = 2.5f;

	public float speedH = 2.0f;
  public float speedV = 2.0f;

  private float yaw = 0.0f;
  private float pitch = 0.0f;

	void Update () {
		// Vector3 position = this.transform.position;
		// if (Input.GetKey(KeyCode.W)) 
		// {
		// 		position.z += speed;
		// 		this.transform.position = position;
		// }
		// if (Input.GetKey(KeyCode.S)) 
		// {
		// 		position.z -= speed;
		// 		this.transform.position = position;
		// } 
		// if (Input.GetKey(KeyCode.A)) 
		// {
		// 		position.x -= speed;
		// 		this.transform.position = position;
		// } 
		// if (Input.GetKey(KeyCode.D)) 
		// {
		// 		position.x += speed;
		// 		this.transform.position = position;
		// } 
		// if (Input.GetKey(KeyCode.Q)) 
		// {
		// 		position.y -= speed;
		// 		this.transform.position = position;
		// } 
		// if (Input.GetKey(KeyCode.E)) 
		// {
		// 		position.y += speed;
		// 		this.transform.position = position;
		// }
		if (Input.GetKey(KeyCode.W)) 
		{
			transform.position += Camera.main.transform.forward * speed * Time.deltaTime;
		}

		// yaw += speedH * Input.GetAxis("Mouse X");
    pitch -= speedV * Input.GetAxis("Mouse Y");

    transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
