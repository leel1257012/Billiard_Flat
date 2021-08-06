using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject spawnPlatform;
    public GameObject[] playersPrefab;
    public GameObject Players;
    public GameObject serialMove;
    public int playerCount;
    // Start is called before the first frame update
    void Start()
    {
        spawnPlatform = GameObject.Find("SpawnPlatform");
        Vector3 spawn = spawnPlatform.transform.position + new Vector3(0f, 1f, 0f);
        for(int i=0; i<playerCount; i++)
        {
            Instantiate(playersPrefab[0], spawn, Quaternion.identity, Players.transform);
        }
        serialMove.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
