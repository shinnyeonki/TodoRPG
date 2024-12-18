using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject m_gainAlert;
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
        m_scene.monster = null;
        GameManager.gm.AddCoins(10);
        GameManager.gm.PlusScore(10);
        GameManager.gm.GainHP(10);
        Debug.Log(GameManager.gm.GetHp());
        StartCoroutine(NextMonster());
    }

    public IEnumerator NextMonster()
    {
        GameManager.monsters.Dequeue();
        GameManager.todoDone.Dequeue();
        
        m_gainAlert.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        m_gainAlert.SetActive(false);
        

        if(GameManager.monsters.Count > 0)
            m_scene.SpawnMonsters();
    }
}
