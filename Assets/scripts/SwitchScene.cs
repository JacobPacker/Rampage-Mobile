using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void SwitchToJhonson()
    {
        SceneManager.LoadScene("DwayneScene");
    }

    public void SwitchToMain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
