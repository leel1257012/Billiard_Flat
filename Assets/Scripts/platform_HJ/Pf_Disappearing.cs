using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_Disappearing : MonoBehaviour
{
    public int disappear = 0;
    public float startTime;
    public float duration = 2.0f;
    float alpha = 1.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (disappear == 1)
        {
            startTime = Time.time;
            disappear++;
        }
        else if (disappear == 2)
        {
            float progress = (Time.time - startTime) / duration;

            progress = Mathf.Clamp(progress, 0, 1);

            alpha = 1 - progress;
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, alpha);

            if (alpha == 0) Destroy(gameObject);
        }
    }
}