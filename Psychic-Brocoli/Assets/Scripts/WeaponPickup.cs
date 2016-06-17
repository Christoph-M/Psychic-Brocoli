using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponPickup : MonoBehaviour {

    public List<GameObject> weaponList = new List<GameObject>();

	void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject temp = (GameObject)Instantiate(weaponList[Random.Range(0, weaponList.Count - 1)], other.transform.position, other.transform.rotation);
            temp.GetComponent<Weapon>().shootKey = "Shoot"+ other.GetComponent<Player>().player.ToString();
        }
    }
}