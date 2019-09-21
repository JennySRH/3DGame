using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstController : MonoBehaviour, SceneController{

    private Role[] Priests;              // 牧师
    private Role[] Devils;               // 恶魔
    private GameObject GroundFrom;       // 陆地
    private GameObject GroundTo;         // 陆地
    private GameObject Water;            // 水
    private Boat boat;             // 船
    private int fromDevilNum;
    private int fromPriestNum;
    private int toDevilNum;
    private int toPriestNum;
    private int state;      // -1代表正在进行，0代表输，1代表赢

    private UserGUI mygui;

    FirstController()
    {
        Priests = new Role[3];
        Devils = new Role[3];
    }

    // Use this for initialization
    void Start() {
        Director director = Director.GetInstance();      //得到导演实例
        director.scene = this;                          //设置当前场景控制器
        this.LoadResources();
    }

    public void LoadResources()
    {
        Debug.Log("load resource");

        state = -1;

        // 创建新的GUI
        mygui = new UserGUI();

        // 加载数量
        fromDevilNum = fromPriestNum = 3;
        toDevilNum = toPriestNum = 0;


        // 加载地面
        GroundFrom = Object.Instantiate(Resources.Load("ground", typeof(GameObject)), new Vector3(0, -5, 15), Quaternion.identity, null) as GameObject;
        GroundFrom.transform.localScale = new Vector3(20, 9, 15);
        GroundFrom.name = "groundfrom";
        GroundTo = Object.Instantiate(Resources.Load("ground", typeof(GameObject)), new Vector3(0, -5, -15), Quaternion.identity, null) as GameObject;
        GroundTo.transform.localScale = new Vector3(20, 9, 15);
        GroundTo.name = "groundto";

        // 加载水
        Water = Object.Instantiate(Resources.Load("Water", typeof(GameObject)), new Vector3(0, -6, 0), Quaternion.identity, null) as GameObject;
        Water.name = "water";

        // 加载船
        boat = new Boat();

        // 加载牧师对象
        for (int i = 0;i < 3;i ++)
        {
            Role temp = new Role("Priest");
            temp.setName("Priest"+ i);
            Priests[i] = temp;
            float p_z = (float)(8 + i * 1.5);
            Priests[i].setOriginalPos(new Vector3(0, -1, p_z));
            Priests[i].setDestPos(new Vector3(0, -1, -p_z));
            Priests[i].character.transform.localScale = new Vector3(1.6f, 2f, 1.6f);
            Priests[i].character.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        // 加载恶魔对象
        for (int i = 0; i < 3; i++)
        {
            Role temp = new Role("Devil");
            temp.setName("Devil" + i);
            Devils[i] = temp;
            float p_z = (float)(13 + i * 1.5);
            Devils[i].setOriginalPos(new Vector3(0, -1, p_z));
            Devils[i].setDestPos(new Vector3(0, -1, -p_z));
            Devils[i].character.transform.localScale = new Vector3(1.6f, 2, 1.6f);
            Devils[i].character.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

    }

    public void print()
    {
        Debug.Log("hello world");
    }

    public Role findRoleByName(string name)
    {
        Role obj = null;
        for (int i = 0; i < 3; i++)
        {
            if (name == ("Priest" + i))
            {
                obj = Priests[i];
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (name == ("Devil" + i))
            {
                obj = Devils[i];
            }
        }
        return obj;
    }

    public int MapName(string name)
    {
        int obj = 0;
        for (int i = 0; i < 3; i++)
        {
            if (name == ("Priest" + i))
            {
                obj = i+1;
                return obj;
            }
        }
        for (int i = 0; i < 3; i++)
        {
            if (name == ("Devil" + i))
            {
                obj = i+4;
                return obj;
            }
        }
        return obj;
    }

    public int getPosition(string name)
    {
        if(name == "boat")
        {
            return boat.position;
        }
        else
        {
            return findRoleByName(name).position;
        }
    }

    public void moveto(string name)
    {
        // 船不是静止状态下不能操作
        if(boat.movestate != 0)
        {
            return;
        }
        if(name == "boat")
        {
            // 船从from到to
            if(boat.position == 0 && boat.num > 0)
            {
                boat.position = 1;
                boat.movestate = 1;
                foreach(int num in boat.BoatState)
                {
                    if(num > 0 && num < 4)
                    {
                        fromPriestNum--;
                        toPriestNum++;
                    }
                    else if(num > 3)
                    {
                        fromDevilNum--;
                        toDevilNum++;
                    }
                }
                
            }
            // 船从to到from
            else if(boat.num > 0)
            {
                boat.position = 0;
                boat.movestate = 2;
                foreach (int num in boat.BoatState)
                {
                    if (num > 0 && num < 4)
                    {
                        toPriestNum--;
                        fromPriestNum++;
                    }
                    else if (num > 3)
                    {
                        toDevilNum--;
                        fromDevilNum++;
                    }
                }
            }
        }
        // 上下船
        else
        {
            Role obj = findRoleByName(name);
            // 上船
            if(obj.position == 0 && obj.position == boat.position && boat.num < 2)
            {
                boat.num++;
                if(boat.BoatState[0] == 0)
                {
                    obj.getBoat(1);
                    boat.BoatState[0] = MapName(name);
                }
                else
                {
                    obj.getBoat(2);
                    boat.BoatState[1] = MapName(name);
                }
            }
            else if(obj.position == 1 && obj.position == boat.position && boat.num < 2)
            {
                boat.num++;
                if (boat.BoatState[0] == 0)
                {
                    obj.getBoat(4);
                    boat.BoatState[0] = MapName(name);
                }
                else
                {
                    obj.getBoat(3);
                    boat.BoatState[1] = MapName(name);
                }
            }
            // 下船
            else if(obj.position == 2)
            {
                if(boat.position == 0)
                {
                    boat.num--;
                    boat.BoatState[obj.boatPos] = 0;
                    obj.MoveToOrigin();
                }
                else
                {
                    boat.num--;
                    boat.BoatState[obj.boatPos] = 0;
                    obj.MoveToDest();
                }
            }
        }
    }

    Role MapNumToRole(int num)
    {
        Role tmp = null;
        if(num<4 && num > 0)
        {
            tmp = Priests[num - 1];
        }
        else if(num >= 4 && num <= 6)
        {
            tmp = Devils[num - 4];
        }
        return tmp;
    }

    // Update is called once per frame
    void Update () {
        if(boat.movestate > 0)
        {
            // 船移动
            Vector3 Current = boat.thisboat.transform.position;
            Vector3[] Target = { new Vector3(0, -3, -5), new Vector3(0, -3, 4) };
            Vector3 tar = Target[boat.movestate - 1];
            int a = boat.movestate - 1;
            if (Current == tar)
            {
                boat.movestate = 0;
            }
            boat.thisboat.transform.position = Vector3.MoveTowards(Current, tar, 8f * Time.deltaTime);
            Vector3[,] targets = { 
                { new Vector3(0, -3, -5), new Vector3(0, -3, -6) },
                { new Vector3(0, -3, 5), new Vector3(0, -3, 6) }
            };

            for (int i = 0;i < 2;i ++)
            {
                int b = i;
                Role r = MapNumToRole(boat.BoatState[i]);
                if (r != null)
                {
                    Vector3 cur = r.character.transform.position;
                    r.character.transform.position = Vector3.MoveTowards(cur, targets[a,b], 8f * Time.deltaTime);
                }
            }
            
        }


        if ((fromPriestNum != 0 && fromDevilNum > fromPriestNum) || (toPriestNum != 0 && toDevilNum > toPriestNum))
        {
            state = 0;
            mygui.setLose();
        }

        if (toDevilNum+toPriestNum == 6 && boat.num == 0)
        {
            state = 1;
            mygui.setWin();
        }
	}

    public void Reset()
    {
        for(int i = 0;i < 3;i ++)
        {
            Destroy(Priests[i].character);
            Destroy(Devils[i].character);
            Destroy(boat.thisboat);
            Destroy(GroundFrom);
            Destroy(GroundTo);
            Destroy(Water);
        }
        LoadResources();
    }


    void OnGUI()
    {
        mygui.display();
    }


}
