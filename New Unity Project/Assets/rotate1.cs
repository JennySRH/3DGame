using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate1 : MonoBehaviour {

    public int speed = 20;
    public Vector3 e = Vector3.forward;

	
	// Update is called once per frame
	void Update () {
        Quaternion q = Quaternion.AngleAxis(speed * Time.deltaTime, e);
        GameObject center = GameObject.Find("b");
        this.transform.position = q * transform.position;// + center.transform.position;
    }
}
