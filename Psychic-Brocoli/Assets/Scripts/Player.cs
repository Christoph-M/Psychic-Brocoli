using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public int player;

	public float speed = 10.0f;

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> () as Rigidbody;
	}
	
	// Update is called once per frame
	void Update () {
		float hor = Input.GetAxisRaw ("HorizontalP" + player);
		float ver = Input.GetAxisRaw ("VerticalP" + player);
		float shootHor = Input.GetAxisRaw ("ShootHorizontalP" + player);
		float shootVer = Input.GetAxisRaw ("ShootVerticalP" + player);

		Vector3 movement = new Vector3 (hor, 0.0f, ver);
		Vector3 shoot = new Vector3 (shootHor, 0.0f, shootVer);

		rb.AddForce (movement * speed);

		if (shoot != Vector3.zero) {
			this.transform.rotation = Quaternion.LookRotation (shoot);

			rb.angularVelocity = Vector3.zero;
		}
	}
}
