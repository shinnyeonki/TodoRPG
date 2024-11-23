using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentBagUI : MonoBehaviour
{
    public EquipmentBag equipmentBag; // EquipmentBag 스크립트 참조
    public GameObject equipmentPanelPrefab; // 장비 패널 프리팹
    public Transform contentParent; // 스크롤 뷰의 Content

    // 칸 나누기 설정
    public int columns = 4; // 한 줄에 배치할 칸 수
    public float cellWidth = 80f; // 각 칸의 너비
    public float cellHeight = 80f; // 각 칸의 높이
    public float spacing = 10f; // 칸 간격

    void Start()
    {
        UpdateEquipmentUI();
    }

    public void UpdateEquipmentUI()
    {
        if (equipmentBag == null)
        {
            Debug.LogWarning("EquipmentBag reference is missing!");
            return;
        }

        // 기존 패널 제거
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        int index = 0; // 아이템 인덱스

        // 획득한 장비 목록을 반복하여 UI에 표시
        foreach (Item item in equipmentBag.acquiredItems)
        {
            // 새로운 패널 생성
            GameObject panel = Instantiate(equipmentPanelPrefab, contentParent);

            // 위치 계산
            int row = index / columns; // 현재 아이템의 행 번호
            int col = index % columns; // 현재 아이템의 열 번호
            float xPos = col * (cellWidth + spacing); // x 좌표 계산
            float yPos = -row * (cellHeight + spacing); // y 좌표 계산 (위에서 아래로)

            // 패널 위치 설정
            RectTransform panelRect = panel.GetComponent<RectTransform>();
            if (panelRect != null)
            {
                panelRect.anchoredPosition = new Vector2(xPos, yPos);
            }

            // 패널 내의 이미지 설정
            Image itemImage = panel.transform.Find("ItemImage")?.GetComponent<Image>();
            if (itemImage != null && item.itemImage != null)
            {
                itemImage.sprite = item.itemImage;
            }
            else
            {
                Debug.LogWarning($"Item image is missing for {item.itemName}");
            }

            // 패널 내의 아이템 이름 텍스트 설정 (TextMeshProUGUI)
            TextMeshProUGUI itemNameText = panel.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            if (itemNameText != null)
            {
                itemNameText.text = item.itemName;
            }
            else
            {
                Debug.LogWarning("ItemName Text component is missing in equipmentPanelPrefab.");
            }

            index++; // 인덱스 증가
        }

        // ContentParent의 크기 조정 (스크롤 가능)
        RectTransform contentRect = contentParent.GetComponent<RectTransform>();
        if (contentRect != null)
        {
            int totalRows = Mathf.CeilToInt((float)equipmentBag.acquiredItems.Count / columns); // 전체 행 계산
            float totalHeight = totalRows * (cellHeight + spacing); // 전체 높이 계산
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, totalHeight);
        }
    }
}
