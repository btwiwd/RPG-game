using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject QuestList;
    public GameObject Buttons;
    public int EnemiesKilled = 0;
    public bool QuestEnded = false;
    public Text healthTxT;
    public GameObject Inventory;
    public GameObject WhenBuyText;
    public ShopController shop;
    public Player player;
    public Inventory inv;
    public GameObject Equipment;
    public Text Armor;
    public Slider hpSlider;
    public Dialoguemanager DM;

    private void Awake()
    {
       inv = GameObject.Find("Hud/Inventory").GetComponent<Inventory>();
       Armor = GameObject.Find("Hud/PlayerStats/Armor").GetComponent<Text>();
    }
    void Start()
    { 
        DM = GameObject.Find("Player").GetComponent<Dialoguemanager>();
        hpSlider = GameObject.Find("Hud/hp/Slider").GetComponent<Slider>();
        healthTxT = GameObject.Find("Hud/hp").GetComponent<Text>();
        Equipment = GameObject.Find("Hud/PlayerStats");
        player = GameObject.Find("Player").GetComponent<Player>();
        shop = GameObject.Find("Hud").GetComponent<ShopController>();
        WhenBuyText = GameObject.Find("Hud/Shop/WhenBuy");
        WhenBuyText.GetComponent<FadeText>().enabled = false;
        WhenBuyText.GetComponent<Text>().enabled = false;
        Inventory = GameObject.Find("Hud/Inventory");
        Inventory.SetActive(false);
        Buttons = GameObject.Find("Hud/Dialogue/Buttons");
        Buttons.SetActive(false);
        QuestList = GameObject.Find("Hud/QuestList");
        QuestList.SetActive(false);
        Equipment.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)&&!Equipment.activeInHierarchy)
        {
            Equipment.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(Input.GetKeyDown(KeyCode.Q)&&Equipment.activeInHierarchy)
        {
            Equipment.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (GameObject.Find("Player").GetComponent<Dialoguemanager>().QuestAccepted)
        {
            QuestList.transform.GetChild(0).gameObject.GetComponent<Text>().text = DialogueData.QuestName +"\n"+ DialogueData.QuestTask[0] + EnemiesKilled.ToString()+"|5";
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&!QuestList.activeInHierarchy)
        {
            QuestList.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape)&&QuestList.activeInHierarchy)
        {
            QuestList.SetActive(false);
        }
        if (EnemiesKilled == 5)
        {
            QuestList.transform.GetChild(0).gameObject.GetComponent<Text>().text = DialogueData.QuestName +"\n"+ DialogueData.QuestTask[1];
            QuestEnded = true;
            DM.CurrentNPC.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = DM.UnActiveQuest;
            Camera.main.gameObject.GetComponent<WayPoint>().target = DM.CurrentNPC.transform;
        }
        if (Input.GetKeyDown(KeyCode.I)&&!Inventory.activeInHierarchy)
        {
            player.InventoryOpen = true;
            Inventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.I)&&Inventory.activeInHierarchy)
        {
            player.InventoryOpen = false;
            Inventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    public void WhenBuy()
    {
        WhenBuyText.GetComponent<FadeText>().enabled = true;
        if (player.Coin < shop.CurrentShop.Pool.ItemPrices[shop.ActiveID])
        {
            WhenBuyText.GetComponent<Text>().text = "You have not enought money! You need"+  (shop.CurrentShop.Pool.ItemPrices[shop.ActiveID] - player.Coin).ToString() + " coins";
        }
        else
        {
            WhenBuyText.GetComponent<Text>().text = "You succesfully bought an item";
            player.Coin -= shop.CurrentShop.Pool.ItemPrices[shop.ActiveID];
            bool IsInInventory = false;
            for(int i = 0; i < inv.InventorySlots.Count; i++)
            {
                if (inv.InventorySlots[i].GetComponent<InventorySlot>().ItemID == shop.ActiveID)
                {
                    inv.InventorySlots[i].GetComponent<InventorySlot>().ItemCount++;
                    IsInInventory = true;
                    break;
                }
            }
            if (!IsInInventory)
            {
                for(int i = 0; i < inv.InventorySlots.Count; i++)
                {
                    if (inv.InventorySlots[i].GetComponent<InventorySlot>().ItemID == -1)
                    {
                        inv.InventorySlots[i].GetComponent<InventorySlot>().ItemID = shop.ActiveID;
                        inv.InventorySlots[i].transform.GetChild(1).gameObject.SetActive(true);
                        inv.InventorySlots[i].transform.GetChild(1).gameObject.GetComponent<Image>().sprite = inv.SP.ItemSprites[inv.InventorySlots[i].GetComponent<InventorySlot>().ItemID];
                        inv.InventorySlots[i].GetComponent<InventorySlot>().ItemCount = 1;
                        break;
                    }
                }
            }
            for (int i = 0; i < shop.ShopSlots.Count; i++)
            {
                if (shop.ShopSlots[i].GetComponent<Slot>().ItemID == shop.ActiveID)
                {
                    shop.CurrentShop.CurrentShop[i].ItemCount--;
                    shop.ShopSlots[i].transform.GetChild(2).GetComponent<Text>().text = shop.CurrentShop.CurrentShop[i].ItemCount.ToString();
                    if (shop.CurrentShop.CurrentShop[i].ItemCount <= 0)
                    {
                        shop.ShopSlots[i].transform.GetChild(0).GetComponent<Image>().color = shop.ShopSlots[i].GetComponent<Slot>().UnActiveColor;
                        shop.ActiveID = -1;
                        shop.ActiveSlot = null;
                    }
                    break;
                }
            }
        }
        WhenBuyText.GetComponent<FadeText>().Show = true;
    }
    public void ChangeArmor(int value)
    {
        Armor.text = "Armor: " + value;
    }
}
