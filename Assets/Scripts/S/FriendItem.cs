using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    public Text EmailText; // 이메일을 표시할 Text 컴포넌트
    public Text ScoreText; // 점수를 표시할 Text 컴포넌트

    public void Setup(string email, int score)
    {
        EmailText.text = email;  // 이메일 표시
        ScoreText.text = score.ToString(); // 점수 표시
    }
}
