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
            return;
        if(GameManager.monsters.Count==0)
            return;
     

        Destroy(m_scene.monster);
        GameManager.gm.AddCoins(10);
        m_scene.monster = null;

        GameManager.monsters.Dequeue();
        GameManager.todoDone.Dequeue();

        if(GameManager.monsters.Count > 0)
            m_scene.SpawnMonsters();
    }
}
