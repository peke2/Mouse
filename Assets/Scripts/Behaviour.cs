using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Behaviour : MonoBehaviour {

	delegate void ProcState();

	ProcState procState = null;

	NavMeshAgent agent;
	Vector3 moveGoal;

	int moveIntervalFrame;

	const int INTERVAL_FRAME_BASE = 120;

	const float DISTANCE_SCALE = 0.3f;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		setIntervalFrame();
		procState = stateWait;
	}
	
	// Update is called once per frame
	void Update () {
		if(procState != null)
		{
			procState();
		}

	}


	void stateMove()
	{
		NavMeshAgent agent = GetComponent<NavMeshAgent>();
		if(agent == null) return;

		if( (moveGoal - transform.position).magnitude < 0.05f)
		//if(agent.velocity.magnitude < 0.0001f)
		{
			//agent.Stop();
			JerboaCharacter jc = GetComponent<JerboaCharacter>();
			jc.Sitdown();
			procState = stateWait;
			setIntervalFrame();
		}
	}

	void stateWait()
	{
		moveIntervalFrame--;
		if(moveIntervalFrame <= 0)
		{
			moveToGoal();
			procState = stateMove;
		}
	}


	void moveToGoal()
	{
		Vector2 v = Random.insideUnitCircle;
		float len = v.magnitude;
		v *= DISTANCE_SCALE;
		Vector3 start = new Vector3(transform.position.x + v.x, 100, transform.position.z + v.y);

		RaycastHit hitInfo = new RaycastHit();

		if(Physics.Raycast(start, new Vector3(0, -1, 0), out hitInfo, Mathf.Infinity, LayerMask.GetMask("Ground")))
		{
			agent.destination = hitInfo.point;
			moveGoal = hitInfo.point;


			JerboaCharacter jc = GetComponent<JerboaCharacter>();
			jc.StandUp();
			if(len > 0.4f)
			{
				jc.walkMode = 2f;       //	関数 Walk() を呼ぶと動作の切り替えと勘違いするので、変数のセットを呼び出す
			}
			else
			{
				jc.walkMode = 1f;
			}
			jc.forwardSpeed = 1f;
			jc.Move();
		}

	}


	void setIntervalFrame()
	{
		moveIntervalFrame = INTERVAL_FRAME_BASE + Random.Range(30, 180);
	}

}
