using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnGUI()
    {
        GUIStyle style = new GUIStyle
        {
            border = new RectOffset(10, 10, 10, 10),
            fontSize = 20,
            fontStyle = FontStyle.BoldAndItalic,
        };
        // normal:Rendering settings for when the component is displayed normally.
        style.normal.textColor = new Color(200 / 255f, 180 / 255f, 150 / 255f);    // 需要除以255，因为范围是0-1

        if (GUI.Button(new Rect(650, 100, 100, 50), "Reset"))
        {
            Director.GetInstance().CurrentSceneController.LoadSource();
        }
     

        style.normal.textColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);
        GUI.Label(new Rect(40, 10, 200, 50), "Score : " + Singleton<ScoreController>.Instance.GetScore().ToString(), style);
        GUI.Label(new Rect(40, 50, 200, 50), "当前风向 : (" + Singleton<WindController>.Instance.GetDirection().x.ToString() + "," +
            Singleton<WindController>.Instance.GetDirection().y.ToString() + ")", style);
        GUI.Label(new Rect(40, 90, 200, 50), "当前风力强度 : " + Singleton<WindController>.Instance.GetStrength().ToString(), style);



    }

}
