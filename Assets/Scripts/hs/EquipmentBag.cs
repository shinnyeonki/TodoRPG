using System.Collections.Generic;
using UnityEngine;

public class EquipmentBag : MonoBehaviour
{
    public List<Item> acquiredItems = new List<Item>();

    // 새로운 아이템 획득 메서드 (중복 체크)
    public void AcquireItem(Item newItem)
    {
        // 중복 아이템이 아닐 때만 추가
        if (!acquiredItems.Exists(item => item.itemName == newItem.itemName))
        {
            acquiredItems.Add(newItem);

            // EquipmentBagUI에 업데이트 요청
            EquipmentBagUI equipmentBagUI = FindObjectOfType<EquipmentBagUI>();
            if (equipmentBagUI != null)
            {
                equipmentBagUI.UpdateEquipmentUI();
            }

            // GameManager에 아이템 추가
            if (GameManager.gm != null)
            {
                GameManager.gm.AddAcquiredItem(newItem.itemName); // 아이템 이름 전달
            }
            else
            {
                Debug.LogWarning("GameManager 인스턴스를 찾을 수 없습니다.");
            }
        }
    }
}
