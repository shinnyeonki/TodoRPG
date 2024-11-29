using UnityEngine;

public class ItemClickHandler : MonoBehaviour
{
    private Item item;
    private EquipmentBagUI equipmentBagUI;

    public void Initialize(Item item, EquipmentBagUI equipmentBagUI)
    {
        this.item = item;
        this.equipmentBagUI = equipmentBagUI;
        Debug.Log($"ItemClickHandler 초기화됨: {item.itemName}");
    }

    void OnMouseDown()
    {
        Debug.Log($"OnMouseDown 호출됨. 클릭된 아이템: {item.itemName}");
        if (equipmentBagUI != null)
        {
            Debug.Log($"EquipmentBagUI로 OnItemClick 전달: {item.itemName}");
            equipmentBagUI.OnItemClick(item, transform.position);
        }
        else
        {
            Debug.LogWarning("EquipmentBagUI가 null입니다!");
        }
    }
}
