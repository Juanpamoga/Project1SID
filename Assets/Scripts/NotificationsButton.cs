using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsButton : MonoBehaviour
{
    public Canvas_Manager canvas_Manager;

    // Start is called before the first frame update
    void Start()
    {
        canvas_Manager = FindObjectOfType<Canvas_Manager>();
    }

    //public void ContinueToNotifications()
    //{
    //    canvas_Manager.ShowNotifications();
    //}
}
