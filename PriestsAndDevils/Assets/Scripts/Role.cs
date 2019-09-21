using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role {
    public GameObject character;
    public Move moveto;
    public int position;    // 0:from 1:to 2:boat
    public int boatPos;     // 0 1
    public Vector3 origin;
    public Vector3 dest;
    public Role(string name)
    {
        position = 0;
        character = Object.Instantiate(Resources.Load(name, typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
        moveto = character.AddComponent(typeof(Move)) as Move;
    }

    public void setName(string name)
    {
        character.name = name;
    }

    public void getBoat(int i)
    {
        position = 2;
        if (i == 1)
        {
            boatPos = 0;
            character.transform.position = new Vector3(0, -3, 4);
        }
        else if(i == 2)
        {
            boatPos = 1;
            character.transform.position = new Vector3(0, -3, 6);
        }
        else if(i == 3)
        {
            boatPos = 1;
            character.transform.position = new Vector3(0, -3, -4);
        }
        else
        {
            boatPos = 0;
            character.transform.position = new Vector3(0, -3, -6);
        }
    }

    public void setOriginalPos(Vector3 a)
    {
        character.transform.position = a;
        origin = a;
    }

    public void setDestPos(Vector3 a)
    {
        dest = a;
    }

    public void MoveToOrigin()
    {
        position = 0;
        character.transform.position = origin;
    }

    public void MoveToDest()
    {
        position = 1;
        character.transform.position = dest;
    }

}
