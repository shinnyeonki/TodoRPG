using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public BaseScene m_scene;
    public void BattlePosition()
    {
        gameObject.transform.position = new Vector3(-0.75f,0,0);
    }
    public void DefaultPosition()
    {
        gameObject.transform.position = new Vector3(0,0,0);
    }

    public void KillMonster()
    {
        if(m_scene.monster == null)
        {
            Debug.Log("not spawn monster");
            return; 
        }

        Destroy(m_scene.monster);
        m_scene.monster = null;

        if(GameManager.monsters.Count==0 || GameManager.todoDone.Count==0)
        {
            Debug.Log("monster queue clear");
            return;
        }
        GameManager.monsters.Dequeue();
        GameManager.todoDone.Dequeue();

        m_scene.SpawnMonsters();
    }
}
