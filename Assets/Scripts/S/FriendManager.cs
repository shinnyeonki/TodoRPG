using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FriendManager : MonoBehaviour
{
    [Header("Friend Add UI")]
    public InputField FriendEmailInputField; // 이메일 입력 필드
    public Button AddFriendButton;          // 친구 추가 버튼

    [Header("Friend List UI")]
    public Text FriendTextPrefab;           // Text 프리팹
    public Transform UnifiedListContent;    // ScrollView Content
    public Button SortByEmailButton;        // 이메일 정렬 버튼
    public Button SortByScoreButton;        // 점수 정렬 버튼

    private void Start()
    {
        // 버튼 클릭 이벤트 연결
        AddFriendButton.onClick.AddListener(AddFriend);
        SortByEmailButton.onClick.AddListener(SortByEmail);
        SortByScoreButton.onClick.AddListener(SortByScore);

        // UI 초기화
        LoadFriendListFromGameManager();
        UpdateFriendListUI();
    }

    public void AddFriend()
    {
        string email = FriendEmailInputField.text;

        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("Email field is empty!");
            return;
        }

        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager가 null입니다. 친구를 추가할 수 없습니다.");
            return;
        }

        // 친구 데이터 추가
        DataManager.Instance.AddFriend(email, 0); // 점수 기본값은 0
        Debug.Log($"Added Friend: {email}");

        // UI 업데이트
        UpdateFriendListUI();
    }


    // 이메일로 정렬
    public void SortByEmail()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager가 null입니다. 정렬할 수 없습니다.");
            return;
        }

        // 이메일 기준으로 정렬
        DataManager.Instance.FriendList.Sort((a, b) => string.Compare(a.Email, b.Email, StringComparison.OrdinalIgnoreCase));

        // UI 업데이트
        UpdateFriendListUI();
        Debug.Log("Friend list sorted by email.");
    }

    // 점수로 정렬
    public void SortByScore()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager가 null입니다. 정렬할 수 없습니다.");
            return;
        }

        // 점수 기준으로 정렬 (내림차순)
        DataManager.Instance.FriendList.Sort((a, b) => b.Score.CompareTo(a.Score));

        // UI 업데이트
        UpdateFriendListUI();
        Debug.Log("Friend list sorted by score.");
    }


    private void LoadFriendListFromGameManager()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager가 null입니다. 데이터를 로드할 수 없습니다.");
            return;
        }

        if (DataManager.Instance.FriendList.Count == 0)
        {
            DataManager.Instance.AddFriend("friend1@email.com", 100);
            DataManager.Instance.AddFriend("friend2@email.com", 200);
            DataManager.Instance.AddFriend("friend3@email.com", 150);
            DataManager.Instance.AddFriend("friend4@email.com", 1000);
            DataManager.Instance.AddFriend("friend5@email.com", 30);
        }
    }

    private void UpdateFriendListUI()
    {
        if (DataManager.Instance == null)
        {
            Debug.LogError("DataManager가 null입니다. UI를 업데이트할 수 없습니다.");
            return;
        }

        foreach (Transform child in UnifiedListContent)
        {
            Destroy(child.gameObject);
        }

        List<Friend> friendList = DataManager.Instance.FriendList;

        float itemHeight = 30f; // 각 항목 간 간격
        float contentHeight = Mathf.Max(180, itemHeight * friendList.Count);

        RectTransform contentRect = UnifiedListContent.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

        for (int i = 0; i < friendList.Count; i++)
        {
            Friend friend = friendList[i];

            GameObject newFriendText = new GameObject("FriendText");
            newFriendText.transform.SetParent(UnifiedListContent);

            Text textComponent = newFriendText.AddComponent<Text>();
            textComponent.text = $"{friend.Email} - {friend.Score}";
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.color = Color.black;
            textComponent.fontSize = 15;
            textComponent.alignment = TextAnchor.UpperLeft;

            RectTransform rectTransform = newFriendText.GetComponent<RectTransform>();
            rectTransform.localScale = Vector3.one;
            rectTransform.sizeDelta = new Vector2(200, itemHeight);
            rectTransform.anchoredPosition = new Vector2(0, -itemHeight * i);
        }

        Debug.Log("Friend list updated in UI.");
    }

}

[System.Serializable]
public class Friend
{
    public string Email { get; set; } // 이메일
    public int Score { get; set; }   // 점수

    public Friend(string email, int score)
    {
        Email = email;
        Score = score;
    }
}