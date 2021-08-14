using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingCircle : MonoBehaviour
{
    public bool counterClock; //�ݽð���� = true, �ð���� = false;
    public float speed; //�� ��ȭ �ӵ�
    public float r; //�� �˵� ������
    float angle = 0; //���� �÷����� Center ������ ����� ��ġ�� ���� 0���� ����
    private Vector2 init;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = LevelManager.instance;
        init = new Vector2(transform.position.x, transform.position.y);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!levelManager.isLaunching)
        {
            if (counterClock) angle += speed;
            else angle -= speed;

            if (angle > 360) angle -= 360;
            if (angle < 0) angle += 360;

            transform.position = new Vector2(init.x + r * Mathf.Cos(Mathf.Deg2Rad * angle), init.y + r * Mathf.Sin(Mathf.Deg2Rad * angle));
        }

    }
}
