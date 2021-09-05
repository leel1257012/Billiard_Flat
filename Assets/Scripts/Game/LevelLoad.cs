using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    [SerializeField]
    int stage;


    public Button[] lvls;
    LevelClear levelClear;
    Color32 clear = new Color32(71, 195, 38, 255);

    // Start is called before the first frame update
    void Start()
    {
        levelClear = LevelClear.instance;
        levelClear.currentStage = stage;
        if (!levelClear.admin)
        {
            for (int i = 1; i <= 6; i++)
            {
                if (!levelClear.isCleared(stage, i))
                {
                    lvls[i - 1].interactable = false;
                }
                else
                {
                    lvls[i - 1].interactable = true;
                    lvls[i - 1].image.color = clear;
                }
            }
            for (int i = 1; i <= 6; i++)
            {
                if (!levelClear.isCleared(stage, i))
                {
                    lvls[i - 1].interactable = true;
                    lvls[i - 1].image.color = new Color32(0, 0, 0, 0);
                    break;
                }
            }
        }


    }

    public void ClearLevel()
    {

    }

    public void SelectLevel(int level)
    {
        levelClear.currentLevel = level;
    }

    public void StageScene()
    {
        SceneManager.LoadScene("MapSelect 1");
    }
}
