using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool FirstClick = true;
    public Color ActiveColor;
    public Color UnActiveColor;
    public Inventory Inv;
    public int ItemID = -1;
    public bool Active = false;
    public int itemCount = 0;
    public ShopItemsPool SIP;
    public Player player;
    public int ItemCount
    {
        get
        {
            return itemCount;
        }
        set
        {
            itemCount = value;
            transform.GetChild(2).GetComponent<Text>().text = itemCount.ToString();
            if (itemCount > 1)
            { 
                transform.GetChild(2).GetComponent<Text>().enabled = true;
            }
            else
            {
                transform.GetChild(2).GetComponent<Text>().enabled = false;
            }
        }
    }
    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        SIP = GameObject.Find("Hud/Shop").GetComponent<ShopItemsPool>();
    }

    void Start()
    {
        Inv = GameObject.Find("Hud").GetComponent<UIManager>().inv;
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
        if (ActiveSlot != null && Vector3.Distance(transform.position, ActiveSlot.transform.position) < 15 && Input.GetMouseButtonUp(0) && (ItemID == -1|| ItemID == ActiveSlot.GetComponent<MoveItem>().ItemID))
        {
            if (ItemID == -1)
            {
                ItemCount++;
                ItemID = ActiveSlot.GetComponent<MoveItem>().ItemID;
                transform.GetChild(1).GetComponent<Image>().sprite = SIP.ItemList[ItemID];
                transform.GetChild(1).gameObject.SetActive(true);
            }
            else if (ActiveSlot.GetComponent<MoveItem>().ItemID == ItemID)
            {
                ItemCount++;
                transform.GetChild(2).gameObject.GetComponent<Text>().enabled = true;

            }
            if (ActiveSlot.GetComponent<MoveItem>().IsEquipment)
            {
                player.Armor -= SIP.ItemsArmor[ItemID];
                ActiveSlot.GetComponent<MoveItem>().ParentSlot.GetComponent<EquipSlot>().ItemID = -1;
                ActiveSlot.GetComponent<MoveItem>().ParentSlot.transform.GetChild(1).gameObject.SetActive(false);
            }
            else if(ActiveSlot.GetComponent<MoveItem>().ParentSlot.name == gameObject.name)
            {
                ItemCount--;
                return;
            }
            else
            {
                for (int i = 0; i < Inv.InventorySlots.Count; i++)
                {
                    if (ActiveSlot.GetComponent<MoveItem>().ParentSlot == Inv.InventorySlots[i])
                    {
                        ActiveSlot.GetComponent<MoveItem>().ParentSlot.GetComponent<InventorySlot>().ItemCount--;
                        if (ActiveSlot.GetComponent<MoveItem>().ParentSlot.GetComponent<InventorySlot>().ItemCount <= 1 )
                        {
                            ActiveSlot.GetComponent<MoveItem>().ParentSlot.transform.GetChild(2).GetComponent<Text>().enabled = false;
                            if (ActiveSlot.GetComponent<MoveItem>().ParentSlot.GetComponent<InventorySlot>().ItemCount <= 0)
                            {
                                ActiveSlot.GetComponent<MoveItem>().ParentSlot.GetComponent<InventorySlot>().ItemID = -1;
                                Inv.InventorySlots[i].transform.GetChild(1).gameObject.SetActive(false);
                            }
                        }
                        break;
                    }
                }
            }
        }
    }
    public void Click()
    {
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            if (FirstClick && ItemID != -1)
            {
                transform.GetChild(0).GetComponent<Image>().color = ActiveColor;
                FirstClick = false;
                if (Inv.PreviousActiveSlot != null)
                {
                    Inv.PreviousActiveSlot.GetComponent<InventorySlot>().DeactivateSlot();
                }
                if (Inv.ActiveSlot == null)
                {
                    Inv.PreviousActiveSlot = gameObject;
                }
                Inv.PreviousActiveSlot = gameObject;
                Inv.ActiveSlot = gameObject;
                Inv.ActiveID = ItemID;
            }
            else if (!FirstClick)
            {
                DeactivateSlot();
            }
        }
    }
    public void DeactivateSlot()
    {
        transform.GetChild(0).GetComponent<Image>().color = UnActiveColor;
        FirstClick = true;
        Inv.ActiveSlot = null;
        Inv.PreviousActiveSlot = null;
        Inv.ActiveID = -1;
    }
    public void OnPointerEnter(PointerEventData data)
    {
        Active = true;
        StartCoroutine(MoveItem());       
    }
    public void OnPointerExit(PointerEventData data)
    {
        Active = false;
        StopCoroutine(MoveItem());
    }   
    public IEnumerator MoveItem()
    {
        while (Active && Inv.ActiveSlot == null)
        {
            if (Input.GetMouseButton(0)&& ItemID != -1)
            {
                transform.GetChild(0).GetComponent<Image>().color = ActiveColor;
                Inv.ActiveSlot = gameObject;
                Image Item = Instantiate(Inv.DragItem, Input.mousePosition, transform.rotation, Inv.gameObject.transform.Find("MoveItem").transform).GetComponent<Image>();
                Item.sprite = transform.GetChild(1).GetComponent<Image>().sprite;
                Item.GetComponent<MoveItem>().ParentSlot = gameObject;
                Item.GetComponent<MoveItem>().ItemID = ItemID;
                yield break;
            }
            else
                Inv.ActiveSlot = null;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
