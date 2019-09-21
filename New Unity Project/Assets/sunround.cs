using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunround : MonoBehaviour {

    public Transform sun;
    public int x, y, z, speed;
	// Use this for initialization
	void Start () {
        speed = Random.Range(150, 200);
        x = Random.Range(-50, 50);
        y = Random.Range(-50, 50);
        z = Random.Range(-50, 50);

    }

    // Update is called once per frame
    void Update () {
        var axis = new Vector3(x, y, z);
        transform.RotateAround(sun.position, axis, speed * Time.deltaTime);
    }
}
