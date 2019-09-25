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
