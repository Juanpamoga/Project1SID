using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using System;

public class UsernameLabel : MonoBehaviour
{

    [SerializeField]
    private Text _label;

    private void Reset()
    {
        _label = GetComponent<Text>();
    }
    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    private void HandleAuthChange(object sender, EventArgs e)
    {
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

        string username;
        if(currentUser == null)
        {
            username = "NULL";
        }
        else
        {
            username = currentUser.UserId;
        }

        _label.text = username;

        string name = currentUser.DisplayName;
        string email = currentUser.Email;

        Debug.Log("Email:" + email);


    }

 
}
