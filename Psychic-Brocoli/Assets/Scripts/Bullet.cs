using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    [HideInInspector]
    public Vector3 direction;
    public float speed;
    public float bulletSpread = 0;
	
    void Start()
    {
        direction = Quaternion.Euler(0, Random.Range(-bulletSpread, bulletSpread), 0) * direction;
    }

	// Update is called once per frame
	void Update () {
        transform.position += direction * speed * Time.deltaTime;
	}
}
