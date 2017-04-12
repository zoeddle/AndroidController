using UnityEngine;
using System.Collections;

public class CharacterInput : MonoBehaviour {

	CharacterController characterController;

	bool jump = false;
	float movement = 0;
	float rotation = 0;

	public GameObject bulletPrefab;
	public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		characterController = gameObject.GetComponent<CharacterController> ();
	}

	public void UpdateMovement(float forward, float sideways)
	{
		movement = forward;
		rotation = sideways;
	}

	public void Jump()
	{
		Debug.Log ("Jump!");
		jump = true;
	}

	public void Fire()
	{
		Debug.Log ("Fire!");
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 20;

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f);  
	}
		
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (0, rotation * 1f, 0));
		if (jump) {
			jump = false;
			Vector3 moveDirection = Vector3.zero;
			moveDirection.y -= 20f * Time.deltaTime;
			//characterController.Move(moveDirection * Time.deltaTime);
			if(characterController.isGrounded)
			{
				characterController.Move (Vector3.up * 5f);
			}
			return;
		}
		else
		{
			Vector3 forward = transform.TransformDirection(Vector3.forward);
			characterController.SimpleMove (forward * movement * 15f);		
		}
	}
}
