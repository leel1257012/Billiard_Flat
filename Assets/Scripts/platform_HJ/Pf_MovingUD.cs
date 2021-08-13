using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pf_MovingUD : MonoBehaviour
{
    public bool UpDown; //up = true, down = false; �� ���� �� true�� �����Ǿ������� �������� ���� �ö�
    public float speed;
    public float distance; //������� ������ ������ �Ÿ�. Scene�� �÷����� ��ġ�� �ʱ� ��ġ�� �������� ��
    Vector2 pos0, pos1;

    // Start is called before the first frame update
    void Start()
    {
        if (UpDown)
        {
            pos0 = new Vector2(transform.position.x, transform.position.y + distance / 2);
            pos1 = new Vector2(transform.position.x, transform.position.y - distance / 2);
        }
        else
        {
            pos0 = new Vector2(transform.position.x, transform.position.y - distance / 2);
            pos1 = new Vector2(transform.position.x, transform.position.y + distance / 2);
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
