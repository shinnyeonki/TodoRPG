using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public InputField EmailInputField;
    public InputField PasswordInputField;

    private string savedEmail = "";
    private string savedPassword = "";

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

        savedEmail = email;
        savedPassword = password;
        Debug.Log($"Account created successfully! Email: {savedEmail}");
    }

    public void Login()
    {
        Debug.Log("Login button clicked!"); // 버튼 동작 확인
        string email = EmailInputField.text;
        string password = PasswordInputField.text;

        if (email == savedEmail && password == savedPassword)
        {
            Debug.Log("Login successful! Loading Friend scene...");
            SceneManager.LoadScene("Friend"); // 씬 이름으로 로드
        }
        else
        {
            Debug.LogError("Invalid email or password!");
        }
    }
}
