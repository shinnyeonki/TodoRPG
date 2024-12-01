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
        // Box Collider2D 자동 검색
        if (box != null)
        {
            boxCollider = box.GetComponent<Collider2D>();
        }

        // 초기 상태 설정
        if (equipmentBagPanel != null)
        {
            equipmentBagPanel.SetActive(!startClosed);
        }

        // Box Collider 초기 상태 설정
        SetBoxColliderEnabled(startClosed);
    }

    public void ToggleBag()
    {
        if (equipmentBagPanel != null)
        {
            bool isActive = equipmentBagPanel.activeSelf;
            equipmentBagPanel.SetActive(!isActive);

            // Box Collider 활성화/비활성화
            SetBoxColliderEnabled(isActive);
        }
    }

    private void SetBoxColliderEnabled(bool isEnabled)
    {
        if (boxCollider != null)
        {
            boxCollider.enabled = isEnabled; // Collider 활성화/비활성화
        }
    }
}
