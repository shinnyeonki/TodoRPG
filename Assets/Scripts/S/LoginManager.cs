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

        // 계정 정보를 PlayerPrefs에 저장
        PlayerPrefs.SetString($"Account_{email}", password);

        Debug.Log($"Account created successfully! Email: {email}");
    }


    public void Login()
    {
        Debug.Log("Login button clicked!");
        string email = EmailInputField.text;
        string password = PasswordInputField.text;

        if (PlayerPrefs.HasKey($"Account_{email}"))
        {
            string storedPassword = PlayerPrefs.GetString($"Account_{email}");
            if (storedPassword == password)
            {
                Debug.Log("Login successful! Loading Main scene...");

                if (GameManager.gm != null)
                {
                    GameManager.gm.SetLoggedInEmail(email);
                    GameManager.gm.LoadUserData(); // 기존 사용자 데이터 로드
                    Debug.Log("로그인 후 사용자 데이터 로드 완료.");
                }
                else
                {
                    Debug.LogError("GameManager가 존재하지 않습니다. GameManager를 씬에 추가했는지 확인하세요.");
                }

                SceneManager.LoadScene("Main");
            }
            else
            {
                Debug.LogError("Invalid password!");
            }
        }
        else
        {
            Debug.LogError("Account does not exist! 새로운 계정을 생성합니다.");

            // 새로운 계정을 위한 기본값 설정
            PlayerPrefs.SetString($"Account_{email}", password);
            Debug.Log($"새 계정 생성: {email}");

            if (GameManager.gm != null)
            {
                GameManager.gm.SetLoggedInEmail(email);
                GameManager.gm.SaveUserData(); // 새로운 사용자 데이터 저장
                Debug.Log("새 계정 데이터 저장 완료.");
            }
        }
    }




}
