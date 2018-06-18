using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA.Input;


public class Gesture : MonoBehaviour {

	GestureRecognizer recognizer;
	Camera userHead;

	public GameObject marker;
	public GameObject viewPoint;

	private void init()
	{
		userHead = GameObject.Find("Main Camera").GetComponent<Camera>();
	}



	private void Awake()
	{
		recognizer = new GestureRecognizer();
		recognizer.Tapped += OnTapped;
		recognizer.StartCapturingGestures();
	}

	// Use this for initialization
	void Start () {
		init();
	}
	
	// Update is called once per frame
	void Update () {
		if(marker != null)
		{
			if(isHit == true)
			{
				marker.SetActive(true);
				marker.transform.position = hitPos;
			}
			else
			{
				marker.SetActive(false);
			}
		}

		updateViewPoint();
	}


	void updateViewPoint()
	{
		Camera camera = GameObject.Find("Main Camera").GetComponent<Camera>();
		Vector3 pos = camera.transform.position;
		Vector3 forward = camera.transform.forward;

		RaycastHit hitInfo;

		if(Physics.Raycast(pos, forward, out hitInfo))
		{
			if(viewPoint != null)
			{
				viewPoint.transform.position = hitInfo.point;
			}
		}

	}




	bool isHit = false;
	Vector3 hitPos = Vector3.zero;

	void OnTapped(TappedEventArgs args)
	{
		//Debug.Log("Air Tapped:" + args.headPose.position.ToString());
		Debug.Log("Air Tapped:" + userHead.transform.position.ToString());
		//Vector3 front = new Vector3(0, 0, 1);

		//args.headPose.rotation

		Vector3 pos = userHead.transform.position;
		Vector3 forward = userHead.transform.forward;

		RaycastHit hitInfo;
		if(Physics.Raycast(pos, forward, out hitInfo))
		{
			hitPos = hitInfo.point;
			isHit = true;

			GameObject gobj;
			gobj = Instantiate(marker);
			gobj.transform.SetParent(transform, false);
		}
		else
		{
			hitPos = Vector3.zero;
			isHit = false;
		}


		/*
		Generator gen = GetComponent<Generator>();
		if(gen != null)
		{
			gen.spawnTarget(pos);
		}*/
	}

}
