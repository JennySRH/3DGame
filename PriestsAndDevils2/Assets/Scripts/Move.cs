using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    void OnMouseDown()
    {
        Debug.Log("onmousedown");
        Director.GetInstance().scene.moveto(transform.name);
    }

	// Use this for initialization
	void Start () {
        Debug.Log(transform.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
