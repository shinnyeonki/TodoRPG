using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void ChangeListScence()
    {
        if(SceneManager.GetActiveScene().name !="TodoList")
            SceneManager.LoadScene("TodoList");
    }

    public void ChangeFriendScence()
    {
        if (SceneManager.GetActiveScene().name != "Friend")
            SceneManager.LoadScene("Friend");
    }
    public void ChangeMainScence()
    {
        if (SceneManager.GetActiveScene().name != "Main")
            SceneManager.LoadScene("Main");
    }
    public void ChangeItemScence()
    {
        if (SceneManager.GetActiveScene().name != "item")
            SceneManager.LoadScene("item");
    }
}
