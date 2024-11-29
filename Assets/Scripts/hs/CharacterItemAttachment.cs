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

        // 새 아이템 생성
        GameObject itemObject = new GameObject(item.itemName);
        SpriteRenderer renderer = itemObject.AddComponent<SpriteRenderer>();
        renderer.sprite = item.itemImage;

        // 아이템 크기 조정
        float desiredSize = 0.5f; // 원하는 크기 (기본 크기의 50%)
        Vector2 spriteSize = renderer.sprite.bounds.size;
        itemObject.transform.localScale = new Vector3(
            desiredSize / spriteSize.x,
            desiredSize / spriteSize.y,
            1
        );

        // 캐릭터 머리 위로 배치
        itemObject.transform.SetParent(transform);
        itemObject.transform.localPosition = new Vector3(0, 1.5f, 0); // 머리 위 위치
        renderer.sortingOrder = 10; // 캐릭터 앞에 렌더링

        currentEquippedItem = itemObject;
        Debug.Log($"아이템 장착됨: {item.itemName}, 크기: {itemObject.transform.localScale}");
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
