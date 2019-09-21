using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class usegravity : MonoBehaviour
{
    public Vector3 initial;
    // Start is called before the first frame update
    void Start()
    {
        initial = transform.position;
        // 方法1：
        /*
        this.gameObject.AddComponent<Rigidbody>();
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);
        */
        // y = x^2, 移到
    }

    // Update is called once per frame
    void Update()
    {
        initial.x -= 0.1f;
        transform.Translate(Time.deltaTime * new Vector3(initial.x, initial.x*initial.x, 0), Space.World);
        //transform.position -= new Vector3(Time.deltaTime, Time.deltaTime * Time.deltaTime + 2 * initial.y * Time.deltaTime, 0);
    }
}
