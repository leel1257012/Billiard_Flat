using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_Disappearing : MonoBehaviour
{
    public int disappear = 0;
    public float sec = 0;
    public float duration = 2.0f;
    private LevelManager levelManager;
    float alpha = 1.0f;

    void Start()
    {
        levelManager = LevelManager.instance;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (disappear == 1)
        {
            sec = 0;
            disappear++;
        }
        else if (disappear == 2)
        {
            if (!levelManager.isLaunching) { sec += 0.02f; }
            float progress = sec / duration;

            progress = Mathf.Clamp(progress, 0, 1);

            alpha = 1 - progress;
            transform.parent.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);

            if (alpha == 0) Destroy(transform.parent.gameObject);
        }
    }
}