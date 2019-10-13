using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAction : MonoBehaviour, IAction
{

    float force = 3;

    public void Move(){}


    public void Move(float force)
    {
        this.force = force;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 如果没有射中靶对象
        if(this.gameObject.transform.position.z < -1)
        {
            this.gameObject.tag = "WaveEnd";
        }
    }

    void FixedUpdate()
    {
        // 如果射中了靶对象
        if (this.gameObject.GetComponent<Rigidbody>() != null)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1) * force);
        }
    }

}
