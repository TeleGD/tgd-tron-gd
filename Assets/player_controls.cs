using UnityEngine;
using System.Collections;

public class player_controls : MonoBehaviour {

	public CharacterController contr;
	Vector3 impulsion_force = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveDir = Vector3.zero;
		if (Input.GetKey (KeyCode.Keypad8))
			moveDir = new Vector3 (0, 0, 1);
		if (Input.GetKey (KeyCode.Keypad9))
			moveDir = new Vector3 (1, 0, 1);
		if (Input.GetKey (KeyCode.Keypad6))
			moveDir = new Vector3 (1, 0, 0);
		if (Input.GetKey (KeyCode.Keypad3))
			moveDir = new Vector3 (1, 0, -1);
		if (Input.GetKey (KeyCode.Keypad2))
			moveDir = new Vector3 (0, 0, -1);
		if (Input.GetKey (KeyCode.Keypad1))
			moveDir = new Vector3 (-1, 0, -1);
		if (Input.GetKey (KeyCode.Keypad4))
			moveDir = new Vector3 (-1, 0, 0);
		if (Input.GetKey (KeyCode.Keypad7))
			moveDir = new Vector3 (-1, 0, 1);

		contr.Move ((moveDir.normalized *3 + impulsion_force) *Time.deltaTime);
		impulsion_force = Vector3.Lerp (impulsion_force, Vector3.zero, 10*Time.deltaTime);
	}



	void impulsion(Vector3 force)
	{
		impulsion_force = force;
	}
}
