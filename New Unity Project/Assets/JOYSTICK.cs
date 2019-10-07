using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{

    public float speedX = 10.0F;
    public float speedY = 10.0F;

    // Update is called once per frame
    void Update()
    {
        float translationY = Input.GetAxis("Vertical") * speedY;
        float translationX = Input.GetAxis("Horizontal") * speedX;
        translationY *= Time.deltaTime;
        translationX *= Time.deltaTime;
        //transform.Translate(0, translationY, 0);
        //transform.Translate(translationX, 0, 0);
        transform.Translate(translationX, translationY, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fired Pressed");
        }
    }

    private void OnGUI()
    {
        //GUI.Button(new Rect(100, 150, 100, 50), "Fire1");
    }
}
