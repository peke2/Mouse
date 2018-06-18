using UnityEngine;
using System.Collections;

public class JerboaCharacter : MonoBehaviour {
	Animator jerboaAnimator;
	public bool jumpStart=false;
	public float groundCheckDistance = 0.6f;
	public float groundCheckOffset=0.01f;
	public bool isGrounded=true;
	public float jumpSpeed=1f;
	Rigidbody jerboaRigid;
	public float forwardSpeed;
	public float turnSpeed;
	public float walkMode=1f;
	public float jumpStartTime=0f;
	
	void Start () {
		jerboaAnimator = GetComponent<Animator> ();
		jerboaRigid=GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		CheckGroundStatus ();
		Move ();
		jumpStartTime+=Time.deltaTime;
	}
	
	public void Attack(){
		jerboaAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		jerboaAnimator.SetTrigger("Hit");
	}

	public void Grooming(){
		jerboaAnimator.SetTrigger("Grooming");
	}

	public void Death(){
		jerboaAnimator.SetBool("IsLived",false);
	}
	
	public void Rebirth(){
		jerboaAnimator.SetBool("IsLived",true);
	}
	
	public void StandUp(){
		jerboaAnimator.SetBool("SitDown",false);
	}
	
	public void Sitdown(){
		jerboaAnimator.SetBool("SitDown",true);
	}
	
	public void Gallop(){
		walkMode = 2f;
	}
	
	public void Walk(){
		walkMode = 1f;
	}
	
	public void Jump(){
		if (isGrounded) {
			jerboaAnimator.SetBool ("JumpStart",true);
			jumpStart = true;
			jumpStartTime=0f;
			isGrounded=false;
			jerboaAnimator.SetBool("IsGrounded",false);
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		isGrounded = Physics.Raycast (transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		
		if (jumpStart) {
			if(jumpStartTime>.15f){
				jumpStart=false;
				jerboaAnimator.SetBool ("JumpStart",false);
				jerboaRigid.AddForce((transform.up+transform.forward*forwardSpeed)*jumpSpeed,ForceMode.Impulse);
				jerboaAnimator.applyRootMotion = false;
				jerboaAnimator.SetBool("IsGrounded",false);
			}
		}
		
		if (isGrounded && !jumpStart && jumpStartTime>.3f) {
			jerboaAnimator.applyRootMotion = true;
			jerboaAnimator.SetBool ("IsGrounded", true);
		} else {
			if(!jumpStart){
				jerboaAnimator.applyRootMotion = false;
				jerboaAnimator.SetBool ("IsGrounded", false);
			}
		}
	}
	
	public void Move(){
		jerboaAnimator.SetFloat ("Forward", forwardSpeed*walkMode);
		jerboaAnimator.SetFloat ("Turn", turnSpeed);
	}
}
