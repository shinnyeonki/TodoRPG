using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSelect : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int total = 0;
    public List<Item> result = new List<Item>();
    public GameObject itemPanelPrefab; // 패널 프리팹
    public Transform panelParent; // 패널을 생성할 부모
    private GameObject currentPanel; // 현재 패널
    private Button confirmButton; // 확인 버튼
    public EquipmentBag equipmentBag; // EquipmentBag 스크립트 참조
    public int coinCost = 10; // 뽑기 시 필요한 코인 수

    void Start()
    {
        foreach (Item item in items)
        {
            total += item.weight;
        }
    }

    // 오브젝트가 클릭될 때 호출
    void OnMouseDown()
    {
        ShowItemResult();
    }

    public void ShowItemResult()
    {
        // 코인이 충분한지 확인
        if (GameManager.gm != null && GameManager.gm.SpendCoin(coinCost))
        {
            // 코인 차감 후 UI 업데이트
            UpdateCoinTextUI();

            // 랜덤 아이템 생성
            Item selectedItem = RandomItem();
            result.Add(selectedItem);

            // 장비 가방에 아이템 추가
            if (equipmentBag != null)
            {
                equipmentBag.AcquireItem(selectedItem);
            }

            // 기존 패널 제거
            if (currentPanel != null)
            {
                Destroy(currentPanel);
            }

            // 새 패널 생성
            currentPanel = Instantiate(itemPanelPrefab, panelParent);

            // 패널에 아이템 정보 적용
            Image itemImage = currentPanel.transform.Find("ItemImage")?.GetComponent<Image>();
            if (itemImage != null && selectedItem != null)
            {
                itemImage.sprite = selectedItem.itemImage;
            }

            // 버튼 연결
            confirmButton = currentPanel.transform.Find("Button")?.GetComponent<Button>();
            if (confirmButton != null)
            {
                confirmButton.onClick.AddListener(() => OnConfirmButtonClick(currentPanel));
            }

            currentPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("코인이 부족하여 뽑기를 진행할 수 없습니다.");
        }
    }

    private void OnConfirmButtonClick(GameObject panel)
    {
        if (panel != null)
        {
            Destroy(panel);
        }
    }

    public Item RandomItem()
    {
        int weight = 0;
        int selectNum = Random.Range(0, total) + 1;

        foreach (Item item in items)
        {
            weight += item.weight;
            if (selectNum <= weight)
            {
                return new Item(item);
            }
        }
        return null;
    }

    // 코인 UI 업데이트 메서드
  private void UpdateCoinTextUI()
{
    if (GameManager.gm != null)
    {
        Debug.Log($"현재 남은 코인: {GameManager.gm.GetCoins()}");
        // GameManager의 GetCoins()를 사용해 현재 상태 확인 (UI는 이벤트로 처리)
    }
}
}
