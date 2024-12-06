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
    public GameObject gameOverPanel;

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
        Debug.Log("Damage Test");
        GameManager.gm.GainHP(-10);
        if (GameManager.gm.GetHp() <= 0)
        {
            gameOverPanel.SetActive(true);
            return;
        }
    }
}
