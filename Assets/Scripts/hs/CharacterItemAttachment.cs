using UnityEngine;

public class CharacterItemAttachment : MonoBehaviour
{
    public Transform characterHead; // 캐릭터 머리 위 위치
    private GameObject currentItemObject; // 현재 장착된 아이템 오브젝트
    private Item currentItem; // 현재 장착된 Item 객체

    // 아이템 장착 메서드
    public void EquipItem(Item item)
    {
        // 기존 아이템 제거
        if (currentItemObject != null)
        {
            Destroy(currentItemObject);
        }

        // 새 아이템 생성 및 장착
        if (item != null && item.itemImage != null)
        {
            GameObject newItemObject = new GameObject(item.itemName); // 아이템 이름을 가진 새 오브젝트 생성
            SpriteRenderer spriteRenderer = newItemObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.itemImage; // 아이템 이미지 설정

            // 위치 및 크기 조정
            newItemObject.transform.position = characterHead.position;
            newItemObject.transform.localScale = new Vector3(0.5f, 0.5f, 1f); // 크기 조정

            // 부모 설정
            newItemObject.transform.SetParent(characterHead);

            // 현재 장착 아이템 업데이트
            currentItemObject = newItemObject;
            Debug.Log($"{item.itemName}이 장착되었습니다.");
        }
        else
        {
            Debug.LogWarning("유효하지 않은 아이템이거나 이미지가 없습니다.");
        }
    }


    // 아이템 해제 메서드
    public void UnequipItem()
    {
        if (currentItemObject != null)
        {
            Destroy(currentItemObject);
            Debug.Log($"{currentItem.itemName} 아이템이 해제되었습니다.");
            currentItem = null;
            currentItemObject = null;
        }
        else
        {
            Debug.LogWarning("장착된 아이템이 없습니다.");
        }
    }
}
