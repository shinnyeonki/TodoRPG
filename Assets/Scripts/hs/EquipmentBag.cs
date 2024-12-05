using System;
using UnityEngine;

public class EquipmentBag : MonoBehaviour
{
    public static EquipmentBag Instance; // 싱글톤 인스턴스

    public static event Action OnItemAcquired; // 아이템 획득 이벤트

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시 유지
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // 중복 인스턴스 제거
        }
    }

    public void AcquireItem(Item newItem)
    {
        if (newItem == null)
        {
            Debug.LogWarning("Failed to call AcquireItem: The provided Item is null.");
            return;
        }

        // 중복 아이템 체크 및 추가
        if (!GameManager.gm.acquiredItems.Exists(item => item.itemName == newItem.itemName))
        {
            GameManager.gm.acquiredItems.Add(newItem);
            Debug.Log($"{newItem.itemName} has been added to GameManager.");

            // 아이템 획득 이벤트 발행
            OnItemAcquired?.Invoke();
        }
        else
        {
            Debug.Log($"{newItem.itemName} is already acquired. Preventing duplicate addition.");
        }
    }
}
