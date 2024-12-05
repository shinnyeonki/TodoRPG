using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        // 친구 데이터 추가
        if (DataManager.Instance != null)
        {
            DataManager.Instance.AddFriend(email, 0); // 점수 기본값은 0으로 설정
            Debug.Log($"Added Friend to DataManager: {email}");
        }
        else
        {
            Debug.LogError("DataManager.Instance is null!");
            return;
        }

        // UI 업데이트
        UpdateFriendListUI();
    }

    // 이메일로 정렬
    public void SortByEmail()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SortFriendsByEmail();
            UpdateFriendListUI();
        }
    }

    // 점수로 정렬
    public void SortByScore()
    {
        if (DataManager.Instance != null)
        {
            DataManager.Instance.SortFriendsByScore();
            UpdateFriendListUI();
        }
    }

    private void UpdateFriendListUI()
    {
        if (DataManager.Instance == null) return;

        // 기존 UI 클리어
        foreach (Transform child in UnifiedListContent)
        {
            Destroy(child.gameObject);
        }

        // DataManager의 친구 리스트를 가져와 UI 갱신
        List<Friend> friendList = DataManager.Instance.FriendList;

        float itemHeight = 30f; // 각 항목 간 간격
        float contentHeight = Mathf.Max(180, itemHeight * friendList.Count);

        RectTransform contentRect = UnifiedListContent.GetComponent<RectTransform>();
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

        for (int i = 0; i < friendList.Count; i++)
        {
            Friend friend = friendList[i];

            // 새 텍스트 오브젝트 생성
            GameObject newFriendText = new GameObject("FriendText");
            newFriendText.transform.SetParent(UnifiedListContent);

            // Text 컴포넌트 추가 및 설정
            Text textComponent = newFriendText.AddComponent<Text>();
            textComponent.text = $"{friend.Email} - {friend.Score}";
            textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
            textComponent.color = Color.black;
            textComponent.fontSize = 15;
            textComponent.alignment = TextAnchor.UpperLeft;

            // RectTransform 설정
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
