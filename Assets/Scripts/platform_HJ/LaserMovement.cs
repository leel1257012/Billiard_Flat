using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    BoxCollider2D bc2d;
    // Start is called before the first frame update
    void Start()
    {
        bc2d = gameObject.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var pos = gameObject.transform.position;
        pos.x-=0.05f;
        gameObject.transform.position = pos;
        
        var res = new List<Collider2D>();
        if(bc2d.OverlapCollider(new ContactFilter2D().NoFilter(), res)>0)
        {
            if(!(res.Count==1 && res[0].gameObject.CompareTag("LaserGun")))
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("yes");
        if(!collision.gameObject.CompareTag("LaserGun"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.gameObject.CompareTag("Wind"))
        {
            Destroy(this.gameObject);
        }
    }
}
