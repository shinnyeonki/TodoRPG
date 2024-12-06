using UnityEngine;
using UnityEngine.UI;

public class EquipmentBagUI : MonoBehaviour
{
    public GameObject equipmentPanelPrefab; // 장비 패널 프리팹
    public Transform contentParent; // Scroll View의 Content
    public GameObject itemOptionsPopup; // 장착/해제 버튼 팝업
    public CharacterItemAttachment characterAttachment; // 캐릭터 장착 관리 스크립트

    private Item selectedItem; // 선택된 아이템 정보

    void Start()
    {
        Debug.Log("EquipmentBagUI Start 호출됨.");

        if (itemOptionsPopup != null)
        {
            Debug.Log("ItemOptionsPopup 초기 상태 비활성화.");
            itemOptionsPopup.SetActive(false);
        }
        else
        {
            Debug.LogWarning("ItemOptionsPopup이 연결되지 않았습니다!");
        }
    }

    void OnEnable()
    {
        EquipmentBag.OnItemAcquired += UpdateEquipmentUI;
        UpdateEquipmentUI(); // UI 활성화 시 최신 상태로 업데이트
    }

    void OnDisable()
    {
        EquipmentBag.OnItemAcquired -= UpdateEquipmentUI;
    }

    public void UpdateEquipmentUI()
    {
        if (GameManager.gm == null)
        {
            Debug.LogWarning("GameManager가 null입니다. UpdateEquipmentUI 중단.");
            return;
        }

        Debug.Log("UpdateEquipmentUI 호출됨. 기존 패널 제거 중.");

        foreach (Transform child in contentParent)
        {
            Destroy(child.gameObject);
            Debug.Log($"패널 제거됨: {child.name}");
        }

        foreach (Item item in GameManager.gm.acquiredItems)
        {
            Debug.Log($"아이템 패널 생성 중: {item.itemName}");

            GameObject panel = Instantiate(equipmentPanelPrefab, contentParent);

            Image itemImage = panel.transform.Find("ItemImage")?.GetComponent<Image>();
            if (itemImage != null && item.itemImage != null)
            {
                Debug.Log($"아이템 이미지 설정됨: {item.itemName}");
                itemImage.sprite = item.itemImage;

                BoxCollider2D collider = itemImage.gameObject.AddComponent<BoxCollider2D>();
                collider.isTrigger = true;

                RectTransform rectTransform = itemImage.GetComponent<RectTransform>();
                collider.size = rectTransform.rect.size;
                collider.offset = Vector2.zero;
                Debug.Log($"Collider 크기 설정됨: {collider.size}");

                ItemClickHandler clickHandler = itemImage.gameObject.AddComponent<ItemClickHandler>();
                clickHandler.Initialize(item, this);
                Debug.Log($"ItemClickHandler 추가 및 초기화됨: {item.itemName}");
            }
            else
            {
                Debug.LogWarning($"아이템 이미지가 null입니다: {item.itemName}");
            }
        }
    }

    public void OnItemClick(Item item, Vector3 screenPosition)
    {
        Debug.Log($"OnItemClick 호출됨: {item.itemName}");

        selectedItem = item;

        if (itemOptionsPopup != null)
        {
            RectTransform popupRect = itemOptionsPopup.GetComponent<RectTransform>();

            // 부모 RectTransform 획득
            RectTransform parentRect = popupRect.parent as RectTransform;
            if (parentRect == null)
            {
                Debug.LogWarning("Popup의 부모가 RectTransform이 아닙니다!");
                return;
            }

            // Screen 좌표를 부모 RectTransform 기준 Local 좌표로 변환
            Vector2 localPoint;
            // eventCamera는 UI 이벤트 처리용 카메라를 의미합니다.
            // Screen Space Overlay Canvas 사용 시 null로 둘 수 있습니다.
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentRect,
                screenPosition,
                null, // Screen Space Overlay라면 null
                out localPoint
            );

            float offsetX = 50f;
            // anchoredPosition을 사용해서 부모 기준 좌표를 설정
            popupRect.anchoredPosition = localPoint + new Vector2(offsetX, 0f);

            popupRect.SetAsLastSibling(); // 팝업을 UI 계층 맨 앞으로

            itemOptionsPopup.SetActive(true);
            Debug.Log("ItemOptionsPopup 활성화 완료.");
        }
        else
        {
            Debug.LogWarning("ItemOptionsPopup이 연결되지 않았습니다!");
        }
    }


    public void OnEquipButtonClick()
    {
        Debug.Log("OnEquipButtonClick 호출됨.");
        if (selectedItem != null && characterAttachment != null)
        {
            Debug.Log($"아이템 장착 요청: {selectedItem.itemName}");
            characterAttachment.EquipItem(selectedItem);

            itemOptionsPopup.SetActive(false);
            Debug.Log("ItemOptionsPopup 비활성화 완료.");
        }
        else
        {
            Debug.LogWarning("선택된 아이템 또는 CharacterAttachment가 null입니다.");
        }
    }

    public void OnUnequipButtonClick()
    {
        Debug.Log("OnUnequipButtonClick 호출됨.");
        if (characterAttachment != null)
        {
            characterAttachment.UnequipItem();
            Debug.Log("아이템 해제됨.");
        }
        else
        {
            Debug.LogWarning("CharacterAttachment가 null입니다.");
        }
        itemOptionsPopup.SetActive(false);
    }
}
