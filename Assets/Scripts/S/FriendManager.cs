using System.Collections.Generic; //삭제 버튼 추가 이전
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

        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("Email field is empty!");
            return;
        }

        // 친구 데이터 생성
        Friend newFriend = new Friend(email, 0);
        friendList.Add(newFriend);

        // 새 텍스트 오브젝트 생성
        GameObject newFriendText = new GameObject("FriendText");
        newFriendText.transform.SetParent(UnifiedListContent);

        // Text 컴포넌트 추가 및 설정
        Text textComponent = newFriendText.AddComponent<Text>();
        textComponent.text = $"{newFriend.Email} - {newFriend.Score}";
        textComponent.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        textComponent.color = Color.black;
        textComponent.fontSize = 15; // 텍스트 크기
        textComponent.alignment = TextAnchor.UpperLeft;

        // RectTransform 설정
        RectTransform rectTransform = newFriendText.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;

        // Content의 자식 개수를 기준으로 텍스트 위치 설정
        int childCount = UnifiedListContent.childCount;
        float itemHeight = 30f; // 각 텍스트의 높이 (Spacing 포함)
        rectTransform.sizeDelta = new Vector2(200, itemHeight);
        rectTransform.anchoredPosition = new Vector2(0, -itemHeight * (childCount - 1));

        // Content의 Height를 조정하여 모든 텍스트를 표시할 수 있도록 설정
        RectTransform contentRect = UnifiedListContent.GetComponent<RectTransform>();
        float contentHeight = Mathf.Max(180, itemHeight * childCount);
        contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, contentHeight);

        Debug.Log($"Added Friend: {email}");
    }


    // 이메일로 정렬
    public void SortByEmail()
    {
        friendList.Sort((f1, f2) => f1.Email.CompareTo(f2.Email));
        UpdateFriendListUI();
    }

    // 점수로 정렬
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

        // 정렬된 데이터로 UI 다시 생성
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


    private void AdjustContentRectTransform()
    {
        RectTransform rectTransform = UnifiedListContent.GetComponent<RectTransform>();

        // 앵커와 피벗을 상단으로 설정
        rectTransform.anchorMin = new Vector2(0, 1);
        rectTransform.anchorMax = new Vector2(0, 1);
        rectTransform.pivot = new Vector2(0, 1);

        // 위치를 초기화
        rectTransform.anchoredPosition = Vector2.zero;
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

/*using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendManager : MonoBehaviour
{
    public static FriendManager Instance { get; private set; } // 싱글턴 인스턴스

    [Header("Friend Add UI")]
    public InputField FriendEmailInputField; // 이메일 입력 필드
    public Button AddFriendButton;          // 친구 추가 버튼

    [Header("Friend List UI")]
    public Text FriendTextPrefab;           // Text 프리팹
    public Transform UnifiedListContent;    // ScrollView Content
    public Button SortByEmailButton;        // 이메일 정렬 버튼
    public Button SortByScoreButton;        // 점수 정렬 버튼

    // 친구 목록 데이터 (정적 리스트)
    private static List<Friend> friendList = new List<Friend>();

    private void Awake()
    {
        // 싱글턴 인스턴스 생성 및 DontDestroyOnLoad 적용
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // UI 활성화는 Friend 씬에서만 실행
        if (IsFriendScene())
        {
            InitializeUI();
        }
    }

    private void OnEnable()
    {
        // Friend 씬으로 돌아왔을 때 UI 갱신
        if (IsFriendScene())
        {
            InitializeUI();
            UpdateFriendListUI();
        }
        else
        {
            DisableUI();
        }
    }

    private void OnDisable()
    {
        // 다른 씬으로 이동 시 UI 비활성화
        DisableUI();
    }

    public void AddFriend()
    {
        if (FriendEmailInputField == null) return;

        string email = FriendEmailInputField.text;

        if (string.IsNullOrEmpty(email))
        {
            Debug.LogError("Email field is empty!");
            return;
        }

        // 친구 데이터 생성
        Friend newFriend = new Friend(email, 0);
        friendList.Add(newFriend);

        // UI 갱신
        UpdateFriendListUI();

        Debug.Log($"Added Friend: {email}");
    }

    // 이메일로 정렬
    public void SortByEmail()
    {
        friendList.Sort((f1, f2) => f1.Email.CompareTo(f2.Email));
        UpdateFriendListUI();
    }

    // 점수로 정렬
    public void SortByScore()
    {
        friendList.Sort((f1, f2) => f2.Score.CompareTo(f1.Score)); // 점수 내림차순
        UpdateFriendListUI();
    }

    private void InitializeUI()
    {
        if (AddFriendButton != null)
            AddFriendButton.onClick.AddListener(AddFriend);
        if (SortByEmailButton != null)
            SortByEmailButton.onClick.AddListener(SortByEmail);
        if (SortByScoreButton != null)
            SortByScoreButton.onClick.AddListener(SortByScore);

        UpdateFriendListUI();
    }

    private void DisableUI()
    {
        // UI 요소가 있다면 비활성화
        if (UnifiedListContent != null)
        {
            foreach (Transform child in UnifiedListContent)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void UpdateFriendListUI()
    {
        // UI 요소가 없는 씬에서는 갱신하지 않음
        if (UnifiedListContent == null) return;

        // 기존 UI 클리어
        foreach (Transform child in UnifiedListContent)
        {
            Destroy(child.gameObject);
        }

        // 정렬된 데이터로 UI 다시 생성
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

    // 현재 씬이 Friend 씬인지 확인
    private bool IsFriendScene()
    {
        return UnifiedListContent != null; // UI 요소가 존재하는지로 판단
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
}*/