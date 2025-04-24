using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMonster : MonoBehaviour
{
    // 몬스터의 스피드는 2f 플레이어와의 거리가 3f 이하이면 seeinplayer 함수를 통해 전투 로그를 띄우도록함. 나중에 추가 구현.
    // 몬스터는 좌우상하를 포함해 8가지 방향을 추가해서 랜덤을 통해서 특정 인덱스에 접근해서 그 방향으로 이동하도록함
    // 그리고 몬스터가 이동할 때 미리 지정해 놓은 min , max가 아닐 경우를 if문을 통해 판단해 아니라면 다시 RandomDirection함수를 실행

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
        Debug.Log("전투 시작");
    }
}