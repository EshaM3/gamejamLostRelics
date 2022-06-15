using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem current;
    private Dictionary<InventoryItemData, InventoryItem> m_itemDictionary;
    public List<InventoryItem> inventory; //{ get; private set; }

    private void Awake()
    {
        inventory = new List<InventoryItem>();
        m_itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();
        current = this;
    }

    public event Action onInventoryChangedEvent;
    public void InventoryChangedEvent()
    {
        if(onInventoryChangedEvent != null)
        {
            onInventoryChangedEvent();
        }
    }

    public InventoryItem Get(InventoryItemData referenceData)
    {
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

    public void Add(InventoryItemData referenceData)
    {
        //searches to see if the item of that type is in the dictionary; adds to stack value
        if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.AddToStack();
        }
        //adds a new item to public inventory item list and the dictionary
        else
        {
            InventoryItem newItem = new InventoryItem(referenceData);
            inventory.Add(newItem);
            m_itemDictionary.Add(referenceData, newItem);
        }
        InventoryChangedEvent();
    }

    public void Remove(InventoryItemData referenceData)
    {
        //if the item exists, remove one from the stack
        if (m_itemDictionary.TryGetValue(referenceData, out InventoryItem value))
        {
            value.RemoveFromStack();

            //if there are no more items left after removing, remove that item
            //from its slot in the public inventory item list and the dictionary
            if(value.stackSize == 0)
            {
                inventory.Remove(value);
                m_itemDictionary.Remove(referenceData);
            }
        }
        InventoryChangedEvent();
    }
}