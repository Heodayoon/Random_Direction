using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor;
using UnityEngine;

public class PatrolMonster : MonoBehaviour
{
    public float speed = 2f;
    public float detectionRange = 0.1f;
    public Vector2 areaMin = new Vector2(-5, -5);
    public Vector2 areaMax = new Vector2(5, 5);
    public Transform player;
    private float pie = 3.1415f;

    private Vector2 direction;

    private void Start()
    {
        RandomDirection();
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, player.position) <= detectionRange) // 범위 안이면 사망 로그
        {
            seeingPlayer();
        }

        MonsterMove(); // 움직임

        if (!isInside((Vector2)transform.position)) //몬스터의 위치가 벽이면 RandomDirection 실행 
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
        float[] angles = new float[] { 0.0f, 45.0f, 90.0f, 135.0f, 180.0f, 225.0f, 270.0f, 315.0f }; 
        // 몬스터가 갈 수 있는 방향 각도를 정의
        Vector2[] validDirections = new Vector2[angles.Length];

        int directionCount = 0;

        foreach (float angle in angles) // 모든 angles를 루프
        {
            float radian = angle * (pie / 180); // 모든 각을 라디안 값으로 변경 mathf계산을 위해.

            Vector2 dir = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)); 
            // 단위 원 기준 Vector2의 x좌표는 cos세타 값, y좌표는 sin세타 값새로운 방향 x,y값 대입.

            Vector2 nextPos = (Vector2)transform.position + dir; // 현재 위치에서 dir값을 더해서 nextPos에 저장

            if (isInside(nextPos)) // 유효한 방향 nextPos이면 validDirections에 저장
            { 
                validDirections[directionCount] = dir; 
                directionCount++;
            }
        }

        int index = Random.Range(0, validDirections.Length); // 유효한 방향 중 랜덤으로 선택
        direction = validDirections[index].normalized; // 선택된 방향을 설정
    }

    bool isInside(Vector2 pos) // x 와 y축을 기준으로 범위를 넘어가는 것을 판단
    {
        if (pos.x >= areaMin.x && pos.x <= areaMax.x && pos.y >= areaMin.y && pos.y <= areaMax.y)
        {
            return true;
        }
        else return false;
    }

    void seeingPlayer() // 1f 거리 안이면 사망
    {
        Debug.Log("플레이어 사망 게임 종료");
        EditorApplication.isPlaying = false;
    }
}
