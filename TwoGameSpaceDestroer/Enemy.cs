using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int hits = 2;
    //��� �������� �� ���� ������ ������ :
    //[SerializeField] GameObject score;
    Score scoreBoard;
    bool isDead = false;

    void Start()
    {
        scoreBoard = FindObjectOfType<Score>();
    }

    // ��������� ��������� (������� ��� �������)
    void AddNoneTriggerCollider()
    {
        //������� �������� � �����������
        Collider boxColider = gameObject.AddComponent<BoxCollider>();
        //��������� � ��������� ������
        boxColider.isTrigger = false;
        
    }

    //����� ��� ����������� ��������������� �������� � ��������
    private void OnParticleCollision(GameObject other)
    {
        DeadEnemy(10);

    }

    private void OnCollisionEnter(Collision collision)
    {
        hits = 0;
        DeadEnemy(50);
    }

    void DeadEnemy(int scoreDestroy)
    {
        hits--;
        if (!isDead && hits <=0)
        {
            print("Dead");
            isDead = true;
            //score.GetComponent<Score>().ScoreHit(scoreDestroy);
            scoreBoard.ScoreHit(scoreDestroy);
            //�������� �������� ������� 
            print("Colide " + gameObject.name);
            //��������� ������ deathFX �� ����� � �������� ������� � � 0 ���������
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        
        //���������� ������
        //Destroy(gameObject, 1f);
    }
}
