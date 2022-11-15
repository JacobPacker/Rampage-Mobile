using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;



// ensure UIscript is executed before anything else
[DefaultExecutionOrder(-1)]



public class UIscript : MonoBehaviour
{

    public static UIscript ui;

    // this script must have a high priority in project settings

    StringBuilder sb;
    // Start is called before the first frame update

    private void Awake()
    {
        //print("uiscript awake");
        if (ui == null)
        {
            ui = this;
        }
        else
        {
            if (ui != this)
            {
                Destroy(gameObject);
            }
        }
    }


    void Start()
    {
        sb = new StringBuilder();
    }

    // Update is called once per frame
    void Update()
    {
        ClearGui();
        SetDebugSpeed();
    }


    private void OnGUI()
    {
        string text = sb.ToString();

        // define debug text area
        GUILayout.BeginArea(new Rect(10f, 10f, 1600f, 1600f));
        GUILayout.Label($"<color='white'><size=18>{text}</size></color>");
        GUILayout.EndArea();


    }

    public void ClearGui()
    {
        sb.Clear();
    }

    public void DrawText(string text)
    {
        sb.AppendLine(text);
    }


    void SetDebugSpeed()
    {
        // set speed
        if (Input.GetKeyDown("1"))
            Time.timeScale = 1f;

        if (Input.GetKeyDown("2"))
            Time.timeScale = 0.8f;

        if (Input.GetKeyDown("3"))
            Time.timeScale = 0.6f;

        if (Input.GetKeyDown("4"))
            Time.timeScale = 0.4f;

        if (Input.GetKeyDown("5"))
            Time.timeScale = 0.2f;

        if (Input.GetKeyDown("6"))
            Time.timeScale = 0.1f;

        if (Input.GetKeyDown("7"))
            Time.timeScale = 0.05f;

        if (Input.GetKeyDown("8"))
            Time.timeScale = 0.03f;

        if (Input.GetKeyDown("9"))
            Time.timeScale = 0.01f;

        if (Input.GetKeyDown("0"))
            Time.timeScale = 0.001f;


        string s;
        s = string.Format("Timescale={0}", Time.timeScale );
        UIscript.ui.DrawText(s);



    }
}
