using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAction : MonoBehaviour, IActionManager
{

    public Vector3 direction;
    public float speed;

    public void Move(Vector3 direction,float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += speed * direction * Time.deltaTime;
    }
}
