using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public GameObject registerCanvas;
    public GameObject loginCanvas;
    public GameObject matchmakingCanvas;


    // Start is called before the first frame update
    void Start()
    {
        //registerCanvas = getcomponent<gameobject>();
        loginCanvas.SetActive(true);
    }

    public void LoginSuccesful()
    {
        loginCanvas.SetActive(false);
        matchmakingCanvas.SetActive(true);
    }

    public void Registration()
    {
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(true);
    }

}
