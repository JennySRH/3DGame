using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour, IUserAction {

    private int state;

    public UserGUI()
    {
        state = -1;
    }

    public void setWin()
    {
        state = 1;
    }

    public void setLose()
    {
        state = 0;
    }

    public void display()
    {
        OnGUI();
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle
        {
            border = new RectOffset(10, 10, 10, 10),
            fontSize = 50,
            fontStyle = FontStyle.BoldAndItalic,
        };
        // normal:Rendering settings for when the component is displayed normally.
        style.normal.textColor = new Color(200 / 255f, 180 / 255f, 150 / 255f);    // 需要除以255，因为范围是0-1
        GUI.Label(new Rect(UnityEngine.Screen.width/4, UnityEngine.Screen.height/10, 200, 80), "Priests and Devils", style);
        if (GUI.Button(new Rect(UnityEngine.Screen.width / 10, UnityEngine.Screen.height / 10, 100, 50), "Reset"))
        {
            Director.GetInstance().scene.Reset();
        }
        if (state == 0)
        {
            style.normal.textColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);
            GUI.Label(new Rect(UnityEngine.Screen.width / 3, UnityEngine.Screen.height/1.3f, 200, 80), "You Lose!", style);
        }
        if (state == 1)
        {
            style.normal.textColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);
            GUI.Label(new Rect(UnityEngine.Screen.width/3, UnityEngine.Screen.height / 1.3f, 200, 80), "You Win!", style);
        }
    }
}
