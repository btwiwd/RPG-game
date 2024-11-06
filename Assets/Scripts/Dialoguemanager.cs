using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialoguemanager : MonoBehaviour
{
    public Text ActiveDialogue;
    public bool ReadyToDialogue = false;
    public Text DialogueField;
    public int ReplicNumber = 1;
    public bool QuestAccepted = false;
    public List<string> Replics;
    public int ReplicsCount = 0;
    public Button NextReplicButton;
    public bool WaitingForQuest = false;
    public bool NPCEnabled = true;
    public string CurrentNPCname;
    public GameObject Shop;
    public Color ActiveQuest;
    public Color UnActiveQuest;
    public GameObject CurrentNPC;
    public GameObject QuestTarget;
    public GameObject Waypoint;
    public ShopController ShopC;
    public ShopGenerator CurrentNPCshop;
    void Start()
    {
        ShopC = GameObject.Find("Hud").GetComponent<ShopController>();
        Waypoint = GameObject.Find("Hud/WayPoint");
        Waypoint.SetActive(false);
        QuestTarget = GameObject.Find("Spawner");
        Shop = GameObject.Find("Hud/Shop");
        Shop.SetActive(false);
        NextReplicButton = GameObject.Find("Hud/Dialogue/DialogueField/NextReplic").GetComponent<Button>();
        DialogueField = GameObject.Find("Hud/Dialogue/DialogueField").GetComponent<Text>();
        DialogueField.enabled = false;
        ActiveDialogue = GameObject.Find("Hud/Dialogue/ActiveDialogue").GetComponent<Text>();
        ActiveDialogue.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&ReadyToDialogue)
         {
            GetComponent<Animator>().SetBool("IsRunning", false);
            GetComponent<Animator>().SetBool("Attack", false);
            GetComponent<Animator>().SetBool("Death", false);
            GetComponent<Animator>().SetBool("GetHit", false);
            GetComponent<Player>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            DialogueField.enabled = true;
            if (CurrentNPCname == "Villager")
            {
            if (!QuestAccepted)
            {
                DialogueField.text = DialogueData.Replics[0];
                Replics = DialogueData.Replics;
                ReplicsCount = DialogueData.Replics.Count;
                ReplicNumber = 1;
                NextReplicButton.enabled = true;
                QuestAccepted = true;
            }
            if (WaitingForQuest)
            {
                DialogueField.text = DialogueData.WaitingReplic;
                NextReplicButton.enabled = false;
                WaitingForQuest = false;
            }
            if (GameObject.Find("Hud").GetComponent<UIManager>().QuestEnded)
            {
                DialogueField.text = DialogueData.FinishReplics[0];
                Replics = DialogueData.FinishReplics;
                ReplicsCount = DialogueData.FinishReplics.Count;
                ReplicNumber = 1;
                NextReplicButton.enabled = true;
                GameObject.Find("Hud").GetComponent<UIManager>().QuestEnded = false;
            }
         }
         else if (CurrentNPCname == "Trader")
         {
            Shop.SetActive(true);
            ShopC.ShowShop(CurrentNPCshop);
            DialogueField.enabled = false;
            ActiveDialogue.enabled = false;
         }
         }
         if ( Input.GetKeyDown(KeyCode.Escape)&&ReadyToDialogue)
         {
            GetComponent<Player>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            DialogueField.enabled = false;
            GameObject.Find("Hud").GetComponent<UIManager>().Buttons.SetActive(false);
            Shop.SetActive(false);
         }
         if (ReplicNumber == 2)
         {
            AddNewQuest();
         }
    }
    public void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "NPC" || col.gameObject.tag == "Merchant")
            {
                ActiveDialogue.enabled = false;
                ReadyToDialogue = false;
                CurrentNPCshop = null;
            }
    }
    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "NPC" && NPCEnabled)
            {
                ActiveDialogue.enabled = true;
                 ReadyToDialogue = true;
                 CurrentNPCname = "Villager";
                 CurrentNPC = col.gameObject.transform.parent.gameObject;
            }
            if (col.gameObject.tag == "Merchant")
            {
                ActiveDialogue.enabled = true;
                 ReadyToDialogue = true;
                 CurrentNPCname = "Trader";
                 CurrentNPCshop = col.gameObject.GetComponent<ShopGenerator>();
            } 
    }
    public void NextReplic()
    {
        if (ReplicNumber < ReplicsCount)
        {
        DialogueField.text = Replics[ReplicNumber];
        ReplicNumber++;
        }
    }
    public void AddNewQuest()
    {
        GameObject.Find("Hud").GetComponent<UIManager>().Buttons.SetActive(true);
        ReplicNumber = 1;
    }
    public void AcceptQuest()
    {
        GetComponent<Player>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DialogueField.enabled = false; 
        GameObject.Find("Hud").GetComponent<UIManager>().Buttons.SetActive(false);
        if (GameObject.Find("Hud").GetComponent<UIManager>().QuestEnded)
        {
            GameObject.Find("Player").GetComponent<Player>().Coin += 50;
            NPCEnabled = false;
            CurrentNPC.transform.GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            Waypoint.SetActive(false);
            Camera.main.gameObject.GetComponent<WayPoint>().QuestTaken = true;
        }
        else
        {
            QuestAccepted = true;
            CurrentNPC.transform.GetChild(0).GetChild(0).GetComponent<Image>().color = ActiveQuest;
            Waypoint.SetActive(true);
            Camera.main.gameObject.GetComponent<WayPoint>().target = QuestTarget.transform;
            Camera.main.gameObject.GetComponent<WayPoint>().QuestTaken = true;
        }
    }
    public void CancelQuest()
    {
        GetComponent<Player>().enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DialogueField.enabled = false;
        GameObject.Find("Hud").GetComponent<UIManager>().Buttons.SetActive(false);
    }
    public void CloseShop()
    {
        GetComponent<Player>().enabled = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            DialogueField.enabled = false;
            GameObject.Find("Hud").GetComponent<UIManager>().Buttons.SetActive(false);
            for(int i = 0; i < Shop.GetComponent<ShopItemsPool>().CurrentItemsCount; i++)
            {
                ShopC.ShopSlots[i].GetComponent<Slot>().DeactivateSlot();
            }
            ShopC.ActiveSlot = null;
            ShopC.PreviousActiveSlot = null;
            ShopC.ActiveID = -1;
            Shop.SetActive(false);
    }
}
