using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class round : MonoBehaviour {

    public Transform center;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update () {
        //GameObject obj = GameObject.Find("sun");
        //transform.RotateAround(obj.transform.position, Vector3.up, 200 * Time.deltaTime);
        transform.RotateAround(center.position, Vector3.up, 200 * Time.deltaTime);
    }
}
