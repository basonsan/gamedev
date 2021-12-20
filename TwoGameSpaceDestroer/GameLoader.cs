using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Invoke("FirstLevelLoad", 5f);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            FirstLevelLoad();
        }
    }

    // Update is called once per frame
    void FirstLevelLoad()
    {
        SceneManager.LoadScene(1);
    }
}
