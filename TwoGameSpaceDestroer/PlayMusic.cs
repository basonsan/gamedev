using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake() //������� ��� �������� ������� � ������ �����
    {

        int numPlayMusic = FindObjectsOfType<PlayMusic>().Length;
        if (numPlayMusic > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(gameObject); //��������� ������ � ��������� ��� ��������
        }
        
    }
    
}
