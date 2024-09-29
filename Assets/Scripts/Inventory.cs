using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject PreviousActiveSlot = null;
    public List<GameObject> InventorySlots;
    public ShopItemsPool SP;
    public int ActiveID = -1;
    public GameObject ActiveSlot = null;
    public GameObject DragItem;
    void Update()
    {

    }
}
