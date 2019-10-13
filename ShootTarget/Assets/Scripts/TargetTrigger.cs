using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTrigger : MonoBehaviour
{
 

    // 当有箭射中某一环后触发
    void OnTriggerEnter(Collider arrow_head)
    {
        //得到箭身
        Transform arrow = arrow_head.gameObject.transform.parent;
        if (arrow == null)
        {
            return;
        }
        if (arrow.tag == "Arrow")
        {
            arrow_head.gameObject.SetActive(false);
            //箭身速度为0，不受物理影响
            arrow.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            arrow.GetComponent<Rigidbody>().isKinematic = true;
            arrow.tag = "Hit";
            // 分数控制器
            int score = this.gameObject.gameObject.GetComponent<TargetData>().GetScore();
            Singleton<ScoreController>.Instance.AddScore(score);
            //Debug.Log(score);
        }
    }

}
