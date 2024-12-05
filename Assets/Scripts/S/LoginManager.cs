using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour
{
    public InputField EmailInputField;
    public InputField PasswordInputField;

    // 여러 계정을 저장하는 Dictionary
    private static Dictionary<string, string> accountDatabase = new Dictionary<string, string>();

    public void CreateAccount()
    {
        Debug.Log("CreateAccount button clicked!"); // 버튼 동작 확인
        string email = EmailInputField.text;
        string password = PasswordInputField.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            Debug.LogError("Email or Password is empty!");
            return;
        }

        if (accountDatabase.ContainsKey(email))
        {
            Debug.LogError("This email is already registered!");
            return;
        }

        accountDatabase[email] = password; // 계정 추가
        Debug.Log($"Account created successfully! Email: {email}");
    }

    public void Login()
    {
        Debug.Log("Login button clicked!"); // 버튼 동작 확인
        string email = EmailInputField.text;
        string password = PasswordInputField.text;

        if (accountDatabase.ContainsKey(email) && accountDatabase[email] == password)
        {
            Debug.Log("Login successful! Loading Friend scene...");
            SceneManager.LoadScene("Main"); // 씬 이름으로 로드
        }
        else
        {
            Debug.LogError("Invalid email or password!");
        }
    }
}
