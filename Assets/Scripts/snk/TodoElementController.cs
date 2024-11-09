using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TodoElementController : MonoBehaviour
{
    public string Key;
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

        // 모든 Todo를 가져옵니다.
        var todoList = StorageManager.GetAll();
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }
        Debug.Log("todoList count before completed : " + todoList.Count);
        // Todo를 제거하고 저장합니다.
        todoList.Remove(Key);
        StorageManager.Save(todoList);
        Debug.Log("todoList count after completed : " + todoList.Count);

        // Todo를 완료한 목록에 추가합니다.
        GameManager.gm.AddTodoDone(Key);

        // 목록에서도 삭제합니다.
        Destroy(gameObject);
    }
    public void ChangePriority(bool isPriority)
    {
        // 모든 Todo를 가져옵니다.
        var todoList = StorageManager.GetAll();
        if (todoList == null)
        {
            Debug.Log("todoList : null");
            todoList = new Dictionary<string, StorageManager.TodoItem>();
        }

        // Todo를 찾고 우선순위를 변경합니다.
        if (todoList.ContainsKey(Key))
        {
            todoList[Key].IsPriority = isPriority;
            StorageManager.Save(todoList);
            Debug.Log("Priority changed for Todo" + todoList[Key].IsPriority);
        }
        else
        {
            Debug.Log("Todo with key: " + Key + " not found.");
        }
    }

}
