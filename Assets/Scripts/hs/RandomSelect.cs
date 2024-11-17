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
            Item selectedItem = RandomItem();
            result.Add(selectedItem);

            if (equipmentBag != null)
            {
                equipmentBag.AcquireItem(selectedItem);
            }

            if (currentPanel != null)
            {
                Destroy(currentPanel);
            }

            currentPanel = Instantiate(itemPanelPrefab, panelParent);

            Image itemImage = currentPanel.transform.Find("ItemImage")?.GetComponent<Image>();
            if (itemImage != null && selectedItem != null)
            {
                itemImage.sprite = selectedItem.itemImage;
            }

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
}
