using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeydownManager : MonoBehaviour
{
    public LayerMask clickableSurface;

    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private GameObject charPanel;

    [SerializeField]
    private GameObject mouseClickEffect;

    public float mouseClickEffectInterval = 1f;
    private float mouseClickEffectCounter;

    public static KeydownManager instance;
    public bool isLootUIVisible = false;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        mouseClickEffectCounter = mouseClickEffectInterval;
    }

    // Update is called once per frame
    void Update()
    {
        mouseClickEffectCounter += Time.deltaTime;
        if (Input.GetMouseButton(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(myRay, out hitInfo, 100, clickableSurface))
            {
                if (mouseClickEffectCounter >= mouseClickEffectInterval)
                {
                    mouseClickEffectCounter = 0;
                    Vector3 pos = hitInfo.point;
                    pos.y = 0.2f;
                    Instantiate(mouseClickEffect, pos, Quaternion.identity);
                }
            }            
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleChar();
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            isLootUIVisible = !isLootUIVisible;
        }
    }

    public void ToggleChar()
    {
        charPanel.SetActive(!charPanel.activeSelf);
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
}
