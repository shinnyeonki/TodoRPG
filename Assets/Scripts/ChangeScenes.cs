using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScenes : MonoBehaviour
{
    public void ChangeListScence()
    {
        SceneManager.LoadScene("TodoList");
    }

    public void ChangeFriendScence()
    {
        SceneManager.LoadScene("Friend");
    }
}
