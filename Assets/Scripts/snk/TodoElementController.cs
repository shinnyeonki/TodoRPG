using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TodoElementController : MonoBehaviour
{
    public string Key { get; set; }
    public StorageManager.TodoItem TodoItem { get; set; }


    void Start()
    {
        // Toggle 컴포넌트를 찾습니다.
        Toggle priorityToggle = transform.Find("TaskRawImage/TodoPriorityToggle").GetComponent<Toggle>();
        if (priorityToggle != null)
        {
            // OnValueChanged 이벤트에 리스너를 추가합니다.
            priorityToggle.onValueChanged.AddListener(OnPriorityToggleChanged);
        }
        else
        {
            Debug.LogError("Priority Toggle not found!");
        }
    }


    public void DeleteSelf()
    {
        // 모든 Todo를 가져옵니다.
        var todoList = StorageManager.GetAll();
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }
        Debug.Log("todoList count before deleting : " + todoList.Count);
        // Todo를 제거하고 저장합니다.
        todoList.Remove(Key);
        StorageManager.Save(todoList);
        Debug.Log("todoList count after deleting : " + todoList.Count);

        // 목록에서도 삭제합니다.
        Destroy(gameObject);
    }
    public void CompleteSelf()
    {
        var todoList = StorageManager.GetAll();
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }

        Debug.Log("todoList count before completed : " + todoList.Count);

        if (!todoList.ContainsKey(Key))
        {
            Debug.LogWarning("완료하려는 Todo를 찾을 수 없습니다. Key: " + Key);
            return;
        }

        var currentTodo = todoList[Key];

        // 고정(우선순위)된 Todo인지 확인
        if (currentTodo.IsPriority)
        {
            // 고정된 Todo는 완료 처리해도 삭제하지 않음
            // 완료 처리 로직: 완료 이벤트 큐에 추가
            GameManager.gm.AddTodoDone(Key);

            // 고정된 Todo는 리스트에 그대로 두므로 StorageManager.Save()로 다시 저장
            StorageManager.Save(todoList);

            Debug.Log("고정된 Todo 완료 처리, 삭제하지 않음. Key: " + Key);

            // 오브젝트도 파괴하지 않으므로 UI 상에 계속 남아있음.
            // 필요하다면 UI에 완료 상태를 표시하는 로직 추가 가능 (예: 색상 변경)
            // 이 경우 UpdateUI() 같은 메서드로 상태 반영 가능.
        }
        else
        {
            // 고정되지 않은 Todo는 기존 동작대로 완료 시 제거
            todoList.Remove(Key);
            StorageManager.Save(todoList);
            Debug.Log("todoList count after completed : " + todoList.Count);

            // 완료 이벤트 큐에 추가
            GameManager.gm.AddTodoDone(Key);

            // 목록에서도 삭제
            Destroy(gameObject);
            Debug.Log("고정되지 않은 Todo 완료 처리 후 삭제됨. Key: " + Key);
        }
    }

    public void OnPriorityToggleChanged(bool isOn)
    {
        // 모든 Todo를 가져옵니다.
        var todoList = StorageManager.GetAll();
        if (todoList == null) //없는 경우 새로 생성
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }

        // Todo를 찾고 우선순위를 변경합니다.
        if (todoList.ContainsKey(Key)) // 해당 키가 있는 경우
        {
            // 저장소를 업데이트
            todoList[Key].IsPriority = isOn;
            // storage 에 저장
            StorageManager.Save(todoList);
            Debug.Log("Priority changed for Todo : " + isOn);

            // UI를 초록색으로 변경
            var priorityIndicator = transform.Find("PriorityIndicator");

            // 현재 GameObject를 파괴
            Destroy(gameObject);

            // TodoListContentController를 찾아서 새로운 GameObject를 추가
            FindObjectOfType<TodoListContentController>().AddTodo(Key, todoList[Key]);

        }
        else
        {
            Debug.Log("Todo with key: " + Key + " not found.");
        }
    }

}
