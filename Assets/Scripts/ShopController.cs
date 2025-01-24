using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public ShopGenerator CurrentShop;
    public List<GameObject> ShopSlots;
    public GameObject BuyButton;
    public GameObject ActiveSlot = null;
    public GameObject PreviousActiveSlot = null;
    public int ActiveID = -1;
    public UIManager UI;
    public Player player;
    public Inventory inv;

    void Start()
    {
        for (int i = 0; i < GameObject.Find("Hud/Shop/ShopSlots").transform.childCount; i++)
        {
            ShopSlots.Add(GameObject.Find("Hud/Shop/ShopSlots").transform.GetChild(i).gameObject);
        }
        BuyButton = GameObject.Find("Hud/Shop/Buy");
        UI = GameObject.Find("Hud").GetComponent<UIManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        inv = GameObject.Find("Hud/Inventory").GetComponent<Inventory>();
    }
    
    void Update()
    {
        if(ActiveSlot == null)
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else if(ActiveSlot != null)
        {
            BuyButton.GetComponent<Button>().interactable = true;
        }
        if(CurrentShop != null)
        {
            // Debug.Log(ShopSlots[0].transform.GetChild(1).GetComponent<Image>().sprite != null || (CurrentShop.CurrentShop.Count > 0 && CurrentShop.CurrentShop[0].ItemCount > 0));
            for(int i = 0; i < ShopSlots.Count; i++)
            {
                if (CurrentShop.CurrentShop.Count > i && CurrentShop.CurrentShop[i].ItemCount > 0)
                {
                    ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
                    ShopSlots[i].transform.GetComponent<Button>().enabled = true;
                    ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = true;
                    ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = true;
                }
                else
                {
                    ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                    ShopSlots[i].transform.GetComponent<Button>().enabled = false;
                    ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
                    ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = false;
                }
            }
        }
    }

    public void ShowShop(ShopGenerator S)
    {
        CurrentShop = S;
        for( int i = 0; i < S.CurrentShop.Count; i++)
        {
            ShopSlots[i].GetComponent<Slot>().ItemID = S.CurrentShop[i].item.id;
            ShopSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = LoadImage(S.CurrentShop[i].item.name);
            ShopSlots[i].transform.GetChild(2).GetComponent<Text>().text = S.CurrentShop[i].ItemCount.ToString();
            ShopSlots[i].transform.GetChild(3).GetComponent<Text>().text = S.CurrentShop[i].item.price.ToString();
        }
        for (int i = S.CurrentShop.Count; i < ShopSlots.Count; i++)
        {
            ShopSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
            ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
            ShopSlots[i].transform.GetComponent<Button>().enabled = false;
            ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
            ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = false;
        }


    }
    public Sprite LoadImage(string name)
    {
        return Resources.Load<Sprite>($"ShopSprites/Icons/{name.Substring(0, name.IndexOf('_'))}/{name}");
    }
    public void WhenBuy()
    {
        UI.WhenBuyText.GetComponent<FadeText>().enabled = true;
        if (player.Coin < ShopItemsPool.ItemByID(ActiveID).price)
        {
            UI.WhenBuyText.GetComponent<Text>().text = "You have not enought money! You need" + (ShopItemsPool.ItemByID(ActiveID).price - player.Coin).ToString() + " coins";
        }
        else
        {
            UI.WhenBuyText.GetComponent<Text>().text = "You succesfully bought an item";
            player.Coin -= ShopItemsPool.ItemByID(ActiveID).price;
            inv.ItemToInventory(this);
            for (int i = 0; i < ShopSlots.Count; i++)
            {
                if (ShopSlots[i].GetComponent<Slot>().ItemID == ActiveID)
                {
                    CurrentShop.CurrentShop[i].ItemCount--;
                    ShopSlots[i].transform.GetChild(2).GetComponent<Text>().text = CurrentShop.CurrentShop[i].ItemCount.ToString();
                    if (CurrentShop.CurrentShop[i].ItemCount <= 0)
                    {
                        ShopSlots[i].transform.GetChild(0).GetComponent<Image>().color = ShopSlots[i].GetComponent<Slot>().UnActiveColor; //сделать тоже самое при закрытие магазина
                        ActiveID = -1;
                        ActiveSlot = null;
                    }
                    break;
                }
            }
        }
        UI.WhenBuyText.GetComponent<FadeText>().Show = true;
    }
}
