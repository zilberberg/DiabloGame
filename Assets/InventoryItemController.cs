using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public LayerMask clickableItems;

    private Vector3 currentPosition;
    private Transform currentParent;

    private Transform spriteChild;

    public Item currentItem;
    private float sizeFactor = 1f;
    public void OnBeginDrag(PointerEventData eventData)
    { 
        currentParent = gameObject.transform.parent;
        currentParent.GetComponent<SlotController>().SetIsVaccant(true);

        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        gameObject.transform.position = eventData.pointerCurrentRaycast.worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        if (eventData.pointerCurrentRaycast.gameObject && eventData.pointerCurrentRaycast.gameObject.tag != "Slot")
        {
            gameObject.transform.parent = currentParent;
            gameObject.transform.position = currentParent.position;
        } else
        {
            currentParent = gameObject.transform.parent;
            gameObject.transform.position = currentParent.transform.position;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteChild = transform.GetChild(0);
        if (currentItem)
        {
            spriteChild.transform.localScale = new Vector3(sizeFactor, sizeFactor, sizeFactor);
            spriteChild.GetComponent<SpriteRenderer>().sprite = currentItem.itemSprite;
        }
        currentPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetSpriteSize(float sizeFactor)
    {
        spriteChild.transform.localScale = new Vector3(sizeFactor, sizeFactor, sizeFactor);
    }

    public void SetItem(Item lootItem, float newSizeFactor)
    {
        currentItem = lootItem;
        sizeFactor = newSizeFactor;
    }
}
