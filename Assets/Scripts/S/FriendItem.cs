using UnityEngine;
using UnityEngine.UI;

public class FriendItem : MonoBehaviour
{
    public Text EmailText; // �̸����� ǥ���� Text ������Ʈ
    public Text ScoreText; // ������ ǥ���� Text ������Ʈ

    public void Setup(string email, int score)
    {
        EmailText.text = email;  // �̸��� ǥ��
        ScoreText.text = score.ToString(); // ���� ǥ��
    }
}