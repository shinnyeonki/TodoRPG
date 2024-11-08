using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm; //gm 인스턴스 생성
    public Queue<int> monsterInQue = new Queue<int>(); //몬스터 이벤트 큐

    //싱글턴 디자인 패턴 체크
    void Awake() 
    {
        if (gm == null) gm = this;
        else
        {
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재");
            Destroy(gameObject);
        }
    }
}
