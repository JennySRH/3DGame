using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalManager : MonoBehaviour, IActionManager
{

    public float speed;

    public void Move(float speed)
    {
        this.speed = speed;
    }
    public void Move(Vector3 direction, float speed)
    {
        Move(speed);
    }

    void FixedUpdate()
    {
        Rigidbody rigid = this.gameObject.GetComponent<Rigidbody>();
        if(rigid)
        {
            //Debug.Log(speed);
            rigid.AddForce(Vector3.down * speed);
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

}
