using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendManager : MonoBehaviour
{
    [Header("Friend Add UI")]
    public InputField FriendEmailInputField; // �̸��� �Է� �ʵ�
    public Button AddFriendButton;          // ģ�� �߰� ��ư

    [Header("Friend List UI")]
    public GameObject FriendItemPrefab;     // ģ�� �׸� ������
    public Transform UnifiedListContent;    // ScrollView Content
    public Button SortByEmailButton;        // �̸��� ���� ��ư
    public Button SortByScoreButton;        // ���� ���� ��ư
    public Transform UnifiedListContainer;

    private List<Friend> friendList = new List<Friend>(); // ģ�� ��� ������

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ ����
        AddFriendButton.onClick.AddListener(AddFriend);
        SortByEmailButton.onClick.AddListener(SortByEmail);
        SortByScoreButton.onClick.AddListener(SortByScore);
    }


    /*public void AddFriend()
    {
        Debug.Log("AddFriend function called."); // AddFriend �Լ� ȣ�� Ȯ��

        string email = FriendEmailInputField.text;

        if (string.IsNullOrEmpty(email))
        {
            Debug.Log("Email is empty. Please enter an email.");
            return;
        }

        Debug.Log($"Adding friend with email: {email}");

        // ģ�� �߰� ó��
        GameObject newFriend = Instantiate(FriendItemPrefab, UnifiedListContainer);
        newFriend.GetComponent<FriendItem>().Setup(email, 0);

        Debug.Log($"Friend added: {email}");
    }*/

    public void AddFriend()
    {
        string email = FriendEmailInputField.text;

        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("Email field is empty!");
            return;
        }

        // ���ο� ģ�� Prefab ����
        GameObject newFriend = Instantiate(FriendItemPrefab, UnifiedListContainer);
        newFriend.GetComponent<FriendItem>().Setup(email, 0);

        // RectTransform ���� (�ʿ��ϸ�)
        RectTransform rectTransform = newFriend.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one; // ������ �ʱ�ȭ
        rectTransform.anchoredPosition3D = Vector3.zero; // ��ġ �ʱ�ȭ

        Debug.Log($"Friend added: {email}");
    }


    public void SortByEmail()
    {
        friendList.Sort((f1, f2) => f1.Email.CompareTo(f2.Email));
        UpdateFriendListUI();
    }

    public void SortByScore()
    {
        friendList.Sort((f1, f2) => f2.Score.CompareTo(f1.Score)); // ���� ��������
        UpdateFriendListUI();
    }

    private void UpdateFriendListUI()
    {
        // ���� UI Ŭ����
        foreach (Transform child in UnifiedListContent)
        {
            Destroy(child.gameObject);
        }

        // ģ�� ��� �ٽ� ����
        foreach (Friend friend in friendList)
        {
            GameObject item = Instantiate(FriendItemPrefab, UnifiedListContent);
            FriendItem itemScript = item.GetComponent<FriendItem>();
            itemScript.Setup(friend.Email, friend.Score);
        }
    }
}

[System.Serializable]
public class Friend
{
    public string Email;
    public int Score;

    public Friend(string email, int score)
    {
        Email = email;
        Score = score;
    }
}