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
        if (Vector2.Distance(transform.position, player.position) <= detectionRange) // ���� ���̸� ��� �α�
        {
            seeingPlayer();
        }

        MonsterMove(); // ������

        if (!isInside((Vector2)transform.position)) //������ ��ġ�� ���̸� RandomDirection ���� 
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
        // ���Ͱ� �� �� �ִ� ���� ������ ����
        Vector2[] validDirections = new Vector2[angles.Length];

        int directionCount = 0;

        foreach (float angle in angles) // ��� angles�� ����
        {
            float radian = angle * (pie / 180); // ��� ���� ���� ������ ���� mathf����� ����.

            Vector2 dir = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)); 
            // ���� �� ���� Vector2�� x��ǥ�� cos��Ÿ ��, y��ǥ�� sin��Ÿ �����ο� ���� x,y�� ����.

            Vector2 nextPos = (Vector2)transform.position + dir; // ���� ��ġ���� dir���� ���ؼ� nextPos�� ����

            if (isInside(nextPos)) // ��ȿ�� ���� nextPos�̸� validDirections�� ����
            { 
                validDirections[directionCount] = dir; 
                directionCount++;
            }
        }

        int index = Random.Range(0, validDirections.Length); // ��ȿ�� ���� �� �������� ����
        direction = validDirections[index].normalized; // ���õ� ������ ����
    }

    bool isInside(Vector2 pos) // x �� y���� �������� ������ �Ѿ�� ���� �Ǵ�
    {
        if (pos.x >= areaMin.x && pos.x <= areaMax.x && pos.y >= areaMin.y && pos.y <= areaMax.y)
        {
            return true;
        }
        else return false;
    }

    void seeingPlayer() // 1f �Ÿ� ���̸� ���
    {
        Debug.Log("�÷��̾� ��� ���� ����");
        EditorApplication.isPlaying = false;
    }
}
