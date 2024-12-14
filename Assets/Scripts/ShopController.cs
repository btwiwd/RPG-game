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

    void Start()
    {
        for (int i = 0; i < GameObject.Find("Hud/Shop/ShopSlots").transform.childCount; i++)
        {
            ShopSlots.Add(GameObject.Find("Hud/Shop/ShopSlots").transform.GetChild(i).gameObject);
        }
        BuyButton = GameObject.Find("Hud/Shop/Buy");
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
        for(int i = 0; i < ShopSlots.Count; i++)
        {
            if(ShopSlots[i].transform.GetChild(1).GetComponent<Image>().sprite == null||CurrentShop.CurrentShop[i].ItemCount <= 0)
            {
                ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = false;
                ShopSlots[i].transform.GetComponent<Button>().enabled = false;
                ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = false;
                ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = false;
            }
            else
            {
                ShopSlots[i].transform.GetChild(1).GetComponent<Image>().enabled = true;
                ShopSlots[i].transform.GetComponent<Button>().enabled = true;
                ShopSlots[i].transform.GetChild(2).GetComponent<Text>().enabled = true;
                ShopSlots[i].transform.GetChild(3).GetComponent<Text>().enabled = true;
            }
        }
    }

    public void ShowShop(ShopGenerator S)
    {
        CurrentShop = S;
        Debug.Log(S.CurrentShop.Count);      //“”Ú
        for( int i = 0; i < S.CurrentShop.Count; i++)
        {
            ShopSlots[i].GetComponent<Slot>().ItemID = S.CurrentShop[i].ItemID;
            ShopSlots[i].transform.GetChild(1).GetComponent<Image>().sprite = LoadImage(S.CurrentShop[i].ItemSpritePath);
            ShopSlots[i].transform.GetChild(2).GetComponent<Text>().text = S.CurrentShop[i].ItemCount.ToString();
            ShopSlots[i].transform.GetChild(3).GetComponent<Text>().text = S.CurrentShop[i].ItemPrice.ToString();
        }
        
    }
    public Sprite LoadImage(string name)
    {
        return Resources.Load<Sprite>($"ShopSprites/Icons/{name.Substring(0, name.IndexOf('_')) + 's'}/{name}");
    }
}
