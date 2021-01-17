using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour, IDropHandler
{
    public float sizeFactor = 1f;

    public bool isVaccant = true;

    public bool isConstraint = false;
    public enum Type { Helmet, Pants, Boots, Gloves, Chest, Weapon, Shield, Any };
    public Type type = Type.Any;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool GetIsVaccant()
    {
        return isVaccant;
    }

    public void SetIsVaccant(bool vaccancy)
    {
        isVaccant = vaccancy;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (isVaccant)
        {
            isVaccant = false;
            eventData.pointerDrag.gameObject.GetComponent<InventoryItemController>().SetSpriteSize(sizeFactor);
            eventData.pointerDrag.gameObject.transform.SetParent(transform);
            eventData.pointerDrag.gameObject.transform.position = transform.position;
        }
    }
}
