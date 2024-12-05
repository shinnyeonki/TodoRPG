using UnityEngine;
using UnityEngine.SceneManagement;

public class FriendUIController : MonoBehaviour
{
    public GameObject FriendUIRoot; // Friend UI 루트 오브젝트

    private bool isInitialized = false; // 초기화 상태 체크

    private void Awake()
    {
        if (FriendUIRoot == null)
        {
            FriendUIRoot = gameObject; // FriendUIRoot가 비어 있으면 자동으로 현재 오브젝트를 연결
        }
    }

    private void Start()
    {
        // 씬 전환 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 초기화 플래그 설정
        isInitialized = true;

        // 현재 씬 상태 확인 후 UI 갱신
        UpdateUIVisibility();
    }

    private void OnDestroy()
    {
        // 씬 전환 이벤트 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnEnable()
    {
        if (isInitialized)
        {
            UpdateUIVisibility();
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 씬이 로드된 후 UI 상태를 갱신
        UpdateUIVisibility();
    }

    private void UpdateUIVisibility()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Friend")
        {
            // Friend 씬에서는 UI 활성화
            FriendUIRoot.SetActive(true);
            Debug.Log("Friend UI 활성화");
        }
        else
        {
            // 다른 씬에서는 UI 비활성화
            FriendUIRoot.SetActive(false);
            Debug.Log("Friend UI 비활성화");
        }
    }
}
