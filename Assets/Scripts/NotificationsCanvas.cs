using Firebase.Auth;
using Firebase.Database;
using Firebase;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationsCanvas : MonoBehaviour
{
    public OnlineCanvas onlineCanvas; // referencia al objeto OnlineCanvas
    private const string notificationsNode = "notifications";

    void Start()
    {
        // escucha los cambios en la base de datos de Firebase
        FirebaseDatabase.DefaultInstance
            .GetReference(notificationsNode)
            .Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .ValueChanged += HandleNotificationsValueChanged;
    }

    // método para mostrar las notificaciones
    public void MostrarNotificaciones()
    {
        // recupera la lista de notificaciones pendientes del usuario actual
        FirebaseDatabase.DefaultInstance
            .GetReference(notificationsNode)
            .Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .GetValueAsync().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.LogError("Error al obtener las notificaciones: " + task.Exception);
                }
                else if (task.IsCompleted)
                {
                    var snapshot = task.Result;
                    if (snapshot.Exists)
                    {
                        // muestra un mensaje en la consola para cada notificación pendiente
                        foreach (var notificationSnapshot in snapshot.Children)
                        {
                            var senderEmail = notificationSnapshot.Child("senderEmail").Value.ToString();
                            Debug.Log("Tienes una solicitud de amistad pendiente de: " + senderEmail);
                        }
                    }
                }
            });
    }

    // método que se llama cuando cambia el valor de las notificaciones en la base de datos
    private void HandleNotificationsValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError("Error al obtener las notificaciones: " + args.DatabaseError.Message);
            return;
        }

        // actualiza las notificaciones cuando cambian
        MostrarNotificaciones();
    }
}
