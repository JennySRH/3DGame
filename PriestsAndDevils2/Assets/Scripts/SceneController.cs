using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface SceneController
{
    void LoadResources();                                  //加载场景
    void print();   // 打印 测试用
    int getPosition(string name); // 获取位置
    void moveto(string name);
    void Reset();
}
