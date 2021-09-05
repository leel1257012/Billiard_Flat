using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LevelButton : MonoBehaviour
{
    public Text level;
    private LevelClear levelCLear;

    private void Start()
    {
        levelCLear = LevelClear.instance;
    }

    public void loadScene()
    {
        //Debug.Log("Level 1-" + level.text);
        SceneManager.LoadScene("Level " + levelCLear.currentStage + "-" + level.text);
    }



}
