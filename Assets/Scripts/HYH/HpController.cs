using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider gauge;
    public TextMeshProUGUI gaugeText;

    private void Start()
    {
        gauge.value = GameManager.gm.GetHp();    
    }

    void Update()
    {
        gauge.value = GameManager.gm.GetHp();
        gaugeText.text = GameManager.gm.GetHp().ToString();
    }

    public void DamageTest()
    {
        GameManager.gm.GainHP(-10);
        if (GameManager.gm.GetHp() <= 0)
        {
            GameManager.gm.SetScore(0);
            Debug.Log("score : "+GameManager.gm.GetScore());
            GameManager.gm.SetHp(0);
            return;
        }
    }
}
