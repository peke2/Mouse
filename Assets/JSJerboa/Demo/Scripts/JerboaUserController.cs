using UnityEngine;
using System.Collections;

public class JerboaUserController : MonoBehaviour {
	JerboaCharacter jerboaCharacter;
	
	void Start () {
		jerboaCharacter = GetComponent <JerboaCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			jerboaCharacter.Attack();
		}
		if (Input.GetButtonDown ("Jump")) {
			jerboaCharacter.Jump();
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			jerboaCharacter.Hit();
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			jerboaCharacter.Grooming();
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			jerboaCharacter.Death();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			jerboaCharacter.Rebirth();
		}		

		if (Input.GetKeyDown (KeyCode.U)) {
			jerboaCharacter.StandUp();
		}		
		if (Input.GetKeyDown (KeyCode.N)) {
			jerboaCharacter.Sitdown();
		}	
		
		if (Input.GetKeyDown (KeyCode.R)) {
			jerboaCharacter.Gallop();
		}	
		if (Input.GetKeyUp (KeyCode.R)) {
			jerboaCharacter.Walk();
		}	
		
		jerboaCharacter.forwardSpeed=jerboaCharacter.walkMode*Input.GetAxis ("Vertical");
		jerboaCharacter.turnSpeed= Input.GetAxis ("Horizontal");
	}
}
