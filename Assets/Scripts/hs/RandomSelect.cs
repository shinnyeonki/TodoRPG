using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelect : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public int total = 0;
    public List<Item> result = new List<Item>();

    void Start()
    {
        // total 가중치 합 계산
        for (int i = 0; i < items.Count; i++)
        {
            total += items[i].weight;
        }
    }

    // 오브젝트가 클릭될 때 호출
    void OnMouseDown()
    {
        Result();
    }

    public void Result()
    {
        result.Add(RandomItem());
    }

    public Item RandomItem()
    {
        int weight = 0;
        int selectNum = Random.Range(0, total) + 1;

        for (int i = 0; i < items.Count; i++)
        {
            weight += items[i].weight;
            if (selectNum <= weight)
            {
                Item temp = new Item(items[i]);
                return temp;
            }
        }
        return null;
    }
}
