using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // 싱글톤 인스턴스
    private int coins = 100; // 초기 코인 값
    public int score = 0; // 초기 점수
    public static Queue<string> todoDone = new Queue<string>(); // Todo 완료 이벤트 큐
    public static Queue<GameObject> monsters = new Queue<GameObject>(); // 몬스터 큐

    public List<Item> acquiredItems = new List<Item>(); // 획득한 아이템 목록

    // 코인 값 변경 이벤트
    public event Action<int> OnCoinsUpdated;

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 GameManager 유지
        }
        else
        {
            Debug.LogWarning("이미 GameManager 인스턴스가 존재합니다. 중복된 인스턴스를 제거합니다.");
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    void Start()
    {
        // 시작 시 코인 값 변경 이벤트 호출
        OnCoinsUpdated?.Invoke(coins);
    }

    public void AddAcquiredItem(Item newItem)
    {
        if (!acquiredItems.Contains(newItem))
        {
            acquiredItems.Add(newItem);
            Debug.Log($"{newItem.itemName}이(가) 획득 목록에 추가되었습니다.");
        }
    }

    // 코인 추가 메서드
    public void AddCoins(int amount)
    {
        coins += amount;
        OnCoinsUpdated?.Invoke(coins); // 이벤트 발행
    }

    // 코인 차감 메서드 (아이템 뽑기 시 사용)
    public bool SpendCoin(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;

            if (OnCoinsUpdated != null)
            {
                Debug.Log("OnCoinsUpdated 이벤트 호출됨");
                OnCoinsUpdated.Invoke(coins);
            }
            else
            {
                Debug.LogWarning("OnCoinsUpdated에 연결된 이벤트가 없습니다.");
            }

            return true;
        }
        Debug.LogWarning("코인이 부족합니다! 현재 코인: " + coins);
        return false;
    }

    // 현재 코인 값 반환 메서드
    public int GetCoins()
    {
        return coins;
    }

    // Todo 완료 이벤트를 추가하는 메서드
    public void AddTodoDone(string todoKey)
    {
        todoDone.Enqueue(todoKey);
        Debug.Log("Todo 완료 이벤트가 추가되었습니다: " + todoKey);
    }

    // 새로운 획득 아이템을 추가하는 메서드
    public void AddAcquiredItem(string itemName)
    {
        Debug.LogWarning("이 메서드는 사용하지 않습니다. AddAcquiredItem(Item newItem)을 사용하세요.");
    }

    // 큐에서 다음 Todo 완료 이벤트를 가져오는 메서드
    public void GetNextTodoDone()
    {
        if (todoDone.Count > 0)
        {
            string todoKey = todoDone.Dequeue();
            Debug.Log("큐에서 Todo를 가져왔습니다: " + todoKey);
        }
        else
        {
            Debug.LogWarning("큐가 비어 있습니다.");
        }
    }

    // 큐가 비어있는지 체크 하는 메서드
    public bool IsTodoDoneClear()
    {
        return !(todoDone.Count > 0);
    }

    // 큐에 있는 모든 Todo 완료 이벤트를 조회하는 메서드
    public void GetAllTodoDone()
    {
        if (todoDone.Count > 0)
        {
            StringBuilder todoDoneString = new StringBuilder("[");
            foreach (var todoKey in todoDone)
            {
                todoDoneString.Append(todoKey).Append(", ");
            }
            todoDoneString.Length -= 2; // 마지막 쉼표와 공백 제거
            todoDoneString.Append("]");
            Debug.Log("큐에 있는 모든 Todo: " + todoDoneString.ToString());
        }
        else
        {
            Debug.LogWarning("큐가 비어 있습니다.");
        }
    }
}
