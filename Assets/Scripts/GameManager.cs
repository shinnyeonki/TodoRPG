using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // gm 인스턴스 저장
    public static Queue<string> todoDone = new Queue<string>(); // Todo 완료 이벤트 큐

    // 싱글톤 패턴을 위한 인스턴스 체크
    void Awake()
    {
        if (gm == null) gm = this;
        else
        {
            Debug.LogWarning("이미 인스턴스가 존재합니다. 중복된 인스턴스를 제거합니다.");
            Destroy(gameObject);
        }
    }

    // 큐에 데이터를 추가하는 메서드
    public void AddTodoDone(string key = null)
    {
        string todoKEY;
        if (key == "null")
        {
            todoKEY = Random.Range(1, 100).ToString(); // 1에서 100 사이의 랜덤 값 생성
        }
        else
        {
            todoKEY = key;
        }
        todoDone.Enqueue(todoKEY);
        Debug.Log("Todo가 큐에 추가되었습니다: " + todoKEY);
    }

    // 큐에서 데이터를 조회하는 메서드
    public void GetNextTodoDone()
    {
        if (todoDone.Count > 0)
        {
            string todoKEY = todoDone.Dequeue();
            Debug.Log("큐에서 Todo를 가져왔습니다: " + todoKEY);
        }
        else
        {
            Debug.LogWarning("큐가 비어 있습니다.");
        }
    }

    //큐가 비어있는지 체크 하는 메서드
    public bool IsTodoDoneClear()
    {
        return !(todoDone.Count > 0);
    }
    

    // 모든 queue를 한줄로 보는 메서드
    public void GetAllTodoDone()
    {
        if (todoDone.Count > 0)
        {
            Debug.Log("큐에 있는 모든 Todo를 조회합니다.");
            StringBuilder todoDoneString = new StringBuilder("[");
            foreach (var todoId in todoDone)
            {
                todoDoneString.Append(todoId).Append(", ");
            }
            todoDoneString.Length--; // 마지막 공백 제거
            todoDoneString.Length--; // 마지막 쉼표 제거
            todoDoneString.Append("]");
            Debug.Log("큐에 있는 모든 Todo를 조회합니다." + todoDoneString.ToString());
        }
        else
        {
            Debug.LogWarning("큐가 비어 있습니다.");
        }
    }
}