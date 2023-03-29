using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginButton : MonoBehaviour
{
    [SerializeField]
    private Button _loginButton;

    [SerializeField]
    private TMP_InputField _emailInputField;
    [SerializeField]
    private TMP_InputField _emailPasswordField;

    private Coroutine _loginCoroutine;

    public event Action<FirebaseUser> OnLoginSucceeded;
    public event Action<string> OnLoginFailed;

    public Canvas_Manager canvas_Manager;


    private void Reset()
    {
        _loginButton = GetComponent<Button>();
        _emailInputField = GameObject.Find("EmailInput").GetComponent<TMP_InputField>();
        _emailPasswordField = GameObject.Find("PasswordInput").GetComponent<TMP_InputField>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _loginButton.onClick.AddListener(HandleLoginButtonClicked);
        canvas_Manager = FindObjectOfType<Canvas_Manager>();
    }

    private void HandleLoginButtonClicked()
    {
        if (_loginCoroutine == null)
        {
            _loginCoroutine = StartCoroutine(LoginCoroutine(_emailInputField.text, _emailPasswordField.text));
        }
    }

    private IEnumerator LoginCoroutine(string email, string password)
    {
        var auth = FirebaseAuth.DefaultInstance;
        var loginTask = auth.SignInWithEmailAndPasswordAsync(email, password);

        yield return new WaitUntil(() => loginTask.IsCompleted);

        if (loginTask.Exception != null)
        {
            Debug.LogWarning($"Login Failed with {loginTask.Exception}");
            OnLoginFailed?.Invoke($"Login Failed with {loginTask.Exception}");
        }
        else
        {
            Debug.Log($"Login succeeded with {loginTask.Result}");
            OnLoginSucceeded?.Invoke(loginTask.Result);
            canvas_Manager.LoginSuccesful();
        }
    }
}
