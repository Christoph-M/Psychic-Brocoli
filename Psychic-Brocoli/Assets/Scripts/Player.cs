using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public uint player;

	public float speed = 10.0f;

	private Rigidbody rb;

	private Vector3 startPos;

	private bool particlesCanHit = true;

    CharacterMotor motor;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> () as Rigidbody;

		startPos = this.transform.position;

        motor = GetComponent<CharacterMotor>();
	}

	void OnParticleCollision(GameObject other) {
		if (particlesCanHit && other.transform.root.GetComponent<Bullet> ().player != this.player) {
			StartCoroutine (this.ParticleImmunity ());
			this.Respawn ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		float hor = Input.GetAxisRaw ("HorizontalP" + player);
		float ver = Input.GetAxisRaw ("VerticalP" + player);
		float shootHor = Input.GetAxisRaw ("ShootHorizontalP" + player);
		float shootVer = Input.GetAxisRaw ("ShootVerticalP" + player);

		Vector3 movement = new Vector3 (hor, 0.0f, ver);
		Vector3 shoot = new Vector3 (shootHor, 0.0f, shootVer);

        motor.inputMoveDirection = movement;

        //rb.AddForce (movement * speed);

		if (shoot != Vector3.zero) {
			this.transform.rotation = Quaternion.LookRotation (shoot);

			//rb.angularVelocity = Vector3.zero;
		}
	}

	public void Respawn() {
		this.transform.position = startPos;

		Score score = FindObjectOfType<Score> ();

		switch (player) {
			case 1: score.IncScore (1); break;
			case 2: score.IncScore (0); break;
		}
	}

	IEnumerator ParticleImmunity() {
		particlesCanHit = false;

		yield return new WaitForSeconds (1.0f);

		particlesCanHit = true;
	}
}
