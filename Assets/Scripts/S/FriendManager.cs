using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendManager : MonoBehaviour
{
    [Header("Friend Add UI")]
    public InputField FriendEmailInputField; // 이메일 입력 필드
    public Button AddFriendButton;          // 친구 추가 버튼

    [Header("Friend List UI")]
    public GameObject FriendItemPrefab;     // 친구 항목 프리팹
    public Transform UnifiedListContent;    // ScrollView Content
    public Button SortByEmailButton;        // 이메일 정렬 버튼
    public Button SortByScoreButton;        // 점수 정렬 버튼
    public Transform UnifiedListContainer;

    private List<Friend> friendList = new List<Friend>(); // 친구 목록 데이터

    void Start()
    {
        // 버튼 클릭 이벤트 연결
        AddFriendButton.onClick.AddListener(AddFriend);
        SortByEmailButton.onClick.AddListener(SortByEmail);
        SortByScoreButton.onClick.AddListener(SortByScore);
    }



    public void AddFriend()
    {
        string email = FriendEmailInputField.text;

        //Instantiate(FriendItemPrefab, UnifiedListContainer);


        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("Email field is empty!");
            return;
        }

        // Prefab 생성
        GameObject newFriend = Instantiate(FriendItemPrefab, UnifiedListContainer);

        // RectTransform은 Vertical Layout Group이 관리하므로 위치를 조정하지 않음
        RectTransform rectTransform = newFriend.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one; // 스케일 초기화

        // FriendItem 데이터 설정
        var friendItem = newFriend.GetComponent<FriendItem>();
        if (friendItem != null)
        {
            friendItem.Setup(email, 0); // 이메일과 초기 점수 설정
            Debug.Log($"Added Friend: {email}");
        }
        else
        {
            Debug.LogError("FriendItem component is missing!");
        }

        if (UnifiedListContainer == null)
        {
            Debug.LogError("UnifiedListContainer is not assigned!");
        }

    }


    public void SortByEmail()
    {
        friendList.Sort((f1, f2) => f1.Email.CompareTo(f2.Email));
        UpdateFriendListUI();
    }

    public void SortByScore()
    {
        friendList.Sort((f1, f2) => f2.Score.CompareTo(f1.Score)); // 점수 내림차순
        UpdateFriendListUI();
    }


    private void UpdateFriendListUI()
    {
        // 기존 UI 클리어
        foreach (Transform child in UnifiedListContent)
        {
            Destroy(child.gameObject);
        }

        // 정렬된 친구 목록으로 UI 다시 생성
        foreach (Friend friend in friendList)
        {
            GameObject item = Instantiate(FriendItemPrefab, UnifiedListContent);
            var itemScript = item.GetComponent<FriendItem>();
            if (itemScript != null)
            {
                itemScript.Setup(friend.Email, friend.Score);
            }
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