using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void BattlePosition()
    {
        gameObject.transform.position = new Vector3(-0.75f,0,0);
    }
    public void DefaultPosition()
    {
        gameObject.transform.position = new Vector3(0,0,0);
    }
}
