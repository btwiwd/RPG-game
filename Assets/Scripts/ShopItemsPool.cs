using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemsPool : MonoBehaviour
{
    public List<Sprite> ItemList;
    public List<int> ItemPrices;
    public List<int> ItemsArmor;
    public List<string> ShopItemNames;
    public bool ISHelmet(int ID)
    {
        return ID >= 60 && ID <= 69? true : false;
    }
    public bool ISBodyArmor(int ID)
    {
        return ID >= 10 && ID <= 19 ? true : false;
    }
    public bool ISGloves(int ID)
    {
        return ID >= 50 && ID <= 59 ? true : false;
    }
    //public bool ISPants(int ID)
    //{
        //return ID >= 60 && ID <= 69 ? true : false;
    //}
    public bool ISBoots(int ID)
    {
        return ID >= 20 && ID <= 29 ? true : false;
    }

}
