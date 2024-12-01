using UnityEngine;

public class ToggleEquipmentBag : MonoBehaviour
{
    [Header("UI Settings")]
    public GameObject equipmentBagPanel; // 가방 패널 오브젝트
    public bool startClosed = true; // UI 시작 상태

    [Header("Box Settings")]
    public GameObject box; // Box 오브젝트
    private Collider2D boxCollider; // Box의 BoxCollider2D

    void Start()
    {
        if (box != null)
        {
            boxCollider = box.GetComponent<Collider2D>();
        }

        if (equipmentBagPanel != null)
        {
            equipmentBagPanel.SetActive(!startClosed);

            var image = equipmentBagPanel.GetComponent<UnityEngine.UI.Image>();
            if (image != null)
            {
                image.raycastTarget = false; // Raycast 차단 해제
            }
        }

        SetBoxColliderEnabled(startClosed);
    }

    public void ToggleBag()
    {
        if (equipmentBagPanel != null)
        {
            bool isActive = equipmentBagPanel.activeSelf;
            equipmentBagPanel.SetActive(!isActive);
            SetBoxColliderEnabled(isActive);
        }
    }

    private void SetBoxColliderEnabled(bool isEnabled)
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = isEnabled;
        }
    }
}
