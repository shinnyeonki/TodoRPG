using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LoginManager : MonoBehaviour
{
    public InputField EmailInputField;
    public InputField PasswordInputField;

    // 간단한 계정 데이터베이스 예시
    private static Dictionary<string, string> accountDatabase = new Dictionary<string, string>();

    public void CreateAccount()
    {
        Debug.Log("CreateAccount button clicked!");
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
        Debug.Log("Login button clicked!");
        string email = EmailInputField.text;
        string password = PasswordInputField.text;

        if (accountDatabase.ContainsKey(email) && accountDatabase[email] == password)
        {
            Debug.Log("Login successful! Loading Main scene...");

            // 여기서 GameManager에 로그인된 계정 정보 등록
            if (GameManager.gm != null)
            {
                GameManager.gm.SetLoggedInEmail(email);
            }
            else
            {
                Debug.LogWarning("GameManager가 존재하지 않습니다! GameManager를 씬에 추가했는지 확인하세요.");
            }

            SceneManager.LoadScene("Main"); // 씬 로드
        }
        else
        {
            Debug.LogError("Invalid email or password!");
        }
    }
}
