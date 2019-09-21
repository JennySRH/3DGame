using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    // 基本行为包括 Awake() Start() Update() FixedUpdate() LateUpdate()，常用事件包括 OnGUI() OnDisable() OnEnable()

    void Awake()
    {
        Debug.Log("awake");
    }

    // Use this for initialization
    void Start () {
        Debug.Log("start");

	}
    // Update is called once per frame
    void Update () {
        Debug.Log("update");
	}

    void FixedUpdate()
    {
        Debug.Log("fixedupdate");
    }

    void LateUpdate()
    {
        Debug.Log("lateupdate");
    }

    void OnGUI()
    {
        Debug.Log("ongui");
    }

    void OnDisable()
    {
        Debug.Log("ondisable");
    }

    void OnEnable()
    {
        Debug.Log("onenable");
    }
}
