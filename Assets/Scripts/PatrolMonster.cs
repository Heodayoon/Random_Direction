using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : MonoBehaviour
{
    // ������ ���ǵ�� 2f �÷��̾���� �Ÿ��� 3f �����̸� seeinplayer �Լ��� ���� ���� �α׸� ��쵵����. ���߿� �߰� ����.
    // ���ʹ� �¿���ϸ� ������ 8���� ������ �߰��ؼ� ������ ���ؼ� Ư�� �ε����� �����ؼ� �� �������� �̵��ϵ�����
    // �׸��� ���Ͱ� �̵��� �� �̸� ������ ���� min , max�� �ƴ� ��츦 if���� ���� �Ǵ��� �ƴ϶�� �ٽ� RandomDirection�Լ��� ����

    public float speed = 2f;
    public float detectionRange = 3f;
    public Vector2 areaMin = new Vector2(-10, -10);
    public Vector2 areaMax = new Vector2(10, 10);
    public Transform player;

    private Vector2 direction;

    private void Start()
    {
        RandomDirection();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            SeeingPlayer();
        }

        MonsterMove();

        if (!IsInside((Vector2)transform.position))
        {
            RandomDirection();
        }
    }

    void MonsterMove()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void RandomDirection()
    {
        Vector2[] directions = new Vector2[]
        {
        new Vector2(1, 0),
        new Vector2(1, 1),
        new Vector2(0, 1),
        new Vector2(-1, 1),
        new Vector2(-1, 0),
        new Vector2(-1, -1),
        new Vector2(0, -1),
        new Vector2(1, -1),
        };

        int index = Random.Range(0, directions.Length);
        direction = directions[index].normalized;
    }


    bool IsInside(Vector2 pos)
    {
        return pos.x >= areaMin.x && pos.x <= areaMax.x &&
               pos.y >= areaMin.y && pos.y <= areaMax.y;
    }
    void SeeingPlayer()
    {
        Debug.Log("���� ����");
    }
}