using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Database;
using Firebase.Auth;

public class NotificationsCanvas : MonoBehaviour
{
    private DatabaseReference databaseReference;
    private FirebaseAuth auth;

    public TextMeshProUGUI notifications;

    private void Start()
    {
        // Obtener una instancia de FirebaseAuth.
        auth = FirebaseAuth.DefaultInstance;

        // Obtener la referencia de la base de datos.
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Escuchar los cambios en el nodo de las solicitudes de amistad.
        databaseReference.Child("friendRequests").ValueChanged += HandleFriendRequestValueChanged;
    }

    private void HandleFriendRequestValueChanged(object sender, ValueChangedEventArgs e)
    {
        if (e.DatabaseError != null)
        {
            Debug.LogError("Error al leer las solicitudes de amistad: " + e.DatabaseError.Message);
            return;
        }

        if (auth.CurrentUser != null)
        {
            // Obtener el ID del usuario autenticado.
            string userId = auth.CurrentUser.UserId;

            // Obtener la lista de solicitudes de amistad para el usuario actual.
            DataSnapshot snapshot = e.Snapshot;
            if (snapshot.HasChild(userId))
            {
                // Obtener el número de solicitudes de amistad pendientes.
                int friendRequestsCount = (int)snapshot.Child(userId).ChildrenCount;

                // Actualizar el objeto de texto con las notificaciones.
                string notificationText = "Tienes " + friendRequestsCount + " solicitudes de amistad pendientes:\n";
                foreach (DataSnapshot friendRequestSnapshot in snapshot.Child(userId).Children)
                {
                    string senderEmail = friendRequestSnapshot.Child("from").GetValue(true).ToString();
                    notificationText += "- " + senderEmail + " te ha enviado una solicitud de amistad\n";
                }
                notifications.text = notificationText;
            }
            else
            {
                // No hay solicitudes de amistad pendientes para este usuario.
                notifications.text = "No tienes solicitudes de amistad pendientes.";
            }
        }
    }
}
