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
        var todoList = StorageManager.GetAll(GameManager.gm.LoggedInEmail); // 수정
        if (todoList != null)
        {
            foreach (var kvp in todoList)
            {
                AddTodo(kvp.Key, kvp.Value);
            }
        }
    }

    public void ChangeDisplayMode()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        DisplayTodoList();
    }

    public void DisplayTodoList()
    {
        // email 기반으로 todoList 가져오기
        Dictionary<string, StorageManager.TodoItem> todoList = StorageManager.GetAll(GameManager.gm.LoggedInEmail); // 수정
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            return;
        }

        Debug.Log("todoList initial count: " + todoList.Count);

        // Display mode에 따른 필터링
        string displayMode = FindObjectOfType<TMP_Dropdown>().captionText.text;
        todoList = FilterTodoList(todoList, displayMode);

        // 정렬
        var sortedTodoList = todoList
            .OrderBy(todo => todo.Value.IsPriority) // 우선순위 기준
            .ThenByDescending(todo => todo.Value.DueDate) // 기한 기준
            .ToList();

        foreach (var todo in sortedTodoList)
        {
            AddTodo(todo.Key, todo.Value);
        }
    }

    public void AddTodo(string key, StorageManager.TodoItem todoItem)
    {
        GameObject todoElementPrefab = (GameObject)Resources.Load("Prefabs/TodoElement");
        if (todoElementPrefab == null)
        {
            Debug.LogError("TodoElement 프리팹을 로드할 수 없습니다.");
            return;
        }
        GameObject todoElementObject = Instantiate(todoElementPrefab);

        todoElementObject.transform.GetComponent<TodoElementController>().Key = key;
        todoElementObject.transform.GetComponent<TodoElementController>().TodoItem = todoItem;

        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoText").GetComponent<Text>().text = todoItem.Text;
        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoDate").GetComponent<Text>().text = todoItem.DueDate.ToString("yyyy-MM-dd HH:mm:ss");
        todoElementObject.transform.Find("TaskRawImage").transform.Find("TodoPriorityToggle").GetComponent<Toggle>().isOn = todoItem.IsPriority;

        todoElementObject.transform.SetParent(transform, false);
        todoElementObject.transform.localScale = new Vector3(1, 1, 1);
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
            DateTime dueDate = todo.Value.DueDate.Date;

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
