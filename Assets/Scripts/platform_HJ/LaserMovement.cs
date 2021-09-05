using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    BoxCollider2D bc2d;
    Rigidbody2D rb2D;
    LevelManager levelManager;
    // Start is called before the first frame update
    void Start()
    {
        var speed = 4f;
        var rad = transform.rotation.z * Mathf.PI / 180;
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        bc2d = gameObject.GetComponent<BoxCollider2D>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        rb2D.velocity = transform.right * (-1) * speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var pos = gameObject.transform.position;
        pos.x-=0.05f;
        gameObject.transform.position = pos;
        */
        //if(levelManager.isLaunching) rb2D.velocity = new Vector2(0f,0f);
        //else                         rb2D.velocity = new Vector2(-4f,0f);

        var res = new List<Collider2D>();
        if(bc2d.OverlapCollider(new ContactFilter2D().NoFilter(), res)>0)
        {
            foreach( var colliding in res){
                if(!((colliding.gameObject.CompareTag("LaserGun") || colliding.gameObject.CompareTag("Wind") || HasComponent<PreviousPlayer>(colliding.gameObject))))
                    Destroy(gameObject);
            }
            
        }
        
    }
    bool HasComponent<T> (GameObject obj)
    {
        return obj.GetComponent<T>() != null;
    }
}
