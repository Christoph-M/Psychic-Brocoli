using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
	public uint player;

    // Update is called once per frame
    void Update()
    {
		if (Input.GetAxisRaw("Shoot" + player) == 1)
        {
            GameObject temp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().direction = transform.forward;
			temp.GetComponent<Bullet> ().player = this.player;
            Destroy(this.gameObject);
        }
    }
}
