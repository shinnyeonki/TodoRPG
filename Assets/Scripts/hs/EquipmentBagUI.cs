using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentBagUI : MonoBehaviour
{
    public EquipmentBag equipmentBag; // EquipmentBag 스크립트 참조
    public GameObject equipmentPanelPrefab; // 장비 패널 프리팹
    public Transform contentParent; // 스크롤 뷰의 Content

    void Start()
    {
        // 장비 가방의 UI 초기화
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
        foreach (var equipment in equipmentBag.acquiredItems)
        {
            GameObject panel = Instantiate(equipmentPanelPrefab, contentParent);

            // 장비 이미지 설정
            Image equipmentImage = panel.transform.Find("EquipmentImage")?.GetComponent<Image>();
            if (equipmentImage != null)
            {
                equipmentImage.sprite = equipment.itemImage;
            }

            // 장비 이름 텍스트 설정
            Text equipmentNameText = panel.transform.Find("EquipmentName")?.GetComponent<Text>();
            if (equipmentNameText != null)
            {
                equipmentNameText.text = equipment.itemName;
            }

            // 획득 상태 텍스트 설정
            Text acquiredStatusText = panel.transform.Find("AcquiredStatus")?.GetComponent<Text>();
            if (acquiredStatusText != null)
            {
                acquiredStatusText.text = "획득";
            }
        }
    }
}
