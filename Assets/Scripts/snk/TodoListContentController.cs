using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using Unity.VisualScripting;

public class TodoListContentController : MonoBehaviour
{


    void Start()
    {
        DisplayTodoList();
    }

    public void ChangeDisplayMode()
    {
        // 모든 자식 객체를 삭제
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        DisplayTodoList();
    }

    public void DisplayTodoList()
    {
        // 여기서 반환되는 것 : Dictionary<string, StorageManager.TodoItem> string은 key, TodoItem은 value
        Dictionary<string, StorageManager.TodoItem> todoList = StorageManager.GetAll();
        Debug.Log("todolist initial count : " + todoList.Count);
        if (todoList == null)
        { // todo list 에 아무것도 없을 때는 그냥 return
            return;
        }


        //여기서 DropDown 메뉴(OVERDUE, DUETODAY, DUETOMORROW, DUENEXTWEEK, DUENEXTMONTH)에 따라 정렬
        //1. 컴포넌트를 가져와서 상태에 따라 switch문으로 각 상태에 맞게 옳지 않는 것은 제거해서 todoList 에 저장


        string displayMode = FindObjectOfType<TMP_Dropdown>().captionText.text;
        todoList = FilterTodoList(todoList, displayMode);




        // 우선순위와 기한에 따라 정렬
        var sortedTodoList = todoList
            .OrderBy(todo => todo.Value.IsPriority) // 우선순위 기준 오름차순 (false가 먼저)
            .ThenByDescending(todo => todo.Value.DueDate) // 기한 기준 내림차순
            .ToList();
        foreach (var todo in sortedTodoList)
        { // 객체를 하나씩 가져와서 순서를 맞춰서 추가 isPriority가 true인 것이 먼저 나오고 그 다음에는 기한이 빠른 것이 나옴
            AddTodo(todo.Key, todo.Value);
        }
    }

    public void AddTodo(string key, StorageManager.TodoItem todoItem)
    {
        {
            // 프리팹에서 인스턴스를 생성
            GameObject todoElementPrefab = (GameObject)Resources.Load("Prefabs/TodoElement");
            if (todoElementPrefab == null)
            {
                Debug.LogError("TodoElement 프리팹을 로드할 수 없습니다.");
                return;
            }
            GameObject todoElementObject = Instantiate(todoElementPrefab) as GameObject;

            // TodoElementController 스크립트에 key와 todoItem을 전달
            todoElementObject.transform.GetComponent<TodoElementController>().Key = key;
            todoElementObject.transform.GetComponent<TodoElementController>().TodoItem = todoItem;

            // TodoElementController 스크립트에 todoText,
            todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoText").GetComponent<Text>().text = todoItem.Text;
            todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoDate").GetComponent<Text>().text = todoItem.DueDate.ToString("yyyy-MM-dd HH:mm:ss");
            todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoPriorityToggle").GetComponent<Toggle>().isOn = todoItem.IsPriority; // 우선순위 토글
            Debug.Log("todoText : " + todoItem.Text + " isPriority : " + todoItem.IsPriority);
            todoElementObject.transform.SetParent(transform, false);
            todoElementObject.transform.SetSiblingIndex(0);

            InsertTodoElement(todoElementObject);

            // > Unity - Scripting API： Transform.localScale
            // > https://docs.unity3d.com/ScriptReference/Transform-localScale.html?_ga=2.65667310.1187445378.1569860193-888141676.1564340563
            todoElementObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void InsertTodoElement(GameObject todoElementObject)
    {
        var newTodoController = todoElementObject.GetComponent<TodoElementController>();
        if (newTodoController == null)
        {
            Debug.LogError("TodoElementController not found on the provided GameObject.");
            return;
        }

        // 새로운 TodoElement를 부모 객체에 추가
        todoElementObject.transform.SetParent(transform, false);

        // 모든 자식 객체를 리스트에 추가
        var children = transform.Cast<Transform>().ToList();

        // 원하는 기준에 따라 정렬 (우선순위와 기한)
        var sortedChildren = children.OrderBy(child =>
        {
            var todo = child.GetComponent<TodoElementController>().TodoItem;
            return todo.IsPriority ? 0 : 1; // 우선순위가 높은 것이 먼저
        }).ThenBy(child =>
        {
            var todo = child.GetComponent<TodoElementController>().TodoItem;
            return todo.DueDate; // 기한이 빠른 것이 먼저
        }).ToList();

        // 정렬된 순서대로 다시 부모 객체에 추가
        for (int i = 0; i < sortedChildren.Count; i++)
        {
            sortedChildren[i].SetSiblingIndex(i);
        }
    }


    public Dictionary<string, StorageManager.TodoItem> FilterTodoList(Dictionary<string, StorageManager.TodoItem> todoList, string displayMode)
    {
        DateTime now = DateTime.Now;
        DateTime today = now.Date;
        DateTime tomorrow = today.AddDays(1);
        DateTime nextWeek = today.AddDays(7);
        DateTime nextMonth = today.AddMonths(1);

        Dictionary<string, StorageManager.TodoItem> filteredTodoList = new Dictionary<string, StorageManager.TodoItem>();

        foreach (var todo in todoList)
        {
            bool addTodo = false;
            DateTime dueDate = todo.Value.DueDate.Date; // 시간 부분을 무시하고 날짜 부분만 사용

            switch (displayMode)
            {
                case "OVER":
                    if (dueDate < today)
                    {
                        addTodo = true;
                    }
                    break;
                case "TODAY":
                    if (dueDate == today)
                    {
                        addTodo = true;
                    }
                    break;
                case "TOMORROW":
                    if (dueDate == tomorrow)
                    {
                        addTodo = true;
                    }
                    break;
                case "WEEK":
                    if (dueDate > tomorrow && dueDate <= nextWeek)
                    {
                        addTodo = true;
                    }
                    break;
                case "MONTH":
                    if (dueDate > nextWeek && dueDate <= nextMonth)
                    {
                        addTodo = true;
                    }
                    break;
                default:
                    addTodo = true;
                    break;
            }

            if (addTodo)
            {
                filteredTodoList.Add(todo.Key, todo.Value);
            }
        }

        return filteredTodoList;
    }


}
