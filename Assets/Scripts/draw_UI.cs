using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class draw_UI : MonoBehaviour
{
    //GameState
    int GameState = 0; //0 = playing, 1 = pause, 2 = question for backing to stage, 3 = question for restarting ...

    //UI
    public string stage = "TEST STAGE"; //the name of stage
    public int level = 1; //current level of stage
    public Text stageName; //Text instance
    public GameObject ballUI; //UI for drawing the list of balls
    bool ballUI_toggle = true;
    public GameObject pauseButton; //pauseButton instance
    public Image[] img_ball;
    public Sprite[] spr_ball;
    public List<int> arr_ball = new List<int>();
    public GameObject pauseUI;
    public GameObject backToStageUI;
    public GameObject restartUI;

    // Start is called before the first frame update
    void Start()
    {
        //unable UI
        pauseUI.SetActive(false);
        backToStageUI.SetActive(false);
        restartUI.SetActive(false);

        //list of codes corresponding ball sprites
        for (int i = 0; i < 5; i++)
        {
            arr_ball.Add(Random.Range(1, 10));
        }
        BallImageUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        //draw stage and current level
        stageName.text = stage + " - Level " + level.ToString();

        //game playing
        if (GameState == 0)
        {
            //press esc to pause
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }

            //use a current ball
            if (Input.GetMouseButtonUp(0))
            {
                UseCurrentBall();
            }

            //press w to add a ball
            if (Input.GetKeyDown(KeyCode.W))
            {
                AddBall();
            }

            //press u to toggle ballUI
            if (Input.GetKeyDown(KeyCode.U))
            {
                ToggleBallUI();
            }
        }
        else if (GameState == 1) //game pause
        {
            //press esc to resume
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else if (GameState == 2) //backToStage
        {

        } 
        else if (GameState == 3) //restart
        {

        }
    }

    // When player uses a current ball of list, updating the list
    void UseCurrentBall()
    {
        arr_ball.RemoveAt(0);
        if (arr_ball.Count < 5) arr_ball.Add(0); //When the number of balls are less than 5 after shooting ball, none_sprite ball must be added.
        BallImageUpdate();
    }

    void AddBall() //When player get a new ball in game
    {
        arr_ball.Insert(0, Random.Range(1, 10)); // 0 is none_sprite (blank)
        if (arr_ball.LastIndexOf(0) != -1) arr_ball.RemoveAt(arr_ball.LastIndexOf(0));
        BallImageUpdate();
    }

    void BallImageUpdate()
    {
        for (int i = 0; i < 5; i++)
        {
            img_ball[i].sprite = spr_ball[arr_ball[i]];
        }
    }

    void ToggleBallUI()
    {
        if (ballUI_toggle)
        {
            ballUI.SetActive(false);
        }
        else
        {
            ballUI.SetActive(true);
        }
        ballUI_toggle = !ballUI_toggle;
    }

    public void PauseGame()
    {
        if (GameState == 0)
        {
            GameState = 1;
            Time.timeScale = 0;
            //able pause UI
            pauseUI.SetActive(true);
        }
        else if (GameState == 1)
        {
            GameState = 0;
            Time.timeScale = 1;
            //unable pause UI
            pauseUI.SetActive(false);
        }
    }

    public void CloseQuestion()
    {
        if (GameState == 2 || GameState == 3)
        {
            GameState = 1;
            backToStageUI.SetActive(false);
            restartUI.SetActive(false);
        }
    }

    public void QuestionBackToStage()
    {
        if (GameState == 1)
        {
            GameState = 2;
            backToStageUI.SetActive(true);
        }
    }

    public void QuestionRestart()
    {
        if (GameState == 1)
        {
            GameState = 3;
            restartUI.SetActive(true);
        }
    }

    public void Restart()
    {
        if (GameState == 3)
        {
            SceneManager.LoadScene("TestScene");
        }
    }

    public void BackToStage()
    {
        if (GameState == 2)
        {
            SceneManager.LoadScene(2);
        }
    }
}