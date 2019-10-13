using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActionManager
{
    void Move(Vector3 direction, float speed);
}