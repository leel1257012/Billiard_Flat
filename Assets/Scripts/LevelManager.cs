using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    GameObject spawnPlatform;
    public GameObject[] playerPrefabs;
    public GameObject[] platformPrefabs;
    public GameObject Players;
    public GameObject serialMove;
    public int playerCount;
    public List<FloorType> curPlayers;
    // Start is called before the first frame update
    void Start()
    {
        spawnPlatform = GameObject.Find("SpawnPlatform");
        Vector3 spawn = spawnPlatform.transform.position + new Vector3(0f, 1f, 0f);
        for(int i=0; i<playerCount; i++)
        {
            Instantiate(playerPrefabs[0], spawn, Quaternion.identity, Players.transform);
            //Debug.Log((int)curPlayers[i]);
        }
        serialMove.SetActive(true);
        
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
