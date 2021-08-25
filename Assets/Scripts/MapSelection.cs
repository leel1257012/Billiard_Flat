using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    public Text curr_stage;
    public Image[] Platforms;
    public Image[] Players;
    public Image[] ClearMarks;
    public int stage = 1;
    public int stageGroup = 0;
    public int maxStage = 8;
    public List<int> clearedStage;

    // Start is called before the first frame update
    void Start()
    {
        DisablePlat();
    }

    // Update is called once per frame
    void Update()
    {
        curr_stage.text = "STAGE " + stage.ToString();

        if (Input.GetKeyDown(KeyCode.A))
        {
            if (stage > 1) stage--;
            DisablePlat();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (stage < maxStage) stage++;
            DisablePlat();
        }

        switch(stage % 4)
        {
            case 1:
                Platforms[0].color = new Color(1, 1, 1, 1);
                ClearMarks[0].color = new Color(1, 1, 0, 1);
                Players[0].gameObject.SetActive(true);
                break;
            case 2:
                Platforms[1].color = new Color(1, 1, 1, 1);
                ClearMarks[1].color = new Color(1, 1, 0, 1);
                Players[1].gameObject.SetActive(true);
                break;
            case 3:
                Platforms[2].color = new Color(1, 1, 1, 1);
                ClearMarks[2].color = new Color(1, 1, 0, 1);
                Players[2].gameObject.SetActive(true);
                break;
            default:
                Platforms[3].color = new Color(1, 1, 1, 1);
                ClearMarks[3].color = new Color(1, 1, 0, 1);
                Players[3].gameObject.SetActive(true);
                break;
        }
    }

    void DisablePlat()
    {
        stageGroup = (stage-1) / 4;
        for (int i = 0; i < 4; i++)
        {
            Platforms[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            ClearMarks[i].color = new Color(0.5f, 0.5f, 0.5f, 0.5f);
            if (clearedStage.Contains(stageGroup * 4 + i + 1))
            {
                ClearMarks[i].gameObject.SetActive(true);
            } else {
                ClearMarks[i].gameObject.SetActive(false);
            }
            Players[i].gameObject.SetActive(false);
        }
    }
}
