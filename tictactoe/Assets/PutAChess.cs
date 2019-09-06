using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutAChess : MonoBehaviour {

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        string str = this.name;
        if (global.chess[str[0] - '1'][str[1] - '1'] == 0)
        {
            int childCount = transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                Destroy(transform.GetChild(0).gameObject);
            }

        }
    }

    void OnMouseDown()
    {
        string str = this.name;
        if(global.chess[str[0]-'1'][str[1]-'1'] != 0)
        {
            return;
        } 
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = ("temp"+str);
        Vector3 player_postion = this.transform.position;
        obj.transform.position = player_postion;
        obj.transform.parent = this.transform;
        if(global.turn==1)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.red;
            global.turn = 0;
            global.chess[str[0] - '1'][str[1] - '1'] = 1;
        }
        else
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.blue;
            global.turn = 1;
            global.chess[str[0] - '1'][str[1] - '1'] = 2;
        }
    }
    void OnMouseUp()
    {
        if(global.ai==1)
        {
            GameObject[] objs = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject temp in objs)
            {
                string str = temp.name;
                if(str.Length==2 && global.chess[str[0] - '1'][str[1] - '1']==0)
                {
                    Debug.Log("hi");
                    GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    obj.name = ("temp" + str);
                    Vector3 player_postion = temp.transform.position;
                    obj.transform.position = player_postion;
                    obj.transform.parent = temp.transform;
                    obj.GetComponent<MeshRenderer>().material.color = Color.blue;
                    global.turn = 1;
                    global.chess[str[0] - '1'][str[1] - '1'] = 2;
                    break;
                }
            }
        }
    }
}
