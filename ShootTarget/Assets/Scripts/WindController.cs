using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindController : MonoBehaviour
{

    private float time = 5;
    private float strength = 0.5f;
    private Vector3 direction = new Vector3(0.01f,0,0);

    // Update is called once per frame
    void Update()
    {
        if(time > 0)
        {
            time -= Time.deltaTime;
        }else
        {
            time = 5;
            strength = UnityEngine.Random.Range(0.5f, 0.7f);
            direction = new Vector3(UnityEngine.Random.Range(-0.02f, 0.02f), UnityEngine.Random.Range(-0.02f, 0.02f), 0);
        }
    }


    public float GetStrength()
    {
        return strength;
    }

    public Vector3 GetDirection()
    {
        return direction;
    }


}
