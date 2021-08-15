using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    private LevelManager levelManager;
    SerialMovement move;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = LevelManager.instance;
        move = GameObject.Find("SerialMoving").GetComponent<SerialMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Platform1(Vector3 pos)
    {

        if (levelManager.starCollision)
        {
            FloorType cur = levelManager.curPlayers[levelManager.curPlayers.Count - 1];
            Instantiate(levelManager.platformPrefabs[(int)cur], pos, Quaternion.identity);
            levelManager.curPlayers.RemoveAt(levelManager.curPlayers.Count - 1);
            move.camera.Player = move.Players[move.top];
        }
        else
        {
            FloorType cur = levelManager.curPlayers[levelManager.curPlayers.Count - 1];
            Instantiate(levelManager.platformPrefabs[(int)cur], pos, Quaternion.identity);
            levelManager.curPlayers.RemoveAt(levelManager.curPlayers.Count - 1);
            move.camera.Player = move.Players[move.top];
        }
    }
}
