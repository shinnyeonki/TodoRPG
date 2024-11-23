using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseScene : MonoBehaviour
{
    public PlayerController player;
    public List<GameObject> monsterList = new List<GameObject>();
    public GameObject monster;

    void Start()
    {
        LoadMainScene();
    }

    public void LoadMainScene()
    {
        InputMonsterPrefabs();
        InputMonsters();
        if (GameManager.gm.IsTodoDoneClear()) 
        {
            Debug.Log("default position");
            player.DefaultPosition();
            return;
        }
        SpawnMonsters();
        //gm.GetNextTodoDone(); TODO 이후 어택 방식에서 삭제로 변경 
    }
    public void SpawnMonsters() //몬스터 생성
    {
        Debug.Log("battle position");
        player.BattlePosition();
               
        Vector3 pos = new Vector3(1, -0.85f, 0);
        monster = Instantiate(GameManager.monsters.First());
        monster.transform.position = pos;
    }


    void InputMonsterPrefabs() //몬스터 리스트 생성
    {
        if(monsterList.Count == 4)
            return;

        monsterList.Add(Resources.Load("Prefabs/Mushroom") as GameObject);
        monsterList.Add(Resources.Load("Prefabs/Skeleton") as GameObject);
        monsterList.Add(Resources.Load("Prefabs/Goblin") as GameObject);
        monsterList.Add(Resources.Load("Prefabs/Flying eye") as GameObject);
    }

    void InputMonsters() //todoDone 큐를 monster 큐에 추가
    {
        if(GameManager.gm.IsTodoDoneClear())
            return;
        if(GameManager.monsters.Count == GameManager.todoDone.Count)
            return;
        
        // 몬스터 큐에 몬스터 랜덤으로 추가
        for(int i=GameManager.monsters.Count; i <GameManager.todoDone.Count+1; i++)
        {
            int random = Random.Range(0, monsterList.Count);
            GameManager.monsters.Enqueue(monsterList[random]);
            Debug.Log("몬스터 추가 : "+random);
        }
    }
}
