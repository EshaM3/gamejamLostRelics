using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIInventoryItemSlot : MonoBehaviour
{
    [SerializeField]
    private Image m_icon;

    [SerializeField]
    private TextMeshProUGUI m_label;

    [SerializeField]
    private GameObject m_stackObj; //this isn't used right now
                                   //but is kept track of in case
                                   //we do use it

    [SerializeField]
    private TextMeshProUGUI m_stackLabel;

    //makes the inventory item box UI
    public void Set(InventoryItem item)
    {
        m_icon.sprite = item.data.icon;
        m_label.text = item.data.displayName;
        m_stackLabel.text = item.stackSize.ToString();
    }
}
