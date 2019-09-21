using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat{
    public int[] BoatState;  // 记录船上的载荷，0代表无，1代表Priest1...,4代表Devil1
    public int num;         // 记录载荷
    public int position;        // 记录船的位置:0在from的位置，1在to的位置
    public GameObject thisboat;
    public Move moveto;
    public int movestate;   // 船的移动状态，0表示静止，1表示从from到to，2表示从to到from
    public Boat()
    {
        movestate = 0;
        num = 0;
        position = 0;
        BoatState = new int[2];
        BoatState[0] = BoatState[1] = 0;
        thisboat = Object.Instantiate(Resources.Load("Boat", typeof(GameObject)), new Vector3(0, -3, 4), Quaternion.identity, null) as GameObject;
        thisboat.name = "boat";
        var unused = thisboat.AddComponent(typeof(Move)) as Move;
        thisboat.AddComponent<BoxCollider>();
        thisboat.transform.rotation = Quaternion.Euler(-90, 0, 0);
        /*
        Transform[] father = thisboat.transform.GetComponentsInChildren<Transform>();
        foreach (var child in father)
        {
            GameObject tmp = child.gameObject;
            var unused = tmp.AddComponent(typeof(Move)) as Move;
            tmp.AddComponent<BoxCollider>();
            //Debug.Log(child.name);
        }
        Debug.Log("hello world!");
        */
    }


}
