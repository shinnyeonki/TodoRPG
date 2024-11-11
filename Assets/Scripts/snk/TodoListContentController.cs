using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class TodoListContentController : MonoBehaviour
{
    void Start() {
        DisplayTodoList();
    }

    public void DisplayTodoList() {
        // 여기서 반환되는 것 : Dictionary<string, StorageManager.TodoItem> string은 key, TodoItem은 value
        var todoList = StorageManager.GetAll(); 
        Debug.Log("todolist initial count : " + todoList.Count);
        if (todoList == null) { // todo list 에 아무것도 없을 때는 그냥 return
            return;
        }
            // 우선순위와 기한에 따라 정렬
        var sortedTodoList = todoList
            .OrderBy(todo => todo.Value.IsPriority) // 우선순위 기준 오름차순 (false가 먼저)
            .ThenByDescending(todo => todo.Value.DueDate) // 기한 기준 내림차순
            .ToList();
        foreach (var todo in sortedTodoList) { // 객체를 하나씩 가져와서 순서를 맞춰서 추가 isPriority가 true인 것이 먼저 나오고 그 다음에는 기한이 빠른 것이 나옴
            AddTodo(todo.Key, todo.Value.Text, todo.Value.DueDate, todo.Value.IsPriority);
        }
    }

    public void AddTodo(string key, string todoText, DateTime dueDate, bool isPriority)
    {
        // 프리팹에서 인스턴스를 생성
        GameObject todoElementPrefab = (GameObject)Resources.Load("Prefabs/TodoElement");
        if (todoElementPrefab == null)
        {
            Debug.LogError("TodoElement 프리팹을 로드할 수 없습니다.");
            return;
        }
        GameObject todoElementObject = Instantiate(todoElementPrefab) as GameObject;

        // TodoElementController 스크립트에 key를 전달
        todoElementObject.transform.GetComponent<TodoElementController>().Key = key;

        // TodoElementController 스크립트에 todoText,
        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoText").GetComponent<Text>().text = todoText;
        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoDate").GetComponent<Text>().text = dueDate.ToString("yyyy-MM-dd HH:mm:ss");
        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoPriorityToggle").GetComponent<Toggle>().isOn = isPriority; // 우선순위 토글
        Debug.Log("todoText : " + todoText + " isPriority : " + isPriority);
        todoElementObject.transform.SetParent(transform, false);
        todoElementObject.transform.SetSiblingIndex(0);

        //InsertTodoElement(todoElementObject, dueDate, isPriority);

        // > Unity - Scripting API： Transform.localScale
        // > https://docs.unity3d.com/ScriptReference/Transform-localScale.html?_ga=2.65667310.1187445378.1569860193-888141676.1564340563
        todoElementObject.transform.localScale = new Vector3(1, 1, 1);
    }

    // public void InsertTodoElement(GameObject todoElementObject, DateTime dueDate, bool isPriority)
    // {
    //     bool inserted = false;
    //     for (int i = 0; i < transform.childCount; i++)
    //     {
    //         var existingTodo = transform.GetChild(i).GetComponent<TodoElementController>();
    //         if (existingTodo != null)
    //         {
    //             if (isPriority && !existingTodo.IsPriority)
    //             {
    //                 todoElementObject.transform.SetParent(transform, false);
    //                 todoElementObject.transform.SetSiblingIndex(i);
    //                 inserted = true;
    //                 break;
    //             }
    //             else if (isPriority == existingTodo.IsPriority && dueDate < existingTodo.DueDate)
    //             {
    //                 todoElementObject.transform.SetParent(transform, false);
    //                 todoElementObject.transform.SetSiblingIndex(i);
    //                 inserted = true;
    //                 break;
    //             }
    //         }
    //     }

    //     if (!inserted)
    //     {
    //         todoElementObject.transform.SetParent(transform, false);
    //         todoElementObject.transform.SetSiblingIndex(transform.childCount);
    //     }
    // }
}
