using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    [Header("About this item")]
    public string itemName = "New Item";
    public string itemDescription = "Item Description";
    public GameObject itemPrefab;
    public Sprite itemSprite;
    public enum Rarity { Common, Uncommon, Rare, Epic, Legendary };
    public Rarity rarity;

    public enum Type { Helmet, Pants, Boots, Gloves, Chest, Weapon, Shield, Coins, HealthPotion };
    public Type type;

    [Header("Item attributes")]
    public float strength;
    public float dexterity;
    public float agility;
    public float vitality;

    public Color32 textColor;

    public bool shouldGlow = false;

    public void SetItemRarity()
    {
        float chance = Random.value;
        if (chance > 0.99)
        {
            //1% = legendary
            rarity = Rarity.Legendary;
            textColor = new Color32(255, 128, 0, 255);
            shouldGlow = true;
        } else if (chance > 0.9)
        {
            //10% - epic
            rarity = Rarity.Epic;
            textColor = new Color32(163, 53, 238, 255);
            shouldGlow = true;
        } else if (chance > 0.7)
        {
            //30% - rare
            rarity = Rarity.Rare;
            textColor = new Color32(0, 112, 221, 255);
            shouldGlow = true;
        } else if (chance > 0.6)
        {
            //40% - uncommon
            rarity = Rarity.Uncommon;
            textColor = new Color32(30, 255, 0, 255);
            shouldGlow = false;
        } else
        {
            //common
            rarity = Rarity.Common;
            textColor = new Color(255, 255, 255);
            shouldGlow = false;
        }
    }

    public GameObject GetLootObject()
    {
        return itemPrefab;
    }
}
