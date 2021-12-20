using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int hits = 2;
    //так работает но есть другой способ :
    //[SerializeField] GameObject score;
    Score scoreBoard;
    bool isDead = false;

    void Start()
    {
        scoreBoard = FindObjectOfType<Score>();
    }

    // добавляем компонент (оставлю для образца)
    void AddNoneTriggerCollider()
    {
        //создаем колайдер и присваиваем
        Collider boxColider = gameObject.AddComponent<BoxCollider>();
        //выключаем у колайдера тригер
        boxColider.isTrigger = false;
        
    }

    //метод для определения соприкосновения партикла с объектом
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
            //получаем названия объекта 
            print("Colide " + gameObject.name);
            //добавляем префаб deathFX на сцену с позицией объекта и с 0 вращением
            GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            gameObject.AddComponent<Rigidbody>();
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        
        //уничтожаем объект
        //Destroy(gameObject, 1f);
    }
}
