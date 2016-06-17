using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshRenderer)), RequireComponent(typeof(MeshFilter))]
public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public string shootKey;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw(shootKey) == 1)
        {
            GameObject temp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().direction = transform.right;
            Destroy(this.gameObject);
        }
    }
}
