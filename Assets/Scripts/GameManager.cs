using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // 싱글톤 인스턴스
    public Queue<string> todoDone = new Queue<string>(); // Todo 완료 이벤트 큐
    public List<string> acquiredItems = new List<string>(); // 획득한 아이템 목록
    public int coin = 100; // 기본 코인 수
    public Text coinText; // 재화를 UI에 표시할 텍스트 컴포넌트

    void Awake()
    {
        if (gm == null) gm = this;
        else
        {
            Debug.LogWarning("이미 GameManager 인스턴스가 존재합니다. 중복된 인스턴스를 제거합니다.");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateCoinUI();
    }

    // 코인 차감 메서드 (아이템 뽑기 시 사용)
    public bool SpendCoin(int amount)
    {
        if (coin >= amount)
        {
            coin -= amount;
            UpdateCoinUI();
            Debug.Log("뽑기 후 남은 코인: " + coin);
            return true;
        }
        Debug.LogWarning("코인이 부족합니다! 현재 코인: " + coin);
        return false;
    }

    // 재화 UI 업데이트 메서드
    public void UpdateCoinUI()
    {
        if (coinText != null)
        {
            coinText.text = "코인: " + coin;
        }
    }

    // Todo 완료 이벤트를 추가하는 메서드 (다른 팀원이 사용하는 기능)
    public void AddTodoDone(string todoKey)
    {
        todoDone.Enqueue(todoKey);
        Debug.Log("Todo 완료 이벤트가 추가되었습니다: " + todoKey);
    }

    // 새로운 획득 아이템을 추가하는 메서드 (EquipmentBag과 연동)
    public void AddAcquiredItem(string itemName)
    {
        acquiredItems.Add(itemName); // 획득한 아이템 리스트에 추가
        Debug.Log("아이템이 획득 목록에 추가되었습니다: " + itemName);
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
