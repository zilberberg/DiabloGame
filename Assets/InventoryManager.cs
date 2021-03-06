﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventory;

    public static InventoryManager instance;

    public GameObject inventoryItem;

    private int currentCoins = 3000;
    public GameObject coinsText; 
    private float inventoryItemScale = 15f;
    private List<GameObject> inventoryItems = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        coinsText.GetComponent<Text>().text = currentCoins.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetHasOpenSlots()
    {
        foreach (Transform child in inventory.transform)
        {
            if (child.childCount == 0)
            {
                return true;
            }
        }
        return false;
    }

    internal void AddItem(Item lootItem)
    {
        if (lootItem.isCoins)
        {
            currentCoins += lootItem.coinsAmount;
            coinsText.GetComponent<Text>().text = currentCoins.ToString();
        } else
        {
            foreach (Transform child in inventory.transform)
            {
                if (lootItem.isAggregated)
                {
                    if (child.childCount > 0)
                    {

                    }
                } else if (child.childCount == 0)
                {
                    GameObject newInventoryItem = Instantiate(inventoryItem, child.position, child.rotation);
                    inventoryItems.Add(newInventoryItem);

                    newInventoryItem.transform.SetParent(child);
                    newInventoryItem.transform.localScale = new Vector3(inventoryItemScale, inventoryItemScale, inventoryItemScale);
                    newInventoryItem.GetComponent<InventoryItemController>().SetItem(lootItem, child.GetComponent<SlotController>().sizeFactor);
                    return;
                }
            }
        }        
    }
}
