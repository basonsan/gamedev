using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionEnter : MonoBehaviour
{
    [Tooltip("Задерка перед загрузкой в секундах")][SerializeField] float loadLevelDelay;
    [Tooltip("Партикл взрыва")] [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        GameOver();
    }

    void GameOver()
    {
        //вызываем функцию из любого скрипта который висит на объекте
        SendMessage("OnPlayerDead");
        deathFX.SetActive(true);
        Invoke("RestartLevel", loadLevelDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }


}
