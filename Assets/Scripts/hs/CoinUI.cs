using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI coinText; // 코인 값을 표시할 UI 텍스트

    void Start()
    {
        if (GameManager.gm != null)
        {
            GameManager.gm.OnCoinsUpdated += RefreshCoinUI;
            UpdateCoinText(GameManager.gm.GetCoins());
            Debug.Log("CoinUI: Start에서 GameManager 이벤트를 강제로 구독");
        }
        else
        {
            Debug.LogWarning("CoinUI: GameManager가 초기화되지 않았습니다.");
        }
    }


    void OnEnable()
    {
        if (GameManager.gm != null)
        {
            GameManager.gm.OnCoinsUpdated += RefreshCoinUI;
            Debug.Log("CoinUI: OnCoinsUpdated 이벤트 구독 성공");
        }
    }

    void OnDisable()
    {
        if (GameManager.gm != null)
        {
            GameManager.gm.OnCoinsUpdated -= RefreshCoinUI;
            Debug.Log("CoinUI: OnCoinsUpdated 이벤트 구독 해제");
        }
    }

    // GameManager의 현재 코인 값을 가져와 UI를 업데이트
    private void RefreshCoinUI(int coins)
    {
        UpdateCoinText(coins);
    }

    // UI 텍스트를 업데이트하는 메서드
    private void UpdateCoinText(int coins)
    {

        if (coinText != null)
        {
            coinText.text = $"coin: {coins}";
        }
        else
        {
            Debug.LogWarning("coinText가 설정되지 않았습니다.");
        }
    }
}
