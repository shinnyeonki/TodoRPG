using UnityEngine;
using UnityEngine.UI;

public class EquipmentBagUI : MonoBehaviour
{
    public EquipmentBag equipmentBag; // EquipmentBag 스크립트 참조
    public GameObject equipmentPanelPrefab; // 장비 패널 프리팹
    public Transform contentParent; // 스크롤 뷰의 Content
    public GameObject itemOptionsPopup; // 아이템 옵션 팝업 (장착/해제 버튼 포함)
    public CharacterItemAttachment characterAttachment; // 캐릭터 장착 스크립트 참조

    private Item selectedItem; // 현재 선택된 아이템

    void Start()
    {
        UpdateEquipmentUI();
    }

    // 장비 UI 업데이트 메서드
    public void UpdateEquipmentUI()
    {
        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in equipmentBag.acquiredItems)
        {
            GameObject panel = Instantiate(equipmentPanelPrefab, contentParent);

            // 아이템 이미지 및 버튼 설정
            Image itemImage = panel.transform.Find("ItemImage")?.GetComponent<Image>();
            if (itemImage != null && item.itemImage != null)
            {
                itemImage.sprite = item.itemImage;
            }

            Button button = panel.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => OnItemClick(item)); // 아이템 클릭 이벤트 연결
            }
        }
    }

    // 아이템 클릭 시 호출
    public void OnItemClick(Item item)
    {
        selectedItem = item; // 현재 선택된 아이템 설정
        itemOptionsPopup.SetActive(true); // 팝업 활성화
    }

    // 장착 버튼 클릭 시 호출
    public void OnEquipButtonClick()
    {
        if (selectedItem != null && characterAttachment != null)
        {
            characterAttachment.EquipItem(selectedItem);
        }
        itemOptionsPopup.SetActive(false); // 팝업 비활성화
    }

    // 해제 버튼 클릭 시 호출
    public void OnUnequipButtonClick()
    {
        if (characterAttachment != null)
        {
            characterAttachment.UnequipItem();
        }
        itemOptionsPopup.SetActive(false); // 팝업 비활성화
    }
}
