using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public float moveSpeed;

	void Update () {
		transform.position = new Vector3(transform.position.x + Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, 
											transform.position.y + Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime,
											transform.position.z);	
	}
}
