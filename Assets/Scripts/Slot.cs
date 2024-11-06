using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Color ActiveColor;
    public Color UnActiveColor;
    public bool FirstClick = true;
    public int ItemID = -1;
    public ShopController shop;

    void Start()
    {
        shop = GameObject.Find("Hud/Shop").GetComponent<ShopController>();
    }
    public void Click()
    {
        if (FirstClick)
        {
            transform.GetChild(0).GetComponent<Image>().color = ActiveColor;
            FirstClick = false;
            if (shop.PreviousActiveSlot != null)
            {
                shop.PreviousActiveSlot.GetComponent<Slot>().DeactivateSlot();
            }
            if (shop.ActiveSlot == null)
            {
                shop.PreviousActiveSlot = gameObject;
            }
            shop.ActiveSlot = gameObject;
            shop.ActiveID = ItemID;
        }
        else if(!FirstClick)
        {
            DeactivateSlot();
        }
    }
    public void DeactivateSlot()
    {
        transform.GetChild(0).GetComponent<Image>().color = UnActiveColor;
        FirstClick = true;
        shop.ActiveSlot = null;
        shop.PreviousActiveSlot = null;
        shop.ActiveID = -1;
    }
}
