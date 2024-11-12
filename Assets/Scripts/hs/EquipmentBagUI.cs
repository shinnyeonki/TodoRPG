using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 사용 추가

public class EquipmentBagUI : MonoBehaviour
{
    public EquipmentBag equipmentBag; // EquipmentBag 스크립트 참조
    public GameObject equipmentPanelPrefab; // 장비 패널 프리팹
    public Transform contentParent; // 스크롤 뷰의 Content

    void Start()
    {
        UpdateEquipmentUI();
    }

    // 장비 UI 업데이트 메서드
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

        // 획득한 장비 목록을 반복하여 UI에 표시
        foreach (Item item in equipmentBag.acquiredItems)
        {
            // 새로운 패널 생성
            GameObject panel = Instantiate(equipmentPanelPrefab, contentParent);

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
        }
    }
}
