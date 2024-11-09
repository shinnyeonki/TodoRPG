using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InputFormObjectController : MonoBehaviour
{

    public InputField inputField;
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
        //  todo list 가져오기
        var todoList = StorageManager.GetAll();
        if (todoList == null) { // todo list 에 아무것도 없을 때
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, string>();
        }
        Debug.Log("todoList count : " + todoList.Count);
        // todo list 에 추가하고 저장 key를 고유하게 고유하게 식별하기
        var key = Convert.ToString((DateTime.UtcNow - UnixEpoch).TotalMilliseconds);
        todoList.Add(key, inputField.text);
        StorageManager.Save(todoList);

        // todo list 추가
        taskArea.transform.GetComponent<TodoListContentController>().AddTodo(key, inputField.text);

        // text 필드 다시 초기화
        inputField.text = null;
    }
}
