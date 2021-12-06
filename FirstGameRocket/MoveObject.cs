using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent] //нельзя добавлять компонент множество раз на один компонент
public class MoveObject : MonoBehaviour
{
    [SerializeField] Vector3 movePosition; // добавляем поле для позиции
    [SerializeField] [Range(0,1)] float moveProgress; // добавляем поле с ползунком от 0 до 1
    [SerializeField] [Range(0, 1)] float moveSpeed;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position; //получаем начальную позицию
    }

    // Update is called once per frame
    void Update()
    {
        moveProgress = Mathf.PingPong(Time.time * moveSpeed, 1);
        Vector3 offset = movePosition * moveProgress; //создаем смещение
        transform.position = startPosition + offset; //задаем новую позицию
    }
}
