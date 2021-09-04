using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoad : MonoBehaviour
{
    [SerializeField]
    int stage;

    public Button[] lvls;
    LevelClear levelClear;

    // Start is called before the first frame update
    void Start()
    {
        levelClear = LevelClear.instance;
        for(int i=1; i<=6; i++)
        {
            if(!levelClear.isCleared(stage, i))
            {
                lvls[i - 1].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
