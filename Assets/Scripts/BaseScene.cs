using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScene : MonoBehaviour
{
    public static BaseScene baseScene;
    public GameManager gm;
    public PlayerController player;
    public List<GameObject> monsterList;

    void Start()
    {
        LoadMainScene();
        InputMonsters();
    }

    public void LoadMainScene()
    {
        monsterList = new List<GameObject>();
        InputMonsters();
        if (!gm.IsTodoDoneClear())
        {
            player.BattlePosition();
            CreateMonster();
        }
        else
        {
            player.DefaultPosition();
        }
    }

    void CreateMonster() //몬스터 생성
    {
        int random = Random.Range(0, monsterList.Count);
        Debug.Log(random);
        Vector3 pos = new Vector3(1, -0.8f, 0);

        GameObject monster = monsterList[random];
        monster.transform.position = pos;
        Instantiate(monsterList[random]);
    }

    void InputMonsters() //몬스터 리스트 생성
    {
        monsterList.Add(Resources.Load("Prefabs/Mushroom") as GameObject);
        monsterList.Add(Resources.Load("Prefabs/Skeleton") as GameObject);
        monsterList.Add(Resources.Load("Prefabs/Goblin") as GameObject);
        monsterList.Add(Resources.Load("Prefabs/Flying eye") as GameObject);
    }
}
