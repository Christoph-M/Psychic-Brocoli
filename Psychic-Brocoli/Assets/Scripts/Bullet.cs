using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [HideInInspector]
    public Vector3 direction;
    public float speed;
    public float bulletSpread = 0;

	private bool canCollide = false;
	
    void Start()
    {
        direction = Quaternion.Euler(0, Random.Range(-bulletSpread, bulletSpread), 0) * direction;

		StartCoroutine (this.StartInactive ());
    }

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && canCollide) {
			other.GetComponent<Player> ().Respawn ();

			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
	}

	IEnumerator StartInactive() {
		yield return new WaitForSeconds (0.1f);

		canCollide = true;
	}
}
