using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingCircle : MonoBehaviour
{
    public bool counterClock; //�ݽð���� = true, �ð���� = false;
    public float speed; //�� ��ȭ �ӵ�
    public float r; //�� �˵� ������
    float angle = 0; //���� �÷����� Center ������ ����� ��ġ�� ���� 0���� ����

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counterClock) angle += speed;
        else angle -= speed;

        if (angle > 360) angle -= 360;
        if (angle < 0) angle += 360;

        transform.position = new Vector2(r * Mathf.Cos(Mathf.Deg2Rad * angle), r * Mathf.Sin(Mathf.Deg2Rad * angle));
    }
}
