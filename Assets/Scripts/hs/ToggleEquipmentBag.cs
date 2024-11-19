using UnityEngine;

public class ToggleEquipmentBag : MonoBehaviour
{
    public GameObject equipmentBagPanel; // EquipmentBagPanel 오브젝트

    // 가방 열기/닫기 함수
    public void ToggleBag()
    {
        if (equipmentBagPanel != null)
        {
            // 현재 활성화 상태를 반전시킴
            equipmentBagPanel.SetActive(!equipmentBagPanel.activeSelf);
        }
    }
}
