using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScene : MonoBehaviour
{
    public static BaseScene baseScene;
    public GameManager gm;
    public PlayerController player;

    void Start()
    {
        LoadMainScene();
    }

    public void LoadMainScene()
    {
        if(!gm.IsTodoDoneClear())
        {
            player.BattlePosition();
            CreateMonster();
        }
    }

    void CreateMonster()
    {
        GameObject monster = Resources.Load("Prefabs/MonsterA") as GameObject;
        Vector3 pos = new Vector3(1, -1, 0);
        monster.transform.position = pos;
        Instantiate(monster);
    }
}
