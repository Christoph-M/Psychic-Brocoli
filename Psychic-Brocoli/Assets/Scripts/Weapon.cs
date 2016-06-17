using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class Weapon : MonoBehaviour {

    public Mesh weaponMesh;
    public GameObject bulletPrefab;
    public KeyCode shootKey;

	// Use this for initialization
	void Start () {
        GetComponent<MeshFilter>().mesh = weaponMesh;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(shootKey))
        {
            GameObject temp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().direction = transform.forward;
        }
	}
}
