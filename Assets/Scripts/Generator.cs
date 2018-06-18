using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Navigation))]

public class Generator : MonoBehaviour {

	public GameObject target;
	public int numSpawn = 1;

	public bool spawnsStartTime = false;

	// Use this for initialization
	void Start ()
	{
		if(spawnsStartTime == true)
		{
			StartCoroutine(waitSpawn());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	IEnumerator waitSpawn()
	{
		Navigation navi = GetComponent<Navigation>();
		if(navi == null)
		{
			yield break;
		}

		while(navi.isReadyNavigation() == false)
		{
			yield return null;
		}

		for(int i = 0; i < numSpawn; i++)
		{
			spawnTargetInArea();
		}
	}


	void spawnTargetInArea()
	{
		Vector2 p = Random.insideUnitCircle * 5.0f;
		Vector3 pos = new Vector3(p.x, 5, p.y);

		spawnTarget(pos);
	}


	public void spawnTarget(Vector3 pos)
	{
		if( target == null ) return;

		GameObject obj = GameObject.Instantiate(target);
		obj.transform.position = pos;
		obj.transform.SetParent(transform, false);
	}

}
