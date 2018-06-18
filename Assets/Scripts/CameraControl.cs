using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    Camera camera;
    public GameObject target;   //  追従対象
    public float distance = 5;  //  対象との距離
    public float delay = 20;    //  追従の遅延

    Vector3 line;

	// Use this for initialization
	void Start () {
        //  初期位置と対象を結ぶ直線上でカメラを追従させる
        camera = GetComponent<Camera>();
        line = target.transform.position - transform.position;
	}
	
	void FixedUpdate () {
        float h = Input.GetAxisRaw("HorizontalR");
        float v = Input.GetAxisRaw("VerticalR");

        bool is_rotate = false;
        if (h != 0.0f)
        {
            Quaternion axis = Quaternion.AngleAxis(3 * h, Vector3.up);
            line = axis * line;
            is_rotate = true;
        }
        if (v != 0.0f)
        {
            Vector3 rv = Vector3.Cross(Vector3.up, line).normalized;
            Quaternion axis = Quaternion.AngleAxis(3 * -v, rv);
            line = axis * line;
            is_rotate = true;
        }

        Vector3 view = line.normalized;
        //Vector3 offset = ((-view * distance + target.transform.position) - transform.position) / delay;
        Vector3 offset = ((-view * distance + target.transform.position) - transform.position); //急激な移動が無ければ、追従の遅延は要らない
        transform.position += offset;
        //transform.LookAt(target.transform);

        if (true == is_rotate)
        {
            transform.LookAt(target.transform);
        }

    }
}
