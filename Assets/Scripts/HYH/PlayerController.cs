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
        GameManager.monsters.Dequeue();
        GameManager.todoDone.Dequeue();
    }
}
