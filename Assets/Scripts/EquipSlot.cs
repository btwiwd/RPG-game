using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EquipSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int ItemID = -1;
    public ShopItemsPool SIP;
    public GameObject Description;
    public EquipmentInventory Inventory;
    public int TypeID;
    public Inventory bag;
    public bool Active;
    public Player player;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        bag = GameObject.Find("Hud/Inventory").GetComponent<Inventory>();
        Inventory = GameObject.Find("Hud/PlayerStats").GetComponent<EquipmentInventory>();
        SIP = GameObject.Find("Hud/Shop").GetComponent<ShopItemsPool>();
    }
    void Update()
    {
        GameObject ActiveSlot;
        try
        {
            ActiveSlot = GameObject.Find("Hud/Inventory/MoveItem").transform.GetChild(0).gameObject;
        }
        catch
        {
            ActiveSlot = null;
        }
        if (ActiveSlot != null && Vector3.Distance(transform.position, ActiveSlot.transform.position) < 15 && Input.GetMouseButtonUp(0) && !ActiveSlot.GetComponent<MoveItem>().ParentSlot.CompareTag("EquipSlot"))
        {
            int SlotNumber = Convert.ToInt32(ActiveSlot.GetComponent<MoveItem>().ParentSlot.gameObject.name.Replace("Slot", "")) - 1;
            int oldID = ItemID;
            if (CheckType(ActiveSlot.GetComponent<MoveItem>().ItemID))
            {
                ItemID = ActiveSlot.GetComponent<MoveItem>().ItemID;
                player.Armor += SIP.ItemsArmor[ItemID];
                transform.GetChild(1).GetComponent<Image>().sprite = SIP.ItemList[ItemID];
                transform.GetChild(1).gameObject.SetActive(true);
                if (bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemID == ItemID)
                {
                    bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemCount--;
                    if (bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemCount <= 1)
                    {
                        bag.InventorySlots[SlotNumber].transform.GetChild(2).gameObject.GetComponent<Text>().enabled = false;
                        if (bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemCount <= 0)
                        {
                            bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemCount = 0;
                            bag.InventorySlots[SlotNumber].transform.GetChild(1).gameObject.SetActive(false);
                            bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemID = -1;
                            bag.InventorySlots[SlotNumber].GetComponent<InventorySlot>().ItemCount = -1;
                        }
                    }
                }
                if (oldID != -1)
                {
                    bool isinInevntory = false;
                    for (int i = 0; i < bag.InventorySlots.Count; i++)
                    {
                        if (bag.InventorySlots[i].GetComponent<InventorySlot>().ItemID == oldID)
                        {
                            bag.InventorySlots[i].GetComponent<InventorySlot>().ItemCount++;
                            isinInevntory =true;
                            break;
                        }    
                    }
                    if (!isinInevntory)
                    {
                        for (int i = 0; i < bag.InventorySlots.Count; i++)
                        {
                            if (bag.InventorySlots[i].GetComponent<InventorySlot>().ItemID == -1)
                            {
                                bag.InventorySlots[i].GetComponent<InventorySlot>().ItemID = oldID;
                                bag.InventorySlots[i].transform.GetChild(1).gameObject.SetActive(true);
                                bag.InventorySlots[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = bag.SP.ItemList[oldID];
                                bag.InventorySlots[i].GetComponent<InventorySlot>().ItemCount = 1;
                                break;
                            }
                        }
                    }
                }
            }   
        }
    }
    public void OnPointerEnter(PointerEventData data)
    {
        if (ItemID != -1)
        {
            Description = Instantiate(Inventory.Description, transform.position - new Vector3(82, 0, 0), transform.rotation, transform);
            Description.transform.GetChild(0).GetComponent<Text>().text = SIP.ShopItemNames[ItemID];
            Description.transform.GetChild(1).GetComponent<Text>().text = "Armor: " + SIP.ItemsArmor[ItemID].ToString();
            Active = true;
            StartCoroutine(MoveItem());
        }
    }
    public void OnPointerExit(PointerEventData data)
    {
        if (ItemID != -1)
        {
            Destroy(Description);
            Active = false;
            StopCoroutine(MoveItem());
        }
    }
    public bool CheckType(int newID)
    {
        switch (TypeID)
        {
            case 1:
                {
                    if (SIP.ISHelmet(newID))
                    {
                        return true;     
                    }
                    break;
                }
            case 2:
                {
                    if (SIP.ISBodyArmor(newID))
                    {
                        return true;
                    }
                    break;
                }
            case 3:
                {
                    if (SIP.ISGloves(newID))
                    {
                        return true;
                    }
                    break;
                }
            case 5:
                {
                    if (SIP.ISBoots(newID))
                    {
                        return true;
                    }
                    break;
                }
            default: return false;
        }
        return false;
    }
    public IEnumerator MoveItem()
    {
        while (Active)
        {
            if (Input.GetMouseButton(0) && ItemID != -1)
            {
                Image Item = Instantiate(bag.DragItem, Input.mousePosition, transform.rotation, bag.gameObject.transform.Find("MoveItem").transform).GetComponent<Image>();
                Item.sprite = transform.GetChild(1).GetComponent<Image>().sprite;
                Item.GetComponent<MoveItem>().ParentSlot = gameObject;
                Item.GetComponent<MoveItem>().ItemID = ItemID;
                Item.GetComponent<MoveItem>().IsEquipment = true;
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
