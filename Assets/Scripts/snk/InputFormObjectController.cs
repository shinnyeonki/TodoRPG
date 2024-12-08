using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UI.Dates;

public class InputFormObjectController : MonoBehaviour
{

    public InputField inputField;

    public DatePicker datePicker;
    public GameObject taskArea;
    // > .net - Unix time conversions in C# - Stack Overflow  
    // > https://stackoverflow.com/questions/7983441/unix-time-conversions-in-c-sharp
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddTask()
    {
        // todo list 가져오기
        var todoList = StorageManager.GetAll(GameManager.gm.LoggedInEmail); // 수정
        if (todoList == null)
        { // todo list 에 아무것도 없을 때 만들어서 사용
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }
        Debug.Log("todoList count : " + todoList.Count);
        // todo list 에 추가하고 저장 key를 고유하게 고유하게 식별하기
        var key = Convert.ToString((DateTime.UtcNow - UnixEpoch).TotalMilliseconds); // 고유한 키 생성

        // TodoItem 객체 생성해서 데이터 넣기
        var selectedDate = (datePicker.SelectedDate != DateTime.MinValue) ? (DateTime)datePicker.SelectedDate : DateTime.UtcNow.AddDays(1);

        var todoItem = new StorageManager.TodoItem
        {
            CreationDate = DateTime.UtcNow,
            DueDate = selectedDate,
            Text = inputField.text,
            IsPriority = false // 기본 우선순위를 false로 설정
        };

        // todo list에 추가하고 저장
        todoList.Add(key, todoItem);
        StorageManager.Save(GameManager.gm.LoggedInEmail, todoList); // 수정

        // todo list 추가
        taskArea.transform.GetComponent<TodoListContentController>().AddTodo(key, todoItem);

        // text 필드 다시 초기화
        inputField.text = null;
    }
}
