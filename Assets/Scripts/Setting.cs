using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Setting : MonoBehaviour
{
    private Slider SensitivityS;
    private Slider CamerasensitivityS;
    private Slider VolumeChangeS;
    private List<GameObject> Keyboard;
    private List<GameObject> General;
    private List<GameObject> Image;
    public Text GoRight;
    public Text GoLeft;
    public Text GoForward;
    public Text GoBack;
    public Text MainHit;
    public Image GoRightImg;
    public Image GoLeftImg;
    public Image GoForwardImg;
    public Image GoBackImg;
    public Image MainHitImg;
    public KeyBoardManager km;
    public List<KeyCode> repkeys = new List<KeyCode>();
    public Toggle FpsOn;
    public Text Quality;
    void Start() 
    {
        Quality = GameObject.Find("Settings/Graphic/Image/GraphicChange").GetComponent<Text>();
        GoRight = GameObject.Find("Settings/GoRight/Text").GetComponent<Text>();
        GoLeft = GameObject.Find("Settings/GoLeft/Text").GetComponent<Text>();
        GoForward = GameObject.Find("Settings/GoForward/Text").GetComponent<Text>();
        GoBack = GameObject.Find("Settings/GoBack/Text").GetComponent<Text>();
        MainHit = GameObject.Find("Settings/MainHit/Text").GetComponent<Text>();
        GoRightImg = GameObject.Find("Settings/GoRight").GetComponent<Image>();
        GoLeftImg = GameObject.Find("Settings/GoLeft").GetComponent<Image>();
        GoForwardImg = GameObject.Find("Settings/GoForward").GetComponent<Image>();
        GoBackImg = GameObject.Find("Settings/GoBack").GetComponent<Image>();
        MainHitImg = GameObject.Find("Settings/MainHit").GetComponent<Image>();
        km = GameObject.Find("KeyBoardManager").GetComponent<KeyBoardManager>();
        SensitivityS = GameObject.Find("Settings/Menu/Sensitivity/SensitivityChange").GetComponent<Slider>();
        SensitivityS.value = Info.Sensitivity/8000;
        Keyboard = new List<GameObject>(GameObject.FindGameObjectsWithTag("Keyboard"));
        General = new List<GameObject>(GameObject.FindGameObjectsWithTag("General"));
        Image = new List<GameObject>(GameObject.FindGameObjectsWithTag("Image"));
        FpsOn = GameObject.Find("Settings/Fps/OnFps").GetComponent<Toggle>();
        SetVisibility(Keyboard, true);
        SetVisibility(General, false);
        SetVisibility(Image, false);
    }
    
    void Update()
    {
        GoRight.text = Info.GoRight.ToString();
        GoLeft.text = Info.GoLeft.ToString();
        GoForward.text = Info.GoForward.ToString();
        GoBack.text = Info.GoBack.ToString();
        MainHit.text = Info.MainHit.ToString();
        float P = SensitivityS.value*8000;
        Info.Sensitivity = P;
        foreach(KeyCode j in repkeys){Debug.Log(j);}
        CheckIsRed();
        Info.IsFps = FpsOn.isOn;
        switch(QualitySettings.GetQualityLevel())
        {
            case 0: Quality.text = "Very Low";break;
            case 1: Quality.text = "Low";break;
            case 2: Quality.text = "Medium";break;
            case 3: Quality.text = "Hight";break;
            case 4: Quality.text = "Very Hight";break;
            case 5: Quality.text = "Ultra";break;
        }
    }
    public void ShowKeyboard() 
    {
        SetVisibility(Keyboard, true);
        SetVisibility(General, false);
        SetVisibility(Image, false);
    }
    public void ShowGeneral() 
    {
        SetVisibility(Keyboard, false);
        SetVisibility(General, true);
        SetVisibility(Image, false);
    }
    public void ShowImage() 
    {
        SetVisibility(Keyboard, false);
        SetVisibility(General, false);
        SetVisibility(Image, true);
    }
    public void SetVisibility(List<GameObject> L, bool V)
    {
        foreach(GameObject G in L)
        {
           G.SetActive(V);
        }
    }
    public void ChangeButton(string currentb)
    {
        km.enabled = true;
        km.currentb = currentb;
    }
    public void CheckButtons()
    {
        KeyCode[] keys =  { Info.GoRight, Info.GoLeft, Info.GoForward, Info.GoBack, Info.MainHit };
        for (int i = 0; i < keys.Length - 1; i++)
        {
            for (int j = i + 1; j < keys.Length; j++)
            {
                if (keys[i] == keys[j] && !repkeys.Contains(keys[i]))
                {
                    repkeys.Add(keys[i]);
                }
            }
        }
    }
    public void CheckIsRed()
    {
        KeyCode[] keys =  { Info.GoRight, Info.GoLeft, Info.GoForward, Info.GoBack, Info.MainHit };
        Image[] KeyButtons =  { GoRightImg, GoLeftImg, GoForwardImg, GoBackImg, MainHitImg };
        for (int i = 0; i < keys.Length; i++)
        {
            foreach (KeyCode j in repkeys)
            {
                if (j == keys[i])
                {
                    KeyButtons[i].color = Color.green;
                    break;
                }
                else
                {
                    KeyButtons[i].color = Color.blue;
                }
            }
        }
        repkeys.Clear();
    }
    public bool CanExit()
    {
        Image[] KeyButtons =  { GoRightImg, GoLeftImg, GoForwardImg, GoBackImg, MainHitImg };
        return KeyButtons.All(x => x.color != Color.green);
    }
    public void ChangeFps(int Number)
    {
        Application.targetFrameRate = Number;
    }
    public void IncreaseQuality()
    {
        QualitySettings.IncreaseLevel();
    }
    public void DecreaseQuality()
    {
        QualitySettings.DecreaseLevel();
    }
}
