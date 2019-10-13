using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JoyStick : MonoBehaviour
{

    public float speedX = 1.0F;
    public float speedY = 1.0F;
    bool flag = true;
    // Update is called once per frame
    void Update()
    {
        if(flag)
        {
            float translationY = Input.GetAxis("Vertical") * speedY;
            float translationX = Input.GetAxis("Horizontal") * speedX;
            translationY *= Time.deltaTime;
            translationX *= Time.deltaTime;
            // 限制移动的范围
            if (transform.position.x - translationX < 1.5 && transform.position.x - translationX > -1.5
                && transform.position.y + translationY > -0.8 && transform.position.y + translationY < 0.8)
            {
                transform.position = transform.position + new Vector3(-translationX, translationY, 0);
            }
            if (Input.GetButton("Jump"))
            {
                // 如果空额按下，则给箭增加刚体，使其能够进行物理学的运动
                this.gameObject.AddComponent<Rigidbody>();
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                flag = false;
            }
        }
    }
}

