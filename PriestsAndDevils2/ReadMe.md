# Priests and Devils ——动作分离版

完整工程文件在github

游戏视频<https://www.bilibili.com/video/av68091576>

在之前的牧师与恶魔的游戏制作中，我们使用`FirstController`来控制游戏中人物的动作，这样写出了的代码低内聚高耦合。在这里，我们对上一版的牧师与恶魔进行更新迭代，将动作从`FirstController`中分离出来，减少程序的耦合提高内聚性。

我们首先创建一个类`Action`用来管理所有游戏事物的动作，然后在`FirstController`中创建这个类的实例，用来管理游戏事物的运动。

```c#
    public Action actionManager;   //动作管理
```

在这个版本中，游戏事物的所有动作都由`Action`来管理，而游戏事物的信息、状态等都由`Role`和`Boat`来管理。比如游戏角色（牧师和恶魔）上下船的动作，由`Fristcontroller`与`Action`进行通信，让`Action`来管理动作的改变，同时告知`Role`进行状态的改变。

`FirstController`的代码如下所示：

```c#
if(obj.position == 0 && obj.position == boat.position && boat.num < 2)
{
    boat.num++;
    if(boat.BoatState[0] == 0)
    {
        // 更改model
        obj.getBoat(1);
        // 更改动作
        actionManager.getBoat(obj, 1);
        boat.BoatState[0] = MapName(name);
    }
    else
    {
        obj.getBoat(2);
        actionManager.getBoat(obj, 2);
        boat.BoatState[1] = MapName(name);
    }
}
```

`Action`中的相关代码如下所示：

```c#
    public void getBoat(Role role, int i)
    {
        if (i == 1)
        {
            role.character.transform.position = new Vector3(0, -3, 4);
        }
        else if (i == 2)
        {
            role.character.transform.position = new Vector3(0, -3, 6);
        }
        else if (i == 3)
        {
            role.character.transform.position = new Vector3(0, -3, -4);
        }
        else
        {
            role.character.transform.position = new Vector3(0, -3, -6);
        }
    }
```

`Role`中的相关代码如下所示：

```c#
    public void getBoat(int i)
    {
        position = 2;
        if (i == 1)
        {
            boatPos = 0;
        }
        else if(i == 2)
        {
            boatPos = 1;
        }
        else if(i == 3)
        {
            boatPos = 1;
        }
        else
        {
            boatPos = 0;
        }
    }
```



其余的更改基本跟上下船相同，`Action`类代码如下所示。

```c#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action 
{

    public void GetBoat(Role role, int i)
    {
        if (i == 1)
        {
            role.character.transform.position = new Vector3(0, -3, 4);
        }
        else if (i == 2)
        {
            role.character.transform.position = new Vector3(0, -3, 6);
        }
        else if (i == 3)
        {
            role.character.transform.position = new Vector3(0, -3, -4);
        }
        else
        {
            role.character.transform.position = new Vector3(0, -3, -6);
        }
    }


    public void MoveToOrigin(Role role)
    {
        role.character.transform.position = role.origin;
    }

    public void MoveToDest(Role role)
    {
        role.character.transform.position = role.dest;
    }


    Role MapNumToRole(int num, Role []Priests, Role []Devils)
    {
        Role tmp = null;
        if (num < 4 && num > 0)
        {
            tmp = Priests[num - 1];
        }
        else if (num >= 4 && num <= 6)
        {
            tmp = Devils[num - 4];
        }
        return tmp;
    }

    public void MoveBoat(Boat boat, Role[] Priests, Role[] Devils)
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

        for (int i = 0; i < 2; i++)
        {
            int b = i;
            Role r = MapNumToRole(boat.BoatState[i], Priests, Devils);
            if (r != null)
            {
                Vector3 cur = r.character.transform.position;
                r.character.transform.position = Vector3.MoveTowards(cur, targets[a, b], 8f * Time.deltaTime);
            }
        }
    }
}

```

