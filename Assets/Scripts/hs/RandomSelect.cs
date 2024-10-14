using System.Collections;
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

    void Start()
    {
        // total 가중치 합 계산
        for (int i = 0; i < items.Count; i++)
        {
            total += items[i].weight;
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
            // 버튼 클릭 이벤트에 함수 연결
            confirmButton.onClick.AddListener(() => OnConfirmButtonClick(currentPanel));
        }

        // 패널 활성화
        currentPanel.SetActive(true);
    }

    // 확인 버튼 클릭 시 호출되는 메서드
    private void OnConfirmButtonClick(GameObject panel)
    {
        // 패널 제거 (혹은 비활성화)
        if (panel != null)
        {
            Destroy(panel); // 패널을 완전히 제거
            // 또는 panel.SetActive(false); // 패널을 비활성화만 할 경우
        }
    }

    public Item RandomItem()
    {
        int weight = 0;
        int selectNum = Random.Range(0, total) + 1;

        for (int i = 0; i < items.Count; i++)
        {
            weight += items[i].weight;
            if (selectNum <= weight)
            {
                Item temp = new Item(items[i]);
                return temp;
            }
        }
        return null;
    }
}
