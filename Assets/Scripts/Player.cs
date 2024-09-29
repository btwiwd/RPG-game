using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    float heightJump = 450f;
    private Vector3 m;
    public bool Grounded = true;
    private CharacterController C;
    public int hp = 100;
    public int armor = 0;
    public GameObject gameOverscreen;
    public bool isMoving = true;
    public Collider Sword;
    public float rotationSpeed = 5f;
    public Animator anim;
    public float AttackTimer;
    public float HitTime = 0;
    public float Coin = 1000;
    public Text CoinText;
    public bool InventoryOpen = false;
    public UIManager UI;
    public GameManager GM;
    public int Armor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
            UI.ChangeArmor(armor);
        }
    }
    public int Hp
    {
        get
        {
            return hp;
        }
        set
        {
            if(hp < value)
            {
                if(value < 100)
                {
                    hp = value;  
                }
                else
                {
                    hp = 100;
                }
                UI.healthTxT.text = hp.ToString();
                UI.hpSlider.value = (float)hp/100;
                return;
            }
            int blockedDamage = armor/5;
            int ActHP = value + blockedDamage;
            if(ActHP >= 0)
            {
                hp = ActHP;
                UI.healthTxT.text = hp.ToString();
                UI.hpSlider.value = (float)hp/100;
            }
            else
            {
                return;
            }
        }
    }
    void Start() 
    {
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        UI = GameObject.Find("Hud").GetComponent<UIManager>();
        CoinText = GameObject.Find("Hud/Coins").GetComponent<Text>();
        anim = GetComponent<Animator>();
        C = GetComponent<CharacterController>();
        Grounded = C.isGrounded;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameOverscreen.SetActive(false);
        Sword.enabled = false;
    }
    void Update()
    {
        if (HitTime - Time.time < -1f)
        {
            anim.SetBool("GetHit", false);
            HitTime = 0;
        }
        if (Input.GetMouseButtonDown(0)&&!InventoryOpen)
        {
            AttackTimer = Time.time;
            anim.SetBool("Attack", true);
            isMoving = false;
            Sword.enabled = true;
        }
        if (Time.time - AttackTimer > 2f)
        {
            anim.SetBool("Attack", false);
            isMoving = true;
            Sword.enabled = false;
        }
        CoinText.text = Coin.ToString();
        if (Grounded)
        {
            m.y = 0;
        }
        if (isMoving)
        {
            Move();
        }
        else 
        {
            m.x = 0;
            m.z = 0;
        }
        m.y += Physics.gravity.y * Time.deltaTime*150;
        C.Move(m * Time.deltaTime);
        Grounded = C.isGrounded;
        Rotate();
    }
    public void Move()
    {
      float verticalInput = Input.GetAxisRaw("Vertical");
      float horizontalInput = Input.GetAxisRaw("Horizontal");
      m = transform.forward * verticalInput + transform.right * horizontalInput;
      m *= moveSpeed;
      if (Grounded && Input.GetKeyDown(KeyCode.Space))
      {
        m.y = heightJump;
      }
      if (verticalInput != 0 || horizontalInput != 0)
      {
        anim.SetBool("IsRunning", true);
      }
      else
      {
        anim.SetBool("IsRunning", false);
      }
    }
    public void Rotate()
    {
        transform.Rotate(Vector3.up*Info.Sensitivity*Time.deltaTime*Input.GetAxis("Mouse X"));
    }
    public void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Enemy")
        {
            anim.SetBool("GetHit", true);
            HitTime = Time.time;
            if (!col.gameObject.GetComponent<Enemy>())
            {
                Hp -= col.gameObject.GetComponent<SwordDamage>().Damage;
            }
            else
            {
                Hp -= col.gameObject.GetComponent<Enemy>().damage;

            }
        }
        if (Hp <= 0 && GM.PlayerDead == false)
        {
            anim.SetBool("Attack", false);
            anim.SetBool("IsRunning", false);
            anim.SetBool("GetHit", false);
            anim.SetBool("Death", true); 
            gameOverscreen.SetActive(true); 
            GM.PlayerDead = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (col.gameObject.tag == "Healer")
        {
            Hp += col.gameObject.GetComponent<Healer>().heal;
        }
        if (col.gameObject.CompareTag("Immobilized"))
        {
            StartCoroutine(Immobilized());
        }
    }
    public IEnumerator Immobilized()
    {
         isMoving = false;
         yield return new WaitForSeconds(5f);
         isMoving = true;
    }
   
}
