using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public List<Friend> FriendList { get; private set; } = new List<Friend>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddFriend(string email, int score)
    {
        Friend newFriend = new Friend(email, score);
        FriendList.Add(newFriend);
        Debug.Log($"Friend added: {email}, Score: {score}");
    }

    public void SortFriendsByEmail()
    {
        FriendList.Sort((f1, f2) => f1.Email.CompareTo(f2.Email));
        Debug.Log("Sorted friends by email.");
    }

    public void SortFriendsByScore()
    {
        FriendList.Sort((f1, f2) => f2.Score.CompareTo(f1.Score)); // 점수 내림차순
        Debug.Log("Sorted friends by score.");
    }
}
