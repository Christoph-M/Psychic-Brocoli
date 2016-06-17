using UnityEngine;
using System.Collections;


[RequireComponent(typeof(CharacterMotor))]
public class CharacterInput : MonoBehaviour 
{
	[System.NonSerialized]
	public bool possessing;
	[System.NonSerialized]
	public GameObject possession;
	[System.NonSerialized]
	public bool equiped;

	CharacterMotor motor;
	Transform cam;


	void Awake()
	{
		motor = GetComponent<CharacterMotor>();
	}

	void Update()
	{
		//Calculate the direction normal based on input
		Vector3 movDir = new Vector3(Input.GetAxis("Horizontal"), 0 ,Input.GetAxis("Vertical"));
		
		//We multiply with the camera's rotation so our forward direction is the camera's forward direction.
		motor.inputMoveDirection = Camera.main.transform.rotation * movDir;

		motor.inputJump = Input.GetButton("Jump");
		
		//Here we put each function that gets used with button
		if(Input.GetButtonDown("Use"))
		{
			UseScript();
		}
		if(Input.GetButtonDown("Kick"))
		{
			DestroyScript();
		}
		if(Input.GetButtonDown("DropWeapon"))
		{
			DropWeapon();
		}
		if(Input.GetButtonDown("Fire1") && HasWeapon())
		{
			Fire();
		}
	}

	//All the functions that get put onto a button

	private void UseScript()
	{

		//if you're posessing something, drop it
		//don't need the raycast if we just want to cancel a possess
		if(possessing == true)
		{
			GameObject player = gameObject;
			//TODO Get rid of SendMessage functions
			possession.SendMessage("OnCancel", player);
			possession = null;
			possessing = false;
		}
		else
		{
			bool didHit;
			RaycastHit hitinfo;
			PhysicsRaycast(out didHit, out hitinfo);

			if(didHit == true)
			{
				possession = hitinfo.collider.gameObject;
				GameObject player = gameObject;
				//TODO Get rid of SendMessage functions
				possession.SendMessage("OnUse", player);
				possessing = true;
			}
		}
	}
	private void DestroyScript()
	{
		if(possessing != true)
		{
			bool didHit;
			RaycastHit hitinfo;
			PhysicsRaycast(out didHit, out hitinfo);


			if(didHit == true && hitinfo.collider.tag == "Box")
			{
				Destroy(hitinfo.collider.gameObject);
			}
		}
	}
	private void DropWeapon()
	{
		Transform tr = gameObject.transform.FindChild("WeaponPrototype");
		if(tr != null)
		{
			GameObject weapon = tr.gameObject;
			weapon.SendMessage("Drop");
		}
	}
	private void Fire()
	{
		//TODO Get rid of SendMessage functions
		gameObject.transform.FindChild("WeaponPrototype").gameObject.SendMessage("Fire");
	}





	void PhysicsRaycast(out bool didHit, out RaycastHit hitinfo)
	{
		//if we need we can also make the layermask an argument for the function
		int layerMask = 1 << 8;


		didHit = Physics.Raycast (transform.position - new Vector3 (0, 0.3f, 0), transform.forward, out hitinfo, 1.0f, layerMask);
	}

	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		//TODO use more tags
		
		//if we hit a weapon
		if(hit.normal.y < 0.9 && hit.gameObject.tag.Equals("Weapon"))
		{
			//TODO Get rid of SendMessage functions
			hit.gameObject.SendMessage("Pickup", gameObject);
		}
	}



	private bool HasWeapon()
	{
		return null != gameObject.transform.FindChild("WeaponPrototype");
	}
}
