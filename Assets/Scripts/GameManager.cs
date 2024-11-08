using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; // gm 인스턴스 저장
    public Queue<int> todoDone = new Queue<int>(); // Todo 완료 이벤트 큐

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
    public void AddTodoDone()//(int todoId)
    {
        int todoId = Random.Range(1, 100); //raddom 으로 생성 1 에서 100 사이 값 //(int)System.DateTime.Now.Ticks;
        todoDone.Enqueue(todoId);
        Debug.Log("Todo가 큐에 추가되었습니다: " + todoId);
    }

    // 큐에서 데이터를 조회하는 메서드
    public void GetNextTodoDone()
    {
        if (todoDone.Count > 0)
        {
            int todoId = todoDone.Dequeue();
            Debug.Log("큐에서 Todo를 가져왔습니다: " + todoId);
            //return todoId;
        }
        else
        {
            Debug.LogWarning("큐가 비어 있습니다.");
            //return -1; // 큐가 비어 있을 때 반환할 값
        }
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
                todoDoneString.Append(todoId).Append(" ");
            }
            todoDoneString.Length--; // 마지막 공백 제거
            todoDoneString.Append("]");
            Debug.Log("큐에 있는 모든 Todo: " + todoDoneString.ToString());
        }
        else
        {
            Debug.LogWarning("큐가 비어 있습니다.");
        }
    }
}