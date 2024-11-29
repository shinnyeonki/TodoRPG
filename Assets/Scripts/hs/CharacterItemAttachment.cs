using UnityEngine;

public class CharacterItemAttachment : MonoBehaviour
{
    private GameObject currentEquippedItem; // 현재 장착된 아이템

    public void EquipItem(Item item)
    {
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
        float desiredSize = 0.5f; // 크기를 1로 설정 (원본 크기 유지)
        Vector2 spriteSize = renderer.sprite.bounds.size;
        itemObject.transform.localScale = new Vector3(
            desiredSize / spriteSize.x,
            desiredSize / spriteSize.y,
            1
        );

        // 캐릭터 머리 위에 배치
        itemObject.transform.SetParent(transform);
        itemObject.transform.localPosition = new Vector3(0, 1.5f, 0); // 머리 위

        // Sorting Order를 낮게 설정하여 패널 뒤로 이동
        renderer.sortingOrder = -1; // 패널보다 뒤에 렌더링되도록 설정

        currentEquippedItem = itemObject;
        Debug.Log($"아이템 장착됨: {item.itemName}, 크기: {itemObject.transform.localScale}, Sorting Order: {renderer.sortingOrder}");
    }

    public void UnequipItem()
    {
        Debug.Log("UnequipItem 호출됨.");

        if (currentEquippedItem != null)
        {
            Destroy(currentEquippedItem);
            currentEquippedItem = null;
            Debug.Log("현재 장착 아이템 해제됨.");
        }
    }
}
