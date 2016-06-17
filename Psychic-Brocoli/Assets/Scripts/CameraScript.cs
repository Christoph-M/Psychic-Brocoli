using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

    public Transform p1;
    public Transform p2;
    Vector3 pointTo;

	// Update is called once per frame
	void Update () {
        pointTo = Vector3.Lerp(p1.transform.position, p2.transform.position, 0.5f);
        pointTo.y = Mathf.Clamp(Vector3.Distance(p1.transform.position, p2.transform.position) * 1.5f, 7, 30);

        transform.position = Vector3.Lerp(transform.position, pointTo, 0.1f);

        Debug.Log(pointTo);
	}
}
