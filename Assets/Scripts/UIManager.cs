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
    public Player player;
    public GameObject Equipment;
    public Text Armor;
    public Slider hpSlider;
    public Dialoguemanager DM;

    private void Awake()
    {
       Armor = GameObject.Find("Hud/PlayerStats/Armor").GetComponent<Text>();
    }
    void Start()
    {
        DM = GameObject.Find("Player").GetComponent<Dialoguemanager>();
        hpSlider = GameObject.Find("Hud/hp/Slider").GetComponent<Slider>();
        healthTxT = GameObject.Find("Hud/hp").GetComponent<Text>();
        Equipment = GameObject.Find("Hud/PlayerStats");
        player = GameObject.Find("Player").GetComponent<Player>();
        WhenBuyText = GameObject.Find("Hud/Shop/WhenBuy");
        WhenBuyText.GetComponent<FadeText>().enabled = false;
        WhenBuyText.GetComponent<Text>().enabled = false;
        Inventory = GameObject.Find("Hud/Inventory");
        Buttons = GameObject.Find("Hud/Dialogue/Buttons");
        Buttons.SetActive(false);
        QuestList = GameObject.Find("Hud/QuestList");
        QuestList.SetActive(false);
        Equipment.SetActive(false);
        Inventory.SetActive(false);
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
    public void ChangeArmor(int value)
    {
        Armor.text = "Armor: " + value;
    }
}
