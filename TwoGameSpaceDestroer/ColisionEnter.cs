using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionEnter : MonoBehaviour
{
    [Tooltip("������� ����� ��������� � ��������")][SerializeField] float loadLevelDelay;
    [Tooltip("������� ������")] [SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        GameOver();
    }

    void GameOver()
    {
        //�������� ������� �� ������ ������� ������� ����� �� �������
        SendMessage("OnPlayerDead");
        deathFX.SetActive(true);
        Invoke("RestartLevel", loadLevelDelay);
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(1);
    }


}
