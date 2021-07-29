using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb2D;
    static float MaxSpeed = 5.0f; // �ִ� �ӷ� ����
    public float JumpForce = 20.0f; // ���� ���� ����

    // Start is called before the first frame update
    void Start()
    {
        this.rb2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Ű �Է� (A, D)
        float h = Input.GetAxisRaw("Horizontal");
        
        // �¿� �̵�
        if (h > 0 && rb2D.velocity.x < 0) rb2D.velocity = new Vector2(-rb2D.velocity.x, rb2D.velocity.y);
        else if (h < 0 && rb2D.velocity.x > 0) rb2D.velocity = new Vector2(-rb2D.velocity.x, rb2D.velocity.y);
        
        rb2D.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        // �ӵ� ����
        if (rb2D.velocity.x > MaxSpeed)
            rb2D.velocity = new Vector2(MaxSpeed, rb2D.velocity.y);

        else if (rb2D.velocity.x < -MaxSpeed)
            rb2D.velocity = new Vector2(-MaxSpeed, rb2D.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ű(A, D) ���� ����
        if (Input.GetButtonUp("Horizontal"))
        {
            rb2D.AddForce(new Vector2(-rb2D.velocity.x, 0), ForceMode2D.Impulse);
        }

        // ����
        if (!isJumping())
        {
            if (Input.GetKeyDown(KeyCode.Space))
                rb2D.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
        }
    }

    bool isJumping()
    {
        if (rb2D.velocity.y == 0) return false;
        else return true;
    }
}
