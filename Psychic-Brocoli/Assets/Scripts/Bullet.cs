using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [HideInInspector]
    public Vector3 direction;
    public float speed;
    public float bulletSpread = 0;
    public bool tumble = false;
    public bool rotateZ = false;

    Vector3 tumbleVector;

	private bool canCollide = false;
	
    void Start()
    {
        direction = Quaternion.Euler(0, Random.Range(-bulletSpread, bulletSpread), 0) * direction;
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0,-90,0);
		StartCoroutine (this.StartInactive ());
        tumbleVector = new Vector3(Random.value * 360, Random.value * 360, Random.value * 360);
    }

	void OnTriggerEnter(Collider other) {
		Debug.Log ("Triggered: " + other.tag);
		if (other.tag == "Player" && canCollide) {
			other.GetComponent<Player> ().Respawn ();

			Destroy (this.gameObject);
		} else if (other.tag == "Wall") {
			Debug.Log ("Wall: " + other.tag);
			Destroy (this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
        if (tumble)
        {
            transform.rotation *= Quaternion.Euler(tumbleVector * Time.deltaTime);
        }
        if (rotateZ)
        {
            transform.rotation *= Quaternion.Euler(0, 360 * Time.deltaTime, 0);
        }
	}

	IEnumerator StartInactive() {
		yield return new WaitForSeconds (0.1f);

		canCollide = true;
	}
}
