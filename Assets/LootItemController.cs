using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LootItemController : MonoBehaviour
{
    public GameObject lootTextObject;
    public float pickDistanceRadius = 10f;

    private enum Rarity { Common, Rare, Epic, Legendary };
    private Rarity rarity;

    private Item lootItem;

    public GameObject particles;
    public GameObject lootLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            lootTextObject.SetActive(KeydownManager.instance.isLootUIVisible);
        }
    }

    private void OnMouseDown()
    {
        //check distance from player
        if (Vector3.Distance(transform.position, PlayerMgr.instance.transform.position) <= pickDistanceRadius)
        {
            //if has space in inventory
            if (InventoryManager.instance.GetHasOpenSlots())
            {
                InventoryManager.instance.AddItem(lootItem);
                Destroy(gameObject);
            }
        }
    }

    public void SetLoot(Item item)
    {
        lootItem = item;
        lootTextObject.GetComponent<TextMeshPro>().text = lootItem.itemName;
        lootTextObject.GetComponent<TextMeshPro>().color = lootItem.textColor;
        lootLight.GetComponent<Light>().color = lootItem.textColor;
        particles.GetComponent<ParticleSystem>().startColor = lootItem.textColor;
        if (lootItem.shouldGlow)
        {
            particles.SetActive(true);
            lootLight.SetActive(true);
        } else
        {
            particles.SetActive(false);
            lootLight.SetActive(false);
        }
    }
}
