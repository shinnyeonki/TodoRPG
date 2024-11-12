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

    void Start()
    {
        // total 가중치 합 계산
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
        Item selectedItem = RandomItem();
        result.Add(selectedItem);

        // 장비 가방에 추가
        if (equipmentBag != null)
        {
            equipmentBag.AcquireItem(selectedItem);
        }

        // 이전 패널 제거
        if (currentPanel != null)
        {
            Destroy(currentPanel);
        }

        // 패널 생성
        currentPanel = Instantiate(itemPanelPrefab, panelParent);

        // 패널에 아이템 정보 적용
        Image itemImage = currentPanel.transform.Find("ItemImage")?.GetComponent<Image>();
        if (itemImage != null && selectedItem != null)
        {
            itemImage.sprite = selectedItem.itemImage; // 이미지 할당
        }

        // 버튼 찾기
        confirmButton = currentPanel.transform.Find("Button")?.GetComponent<Button>();
        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(() => OnConfirmButtonClick(currentPanel));
        }

        currentPanel.SetActive(true);
    }

    // 확인 버튼 클릭 시 호출되는 메서드
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
