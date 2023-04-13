using Firebase.Auth;
using Firebase.Database;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineCanvas : MonoBehaviour
{
    public TMP_Text userListText;
    public DatabaseReference databaseReference;

    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        ShowUserList();
    }

    public async void ShowUserList()
    {
        userListText.text = "Loading users...";

        // Obtiene la lista de usuarios de Realtime Database
        var dataSnapshot = await databaseReference.Child("users").GetValueAsync();

        // Loop para la lista de usuarios y obtiene su 'email'
        var users = new List<string>();
        foreach (var userSnapshot in dataSnapshot.Children)
        {
            var email = userSnapshot.Child("email").GetValue(true).ToString();
            users.Add(email);
        }

        // Actualiza la lista de texto 
        userListText.text = string.Join("\n", users);
    }
}
