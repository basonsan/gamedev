using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() //функция для переноса объекта в другие сцены
    {

        int numPlayMusic = FindObjectsOfType<PlayMusic>().Length;
        if (numPlayMusic > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject); //добавляем объект в контейнер для переноса
        }
        
    }
    
}
