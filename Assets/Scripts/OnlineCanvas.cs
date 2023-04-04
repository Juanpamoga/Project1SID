using Firebase.Auth;
using Firebase.Database;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnlineCanvas : MonoBehaviour
{
    public TMP_Text userListText;
    public DatabaseReference databaseReference;
    public GameObject addFriendButtonPrefab; // reference to the AddFriend_Button prefab
    public Transform buttonsContainer; // reference to the container that will hold the buttons
    public float buttonSpacing = 5f; // spacing between each button in the list
    private const float buttonHeight = 50f;
    private float buttonPadding = 10f;



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

        // Instancia un botón para cada usuario
        for (int i = 0; i < users.Count; i++)
        {
            var button = Instantiate(addFriendButtonPrefab, buttonsContainer.transform);
            // Setea la posición del botón en base al índice de usuario
            var buttonTransform = button.GetComponent<RectTransform>();
            buttonTransform.anchoredPosition = new Vector2(0, -buttonHeight * buttonSpacing * i - buttonPadding * i);
        }

        // Actualiza la lista de texto 
        userListText.text = string.Join("\n", users);
    }
}
