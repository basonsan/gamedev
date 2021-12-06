using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; //библиотека для переключения уровней

public class Rocket : MonoBehaviour
{
    Rigidbody rigitBody;
    AudioSource audioSource;
    [SerializeField] Text energyText;
    [SerializeField] int energyTotal = 100;
    [SerializeField] int energyApply = 100;
    [SerializeField] float rotSpeed = 100f;
    [SerializeField] float flySpeed = 100f;
    [SerializeField] AudioClip flySound; //добавляем поле звук
    [SerializeField] AudioClip boomSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem flyParticle; //добавляем поле эффектов
    [SerializeField] ParticleSystem finishParticle;
    [SerializeField] ParticleSystem deadParticle;
    bool CheatMode = false;
    enum State {Playing, Dead, NextLevel};
    State state = State.Playing;
    //public float rotSpeed = 100f; можем менять в любом скрипте и самом юнити
    //[SerializeField] float rotSpeed = 100f; можем менять в самом юнити



    // Start is called before the first frame update
    void Start()
    {
        energyText.text = energyTotal.ToString();
        state = State.Playing;
        rigitBody = GetComponent<Rigidbody>(); // подключаем компонент управлением тела
        audioSource = GetComponent<AudioSource>(); //подключаем компонент подключения звука
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Playing && energyTotal > 5 )
        {
            RotateRocket();
            LaunchRocket();
        }
        if (Debug.isDebugBuild) //проверка что это дебаг билд, на релизе это работать не будет (если не выбрано)
        {
            if (Input.GetKey(KeyCode.C)) {
                CheatMode = !CheatMode;
            };
            if(Input.GetKey(KeyCode.L)) {
                LoadNextLevel();
            };
        }
        
    }

    void LaunchRocket()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            energyTotal -= Mathf.RoundToInt(energyApply * Time.deltaTime); //окргулгяем и конвертируем в инт
            energyText.text = energyTotal.ToString(); //конвертируем в строку
            float flyionSpeed = flySpeed * Time.deltaTime; //убираем привязку к фпс
            rigitBody.AddRelativeForce(Vector3.up * flyionSpeed); //Передвигаем по оси Z
            if (!audioSource.isPlaying) //проверка на воспроизведение звука
                //audioSource.Play(); //воспроизводим звук прикрепленный к объекту
                audioSource.PlayOneShot(flySound);
            flyParticle.Play();
        }
        else
        {
            audioSource.Pause(); //приостанавливаем звук
            flyParticle.Stop();
        }
        
    }
    void RotateRocket()
    {
        float rotarionSpeed = rotSpeed * Time.deltaTime; //убираем привязку к фпс
        rigitBody.freezeRotation = true; //для удаления бага с вхождением в штопор
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //transform.Rotate(Vector3.forward);
            //transform.Rotate(new Vector3(0, 0, 1));
            transform.Rotate(Vector3.forward * rotarionSpeed);//поворачиваем ракету
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //transform.Rotate(-Vector3.forward);
            //transform.Rotate(new Vector3(0,0,-1));
            transform.Rotate(-Vector3.forward * rotarionSpeed); //поворачиваем ракету
        }
        rigitBody.freezeRotation = false;
    }

    private void OnCollisionEnter(Collision collision) //функция для определения соприкосновения ракеты
    {
        if (state != State.Playing || CheatMode)
        {
            return; //останавливаем функцию
        }
        switch(collision.gameObject.tag) //получаем тег с которым произошло соприкосновение
        {
            case "Friendly":
                break;
            case "Battary":
                PlusEnergy(360, collision.gameObject);
                break;
            case "Finish":
                Finish();
                break;
            default:
                Lose();
                break;
        }
    }

    void PlusEnergy(int energyToAdd, GameObject batteryObj)
    {
        batteryObj.GetComponent<BoxCollider>().enabled = false; //отключаем бокс колайдер
        energyTotal += energyToAdd;
        energyText.text = energyTotal.ToString();
        Destroy(batteryObj);
        if (energyTotal < 5)
        {
            Lose();
        }
    }

    void Finish()
    {
        state = State.NextLevel;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        flyParticle.Stop();
        finishParticle.Play();
        Invoke("LoadNextLevel", 2f);
    }

    void Lose()
    {
        state = State.Dead;
        audioSource.Stop();
        audioSource.PlayOneShot(boomSound);
        flyParticle.Stop();
        deadParticle.Play();
        Invoke("LoadFirstLevel", 2f);
    }
    void LoadNextLevel ()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        int countLevel = SceneManager.sceneCountInBuildSettings;
        print(nextLevelIndex);
        print(countLevel);
        if (nextLevelIndex == countLevel)
        {
            nextLevelIndex = 1;
        }
        SceneManager.LoadScene(nextLevelIndex);
    }

    void LoadFirstLevel()
    {
        int nextLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nextLevelIndex);
    }
}
