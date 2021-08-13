using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingLR : MonoBehaviour
{
    public bool LeftRight; //L = true, R = false; �� ���� �� true�� �����Ǿ������� �������� ���� �ö�
    public float speed;
    public float distance; //������� ������ ������ �Ÿ�. Scene�� �÷����� ��ġ�� �ʱ� ��ġ�� �������� ��
    Vector2 pos0, pos1;

    // Start is called before the first frame update
    void Start()
    {
        if (LeftRight)
        {
            pos0 = new Vector2(transform.position.x - distance / 2, transform.position.y);
            pos1 = new Vector2(transform.position.x + distance / 2, transform.position.y);
        }
        else
        {
            pos0 = new Vector2(transform.position.x + distance / 2, transform.position.y);
            pos1 = new Vector2(transform.position.x - distance / 2, transform.position.y);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, pos0, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, pos0) <= 0.05f)
        {
            Vector2 temp_pos = pos0;
            pos0 = pos1;
            pos1 = temp_pos;
        }
    }
}