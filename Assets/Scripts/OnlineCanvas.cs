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
    public TMP_Text onlineUserListText;
    public DatabaseReference databaseReference;
    private FirebaseAuth auth;
    private FirebaseUser user;

    void Start()
    {
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        auth = FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
        if (user != null)
        {
            AddUserToOnlineList(user.Email);
        }
        ShowUserList();
        ShowOnlineUserList();
    }

    public async void ShowUserList()
    {
        userListText.text = "Loading users...";

        var dataSnapshot = await databaseReference.Child("users").GetValueAsync();

        var users = new List<string>();
        foreach (var userSnapshot in dataSnapshot.Children)
        {
            var email = userSnapshot.Child("email").GetValue(true).ToString();
            users.Add(email);
        }

        userListText.text = string.Join("\n", users);
    }

    private void AddUserToOnlineList(string email)
    {
        databaseReference.Child("Online Users").Child(auth.CurrentUser.UserId).SetValueAsync(email);
    }

    private void RemoveUserFromOnlineList()
    {
        databaseReference.Child("Online Users").Child(auth.CurrentUser.UserId).RemoveValueAsync();
    }

    public void ShowOnlineUserList()
    {
        databaseReference.Child("Online Users").ValueChanged += HandleOnlineUserListChanged;
    }

    private void HandleOnlineUserListChanged(object sender, ValueChangedEventArgs args)
    {
        var users = new List<string>();
        foreach (var userSnapshot in args.Snapshot.Children)
        {
            var email = userSnapshot.GetValue(true).ToString();
            users.Add(email);
        }

        onlineUserListText.text = string.Join("\n", users);
    }

    void OnDestroy()
    {
        if (user != null)
        {
            RemoveUserFromOnlineList();
        }
    }
}
