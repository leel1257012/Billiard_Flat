using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    draw_UI draw_UI;

    GameObject spawnPlatform;
    public GameObject[] playerPrefabs;
    public GameObject[] platformPrefabs;
    public GameObject Players;
    public GameObject serialMove;
    public int playerCount;
    public List<FloorType> curPlayers;
    public GameObject gameUI;
    public int totalPlayers;
    public bool isLaunching;
    public bool starCollision;
    public bool timerActive = true;
    public bool gameOver = false;

    float delayTime;

    public bool testMode;
    // Start is called before the first frame update
    void Start()
    {
        playerCount = curPlayers.Count;
        totalPlayers = playerCount;
        spawnPlatform = GameObject.Find("SpawnPlatform");
        draw_UI = GameObject.Find("GameManager").GetComponent<draw_UI>();
        Vector3 spawn = spawnPlatform.transform.position + new Vector3(0f, 1f, 0f);
        for(int i=0; i<playerCount; i++)
        {
            Instantiate(playerPrefabs[(int)curPlayers[i]], spawn, Quaternion.identity, Players.transform);
            //Debug.Log((int)curPlayers[i]);
        }
        gameUI.SetActive(true);
        serialMove.SetActive(true);
        
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //게암오버
        if (gameOver)
        {
            delayTime += Time.deltaTime;
            if (delayTime >= 3)
            {
                draw_UI.GameOver(false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}
