using UnityEngine;

public class CharacterItemAttachment : MonoBehaviour
{
    private GameObject currentEquippedItem;

    private void Start()
    {
        // 씬 로드 시 상태 복원
        if (GameManager.gm != null && GameManager.gm.currentEquippedItem != null)
        {
            if (GameManager.gm.currentEquippedItem.itemImage != null)
            {
                Debug.Log("장착된 아이템 복원 중...");
                EquipItem(GameManager.gm.currentEquippedItem);
            }
            else
            {
                Debug.LogWarning("GameManager에 장착된 아이템의 이미지가 없습니다.");
            }
        }
        else
        {
            Debug.Log("GameManager에 복원할 장착된 아이템이 없습니다.");
        }
    }

    public void EquipItem(Item item)
    {
        if (item == null)
        {
            Debug.LogError("EquipItem 호출 실패: 전달된 Item이 null입니다.");
            return;
        }

        if (item.itemImage == null)
        {
            Debug.LogError("EquipItem 호출 실패: 아이템에 이미지가 설정되지 않았습니다.");
            return;
        }

        Debug.Log($"EquipItem 호출됨: {item.itemName}");

        if (currentEquippedItem != null)
        {
            Destroy(currentEquippedItem);
            Debug.Log("기존 장착 아이템 제거됨.");
        }

        GameObject itemObject = new GameObject(item.itemName);
        SpriteRenderer renderer = itemObject.AddComponent<SpriteRenderer>();
        renderer.sprite = item.itemImage;

        // Collider 추가 및 Trigger 설정
        BoxCollider2D collider = itemObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        // 크기 조정
        float desiredSize = 0.5f;
        Vector2 spriteSize = renderer.sprite.bounds.size;
        itemObject.transform.localScale = new Vector3(
            desiredSize / spriteSize.x,
            desiredSize / spriteSize.y,
            1
        );

        // 캐릭터 머리 위에 배치
        itemObject.transform.SetParent(transform);
        itemObject.transform.localPosition = new Vector3(0, 1.5f, 0); // 머리 위
        renderer.sortingOrder = -1; // 패널보다 뒤에 렌더링되도록 설정

        currentEquippedItem = itemObject;

        // GameManager에 저장
        GameManager.gm.currentEquippedItem = item;

        Debug.Log($"아이템 장착됨: {item.itemName}, 크기: {itemObject.transform.localScale}, Sorting Order: {renderer.sortingOrder}");
    }

    public void UnequipItem()
    {
        Debug.Log("UnequipItem 호출됨.");

        if (currentEquippedItem != null)
        {
            Destroy(currentEquippedItem);
            currentEquippedItem = null;
            GameManager.gm.currentEquippedItem = null;
            Debug.Log("현재 장착 아이템 해제됨.");
        }
    }
}
