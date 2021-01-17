using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootController : MonoBehaviour
{
    [SerializeField]
    private List<Item> itemList = new List<Item>();

    public static LootController instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Item GetRandomItem()
    {
        Item newItem = itemList[Random.Range(0, itemList.Count)];
        newItem.SetItemRarity();
        return newItem;
    }
}
