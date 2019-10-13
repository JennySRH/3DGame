using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCAction : MonoBehaviour, IAction
{
    private float time = 1;
    float radian = 0;
    float radius = 0.01f; 
    Vector3 initPosition;

    public void Move()
    {
        Move(Singleton<WindController>.Instance.GetDirection(), Singleton<WindController>.Instance.GetStrength());
    }

    public void Move(Vector3 direction, float strength)
    {
        this.transform.position += direction * strength;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(time > 0)
        {
            
            if (this.gameObject.tag == "Hit")
            {
                 time -= Time.deltaTime;
                // 弧度每次增加
                 radian += 0.05f;
                // 颤抖
                 float dy = Mathf.Cos(radian) * radius;
                 transform.position = initPosition + new Vector3(0, dy, 0);
            }
            else
            {
                initPosition = transform.position;
            }
        }
        if(time <= 0)
        {
            this.gameObject.tag = "WaveEnd";
            transform.position = initPosition;
        }
        
    }
}
