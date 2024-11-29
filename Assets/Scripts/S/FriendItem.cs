/*using UnityEngine;
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
}*/


using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    public Text EmailText;  // 이메일을 표시하는 Text
    public Text ScoreText;  // 점수를 표시하는 Text

    public string Email { get; private set; } // 이메일 프로퍼티 추가

    public void Setup(string email, int score)
    {
        Email = email; // 프로퍼티에 이메일 저장
        EmailText.text = email;
        ScoreText.text = score.ToString();
    }
}
