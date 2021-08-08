using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    private LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Platform1(Vector3 pos)
    {
        //Instantiate(platform1,pos,Quaternion.identity);
        FloorType cur = levelManager.curPlayers[0];
        levelManager.curPlayers.RemoveAt(0);
        Instantiate(levelManager.platformPrefabs[(int)cur], pos, Quaternion.identity);
    }
}
