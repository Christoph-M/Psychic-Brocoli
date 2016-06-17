using UnityEngine;
using System.Collections;


public class MouseInput : MonoBehaviour 
{

	private Vector3 characterPosition;
	private Plane cursorPlane;
	private Camera mainCamera;
	private GameObject currentLevel;
	private Collider mouseCollider;
	private Vector3 cursorPlanePosition;


	public Vector3 cursorPosition;


	void Awake()
	{
		cursorPlanePosition = Vector3.zero;
		mainCamera = Camera.main;
		currentLevel = GameObject.FindWithTag("Level");
		mouseCollider = currentLevel.GetComponent<Collider>();

		cursorPlane = new Plane(Vector3.up ,cursorPlanePosition);
	}



	
	// Update is called once per frame
	void Update () 
	{
		FindCursorPosition();

	}


	//Could optimize the function. Right now the same function gets called 2 times with minor differences.
	//But it works.
	void FindCursorPosition()
	{
		Vector3 cursorScreenPosition = Input.mousePosition;

		cursorPosition = ScreenPointToWorldPointOnPlane(cursorPlane ,cursorScreenPosition  , mainCamera);

		Vector3 cursorOnColliderPosition = ScreenPointToWorldPointOnCollider(mouseCollider, cursorScreenPosition, mainCamera);

		float correctionAngle;





		correctionAngle = cursorOnColliderPosition.y - cursorPlanePosition.y;

		cursorPlanePosition.y += correctionAngle;

		cursorPlane.SetNormalAndPosition(Vector3.up, cursorPlanePosition);

		TurnTowardsCursor(cursorPosition);






		/*
		if(Input.GetKey(KeyCode.R))
		{
			Debug.Log(cursorPosition);
		}
		*/
	
	}


	void TurnTowardsCursor(Vector3 cursorPos)
	{
		float speed = 20f;
		Vector3 targetDir = cursorPos - transform.position;
		speed *= Time.deltaTime;
		
		//constrain rotation so it doesn't turn up and down
		targetDir.y *= 0;
		
		//Handy that DrawRay
		Debug.DrawRay(transform.position, targetDir, Color.red);
		transform.rotation = Quaternion.LookRotation(targetDir);
	}




//Collider

	Vector3 ScreenPointToWorldPointOnCollider(Collider collider, Vector3 screenPoint, Camera camera)
	{
		Ray ray = camera.ScreenPointToRay(screenPoint);

		return ColliderRayIntersection(collider, ray);

	}
		                              
	Vector3 ColliderRayIntersection(Collider collider, Ray ray)
	{

		RaycastHit hitInfo;
		collider.Raycast(ray, out hitInfo, 300.0F);
		return ray.GetPoint (hitInfo.distance);
	}

//plane

	Vector3 ScreenPointToWorldPointOnPlane(Plane plane, Vector3 screenPoint, Camera camera)
	{
		
		Ray ray = camera.ScreenPointToRay(screenPoint);
		
		return PlaneRayIntersection (plane, ray);
	}

	Vector3 PlaneRayIntersection(Plane plane, Ray ray)
	{
		float dist;
		plane.Raycast (ray, out dist);
		return ray.GetPoint (dist);
	}

}
