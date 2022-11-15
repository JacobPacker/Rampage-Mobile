using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager lm;

    
    void Awake()
    {
        // make a static reference to the levelmanager for all gameobjects to access
        lm = GetComponent<LevelManager>();

        // initialise the debug UI
        GameObject uiObject = new GameObject("UI");
        uiObject.AddComponent<UIscript>();

    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
