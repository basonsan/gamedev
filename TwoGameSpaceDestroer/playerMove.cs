using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;
//using UnityStandardAssets;

public class playerMove : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("General setting")]
    //для новой системы управления
    //[SerializeField] InputAction movement;
    //добавляем подсказку при наведении
    [Tooltip("м/с")] [SerializeField] float Speed = 12f;
    [SerializeField] float XClam = 12f;
    [SerializeField] float YClam = 8f;
    [SerializeField] GameObject[] guns;
    [Header("Rotation setting")]
    [SerializeField] float xRotFactor = 2.37f;
    [SerializeField] float yRotFactor = 2f;
    [SerializeField] float xMoveRot = 0;
    [SerializeField] float yMoveRot = 0;
    [SerializeField] float zMoveRot = 0;
    float xMove, yMove;
    bool isControlEnabled = true;
    Rigidbody rigitBody;
    void Start()
    {
        rigitBody = GetComponent<Rigidbody>();
    }
    //для использования новой способа управления надо включать movement
    /*void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            MoveShip();
            RotateShip();
            FireGuns();
        }
    }

    void OnPlayerDead()
    {
        isControlEnabled = false;
        rigitBody.isKinematic = false;
    } 

    void MoveShip ()
    {
        //xMove = movement.ReadValue<Vector2>().x;
        //yMove = movement.ReadValue<Vector2>().y;
        //старая система управления
        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");
        
        //получаем сдвиг с учетом скорости корабля и количества кадров в секунду:
        float xOffset = xMove * Speed * Time.deltaTime;
        float yOffset = yMove * Speed * Time.deltaTime;
        //получаем новую позицию корабля:
        float newXPos = transform.localPosition.x + xOffset;
        float newYPos = transform.localPosition.y + yOffset;
        //ограничиваем позицию корабля
        float clamXPos = Mathf.Clamp(newXPos, -XClam, XClam);
        float clamYPos = Mathf.Clamp(newYPos, -YClam, YClam);
        //выставляем новую позицию коробля
        transform.localPosition = new Vector3(clamXPos, clamYPos, transform.localPosition.z);
    }

    void RotateShip ()
    {
        //добавляем угол поворота + дополнительный поворот во время движения
        float xRot = transform.localPosition.y * xRotFactor + yMove * yMoveRot;
        float yRot = transform.localPosition.x * yRotFactor + xMove * xMoveRot;
        //добавляем кручение *бочка* при движение по горизонтали
        float zRot = xMove * zMoveRot;

        transform.localRotation = Quaternion.Euler(xRot, yRot, zRot);
    }

    void FireGuns()
    {
        //если нажата клавиша огонь1
        if(Input.GetButton("Fire1"))
        {
            ActiveGuns();
        } else
        {
            DeactiveGuns();
        }
    }

    void ActiveGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(true);
        }
    }

    void DeactiveGuns()
    {
        foreach (GameObject gun in guns)
        {
            gun.SetActive(false);
        }
    }
}

