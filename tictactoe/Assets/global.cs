using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global : MonoBehaviour {
    public static int turn = 1;
    public static int[][] chess = new int[3][];// 创建一个3行的二维数组
    public static int flag = 0;
    public static int ai = 0;
	// Use this for initialization
	void Start () {
        for (int i = 0; i < 3; i++)
        {
            chess[i] = new int[3] { 0, 0, 0 };
        }
    }

    void Reset()
    {
        ai = 0;
        flag = 0;
        turn = 1;
        for(int i = 0;i < 3;i ++)
        {
            for(int j = 0;j < 3;j ++)
            {
                chess[i][j] = 0;
            }
        }
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
        GUI.Label(new Rect(340, 10, 200, 80), "TicTacToe", style);
        
        if(GUI.Button(new Rect(100, 150, 100, 50), "Reset"))
        {
            Reset();
        }
        if (flag == 1)
        {
            style.normal.textColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);
            GUI.Label(new Rect(300, 350, 200, 80), "Red Victory!", style);
        }
        else if(flag == 2)
        {
            style.normal.textColor = new Color(0 / 255f, 0 / 255f, 255 / 255f);
            GUI.Label(new Rect(300, 350, 200, 80), "Blue Victory!", style);
        }
        if(GUI.Button(new Rect(100, 250, 100, 50), "AI"))
        {
            ai = 1;
        }

    }

    void CheckWin()
    {
        if (flag > 0)
        {
            return;
        }
        // 遍历所有对象
        for (int i = 0; i < 3; i++)
        {
            if (chess[i][0] != 0 && chess[i][0] == chess[i][1] && chess[i][1] == chess[i][2])
            {
                flag = chess[i][0];
            }
            if (chess[0][i] != 0 && chess[0][i] == chess[1][i] && chess[1][i] == chess[2][i])
            {
                flag = chess[0][i];
            }
        }
        if (chess[0][0] != 0 && chess[0][0] == chess[1][1] && chess[1][1] == chess[2][2])
        {
            flag = chess[0][0];
        }
        if (chess[0][2] != 0 && chess[0][2] == chess[1][1] && chess[1][1] == chess[2][0])
        {
            flag = chess[0][2];
        }
    }

    // Update is called once per frame
    void Update () {
        CheckWin();
        
    }
}

