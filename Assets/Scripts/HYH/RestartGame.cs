using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    public GameObject gameOverPanel;
    // Start is called before the first frame update
    public void ResetGame()
    {
        GameManager.gm.ResetGame();
        gameOverPanel.SetActive(false);
        GameManager.gm.SaveUserData();
    }
}
