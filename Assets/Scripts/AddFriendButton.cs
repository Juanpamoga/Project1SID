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
using System.Linq;

public class AddFriendButton : MonoBehaviour
{
    public TMP_InputField friendNameInputField;
    public OnlineCanvas onlineCanvas;

    private DatabaseReference databaseReference;

    private void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public async void OnClick()
    {
        Debug.Log("Click");
        string friendEmail = friendNameInputField.text;

        // Busca al usuario en la base de datos de Firebase
        var dataSnapshot = await databaseReference.Child("users").OrderByChild("email").EqualTo(friendEmail).GetValueAsync();

        if (dataSnapshot.HasChildren)
        {
            // Envía una solicitud de amistad al usuario encontrado
            string currentUserEmail = FirebaseAuth.DefaultInstance.CurrentUser.Email;
            string friendUid = dataSnapshot.Children.First().Key;

            var friendRequest = new Dictionary<string, object>();
            friendRequest["from"] = currentUserEmail;
            friendRequest["to"] = friendEmail;
            friendRequest["status"] = "pending";

            await databaseReference.Child("friendRequests").Child(friendUid).Push().SetValueAsync(friendRequest);

            Debug.Log("Solicitud de amistad enviada a: " + friendEmail);
        }
        else
        {
            // Muestra un mensaje de error si el usuario no se encuentra en la base de datos
            Debug.Log("Solicitud inválida, usuario no encontrado: " + friendEmail);
        }
    }
}
