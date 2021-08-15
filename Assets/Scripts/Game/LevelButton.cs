using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelButton : MonoBehaviour
{
    public Text level;

    public void loadScene()
    {
        //Debug.Log("Level 1-" + level.text);
        SceneManager.LoadScene("Level 1-" + level.text);
    }

    public void loadlv5()
    {
        SceneManager.LoadScene("Sample Level 1");
    }
    public void loadlv6()
    {
        SceneManager.LoadScene("Sample Level 2");
    }



}
