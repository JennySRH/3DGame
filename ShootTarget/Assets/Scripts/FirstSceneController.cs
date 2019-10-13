using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour, ISceneController
{
    // 靶对象
    public GameObject target;
    // 箭队列
    public Queue<GameObject> arrowQueue = new Queue<GameObject>();
    // 当前正在操控的箭
    public GameObject arrow;
    // 风
    public GameObject wind;
    // 树
    public GameObject tree;
    // 地面
    public GameObject floor;
    public void LoadSource()
    {
        // 清除记分
        Singleton<ScoreController>.Instance.ClearScore();
        // 靶对象初始化
        if (target != null)
        {
            Destroy(target);
        }
        target = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Target"));

        if(arrow != null)
        {
            Singleton<ArrowFactory>.Instance.FreeArrow(arrow);
        }

        // 箭队列初始化
        while (arrowQueue.Count > 0)
        {
            Singleton<ArrowFactory>.Instance.FreeArrow(arrowQueue.Dequeue());
        }
        arrowQueue.Clear();

        // 初始化一个箭对象
        arrow = Singleton<ArrowFactory>.Instance.GetArrow();
        arrowQueue.Enqueue(arrow);
    }

    // Start is called before the first frame update
    void Start()
    {


        this.gameObject.AddComponent<ArrowFactory>();
        this.gameObject.AddComponent<ScoreController>();
        this.gameObject.AddComponent<IGUI>();
        this.gameObject.AddComponent<WindController>();
        tree = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Tree")); 
        floor = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Plane"));
        wind = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/Wind"));
        // 设置第一个场景控制器为当前场景控制器
        Director.GetInstance().CurrentSceneController = this;
        Director.GetInstance().CurrentSceneController.LoadSource();
    }

    // Update is called once per frame
    void Update()
    {
        if(arrow.tag == "WaveEnd")
        {
            arrow = Singleton<ArrowFactory>.Instance.GetArrow();
            arrowQueue.Enqueue(arrow);
        }
    }
}
