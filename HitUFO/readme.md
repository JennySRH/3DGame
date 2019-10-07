## 打飞碟游戏

打飞碟游戏共有3个round，整体难度是由飞碟的速度和飞碟出现的间隔体现的。

本次鼠标打飞碟游戏的实现参考了前辈的博客<https://blog.csdn.net/x2_yt/article/details/66969242>

每个飞碟有以下属性：

```c#
public class DiskData : MonoBehaviour
{
    // 色彩、大小、发射位置、速度、角度
    public Vector3 size;
    public Color color;
    public float speed;
    public Vector3 direction;
    public Vector3 initPosition;

}
```

所有的飞碟用一个工厂类来管理，该工厂是场景单实例的，用来管理不同飞碟的生产和回收。根据当前的round设定飞碟的大小、速度，round越大，飞碟的越小，速度越快。

```c#
        // 飞碟的速度跟round成正比
        newDisk.GetComponent<DiskData>().speed = 2.0f * round;
        // 飞碟的大小跟round成反比（缩放倍数）
        newDisk.GetComponent<DiskData>().size = 1 - 0.1f * (round-1);
        // 飞碟的颜色是随机生成的
        float random = UnityEngine.Random.Range(0f, 3f);
        if (random < 1)
        {
            newDisk.GetComponent<DiskData>().color = Color.yellow;
        }
        else if(random < 2)
        {
            newDisk.GetComponent<DiskData>().color = Color.red;
        }
        else
        {
            newDisk.GetComponent<DiskData>().color = Color.blue;
        }
        // 飞碟的发射方向
        float RanX = UnityEngine.Random.Range(-1f, 1f) < 0 ? -1 : 1;
        newDisk.GetComponent<DiskData>().direction = new Vector3(-RanX, UnityEngine.Random.Range(-1f, 1f), 0);
        // 飞碟的初始位置
        newDisk.GetComponent<DiskData>().initPosition = new Vector3(RanX*9, UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

```

MVC模式跟之前完全相同，有导演类、场景类、动作类、UI类。

导演类是单例模式

```c#
public class Director: System.Object
{
    public ISceneController currentSceneControl { get; set; }
    private static Director director;
    private Director(){}
    public static Director getInstance()
    {
        if (director == null)
        {
            director = new Director();
        }
        return director;
    }
}
```

场记类有一个公共接口

```c#
public interface ISceneController 
{
    void LoadSource();
}

```

FirstController实现了公共接口，作为第一个场景

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 第一个场景控制器
public class FirstSceneController : MonoBehaviour, ISceneController
{
    // 已经发射的飞碟数量，第一关10个飞碟，第二关20个飞碟，第三关30个飞碟
    public int DiskNum;
    // 当前时间
    public float time;
    // 当前回合数
    public int round;
    // 飞碟队列
    public Queue<GameObject> diskQueue = new Queue<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        round = 0;
        DiskNum = 0;
        // 设置第一个场景控制器为当前场景控制器
        Director.getInstance().currentSceneControl = this;
        this.gameObject.AddComponent<DiskFactory>();
        this.gameObject.AddComponent<UserGUI>();
        Director.getInstance().currentSceneControl.LoadSource();

    }

    // 初始化每个回合的飞碟队列
    void InitDiskQueue(int round)
    {
        for(int i = 0;i < round*10;i ++)
        {
            diskQueue.Enqueue(Singleton<DiskFactory>.Instance.GetDisk(round));
        }
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        // 发射飞碟的间隔回合数成反比
        if(time >= 2.0f-0.3*round)
        {
            if (DiskNum == 0)
            {
                round = 1;
                this.gameObject.GetComponent<UserGUI>().round = 1;
                InitDiskQueue(round);
            }
            else if (DiskNum == 10)
            {
                round = 2;
                this.gameObject.GetComponent<UserGUI>().round = 2;
                InitDiskQueue(round);
            }
            else if (DiskNum == 20)
            {
                round = 3;
                this.gameObject.GetComponent<UserGUI>().round = 3;
                InitDiskQueue(round);
            }
            else if(DiskNum >= 30)
            {
                Reset();
            }
            if(DiskNum < 30)
            {
                time = 0;
                ThrowDisk();
                DiskNum++;
                this.gameObject.GetComponent<UserGUI>().total++;
                Debug.Log(DiskNum);
            }


        }
    }

    public void ThrowDisk()
    {
        if(diskQueue.Count > 0)
        {
            GameObject disk = diskQueue.Dequeue();
            disk.GetComponent<Renderer>().material.color = disk.GetComponent<DiskData>().color;
            disk.transform.position = disk.GetComponent<DiskData>().initPosition;
            disk.transform.localScale = disk.GetComponent<DiskData>().size * disk.transform.localScale;
            disk.SetActive(true);
            disk.AddComponent<Action>();
            disk.GetComponent<Action>().Move(disk.GetComponent<DiskData>().direction, disk.GetComponent<DiskData>().speed);
        }
    }

    public void LoadSource()
    {
        this.gameObject.GetComponent<UserGUI>().round = 0;
        this.gameObject.GetComponent<UserGUI>().total = 0;
        ScoreController.getInstance().score = 0;
        DiskNum = 0;
        time = 0;
        round = 0;
        diskQueue.Clear();
    }

    void Reset()
    {
        this.gameObject.GetComponent<UserGUI>().reset = 1;
    }

}

```

记分的类也是单例模式

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController
{

    public int score = 0;

    private static ScoreController scorecontrol;

    private ScoreController() {
        score = 0;
    }
    public static ScoreController getInstance()
    {
        if (scorecontrol == null)
        {
            scorecontrol = new ScoreController();
        }
        return scorecontrol;
    }

    public void AddScore()
    {
        score++;
    }

    public int GetScore()
    {
        return score;
    }

}
```

用户点击事件和飞碟运动事件都在`Action`类中实现。

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    public Vector3 direction;
    public float speed;
    public GameObject cam;

    public void Move(Vector3 direction,float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += speed * direction * Time.deltaTime;


        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Fired Pressed");
            Debug.Log(Input.mousePosition);

            Vector3 mp = Input.mousePosition; //get Screen Position

            //create ray, origin is camera, and direction to mousepoint
            Camera ca;
            if (cam != null) ca = cam.GetComponent<Camera>();
            else ca = Camera.main;

            Ray ray = ca.ScreenPointToRay(Input.mousePosition);

            //Return the ray's hits
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                print(hit.transform.gameObject.name);
                if (hit.collider.gameObject.tag.Contains("Finish"))
                { //plane tag
                    Debug.Log("hit " + hit.collider.gameObject.name + "!");
                }
                Singleton<DiskFactory>.Instance.FreeDisk(hit.transform.gameObject);
                ScoreController.getInstance().AddScore();
            }
        }


    }
}
```

