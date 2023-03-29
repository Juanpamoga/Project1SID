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
    [SerializeField]
    private TextMeshProUGUI _userListText;

    private void Start()
    {

        ShowUserList();
    }

    private async void ShowUserList()
    {

        DatabaseReference databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        // Retrieve the user list from the "users" node
        DataSnapshot snapshot = await databaseRef.Child("users").GetValueAsync();
        Dictionary<string, object> users = (Dictionary<string, object>)snapshot.Value;

        // Display the list of user emails in the onlineCanvas
        string userList = "";
        foreach (KeyValuePair<string, object> user in users)
        {
            string email = (string)user.Value;
            userList += "- " + email + "\n";
        }
        _userListText.text = userList;
    }
}
