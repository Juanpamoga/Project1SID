using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Manager : MonoBehaviour
{
    public GameObject registerCanvas;
    public GameObject loginCanvas;
    public GameObject onlineCanvas;
    public GameObject notificationsCanvas;



    // Start is called before the first frame update
    void Start()
    {
        //registerCanvas = getcomponent<gameobject>();
        loginCanvas.SetActive(true);
    }

    public void LoginSuccesful()
    {
        loginCanvas.SetActive(false);
        onlineCanvas.SetActive(true);
    }

    public void Registration()
    {
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(true);
    }

    public void GoBackToLogin()
    {
        loginCanvas.SetActive(true);
        registerCanvas.SetActive(false);
    }

    public void ShowNotifications()
    {
        notificationsCanvas.SetActive(true);
        onlineCanvas.SetActive(false);
    }

    public void ShowOnlineCanvas()
    {
        onlineCanvas.SetActive(true);
        notificationsCanvas.SetActive(false);
       
    }
}
