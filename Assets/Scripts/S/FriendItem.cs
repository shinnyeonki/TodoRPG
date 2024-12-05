using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    public Text EmailText;   // 이메일을 표시할 Text
    public Text ScoreText;   // 점수를 표시할 Text

    public void Setup(string email, int score)
    {
        if (EmailText != null)
        {
            EmailText.text = email;  // 이메일 텍스트 설정
        }

        if (ScoreText != null)
        {
            ScoreText.text = score.ToString();  // 점수 텍스트 설정
        }
    }
}

