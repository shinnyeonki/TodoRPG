using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemGrade {S, A, B, C}
[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemImage;
    public ItemGrade itemGrade;
    public int weight;
    public Item(Item item)
    {
    this.itemName = item.itemName;
    this.itemImage = item.itemImage;
    this.itemGrade = item.itemGrade;
    this.weight = item.weight;
    }

}

