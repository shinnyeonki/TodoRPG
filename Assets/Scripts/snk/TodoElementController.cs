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
        if (string.IsNullOrEmpty(GameManager.gm?.LoggedInEmail))
        {
            Debug.LogError("LoggedInEmail이 설정되지 않았습니다.");
            return;
        }

        var todoList = StorageManager.GetAll(GameManager.gm.LoggedInEmail);
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }
        Debug.Log("todoList count before deleting : " + todoList.Count);

        if (todoList.ContainsKey(Key))
        {
            todoList.Remove(Key);
            StorageManager.Save(GameManager.gm.LoggedInEmail, todoList);
            Debug.Log("todoList count after deleting : " + todoList.Count);
        }
        else
        {
            Debug.LogWarning("삭제하려는 Todo가 리스트에 존재하지 않습니다. Key: " + Key);
        }

        Destroy(gameObject);
    }


    public void CompleteSelf()
    {
        var todoList = StorageManager.GetAll(GameManager.gm.LoggedInEmail); // 수정
        if (todoList == null)
        {
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }

        if (!todoList.ContainsKey(Key))
        {
            Debug.LogWarning("완료하려는 Todo를 찾을 수 없습니다. Key: " + Key);
            return;
        }

        var currentTodo = todoList[Key];

        GameManager.gm.AddTodoDone(Key); // 기존 방식 유지

        if (!currentTodo.IsPriority)
        {
            todoList.Remove(Key);
            StorageManager.Save(GameManager.gm.LoggedInEmail, todoList); // 수정
        }

        Destroy(gameObject); // 위치 수정
    }

    public void OnPriorityToggleChanged(bool isOn)
    {
        var todoList = StorageManager.GetAll(GameManager.gm.LoggedInEmail); // 수정
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }

        if (todoList.ContainsKey(Key))
        {
            todoList[Key].IsPriority = isOn;
            StorageManager.Save(GameManager.gm.LoggedInEmail, todoList); // 수정
            Debug.Log("Priority changed for Todo : " + isOn);

            Destroy(gameObject);

            var todoListController = FindObjectOfType<TodoListContentController>();
            if (todoListController != null)
            {
                todoListController.AddTodo(Key, todoList[Key]);
            }
        }
        else
        {
            Debug.Log("Todo with key: " + Key + " not found.");
        }
    }
}
