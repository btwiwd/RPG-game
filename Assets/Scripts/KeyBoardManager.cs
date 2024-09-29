using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardManager : MonoBehaviour
{
    public string currentb;

    public void OnGUI()
    {
        KeyCode[] MouseKeys = { KeyCode.Mouse0, KeyCode.Mouse1, KeyCode.Mouse2, KeyCode.Mouse3, KeyCode.Mouse4, KeyCode.Mouse5, KeyCode.Mouse6 };
        for (int i = 0; i < 7; i++)
        {
            if (Input.GetMouseButtonDown(i))
            {
                switch (currentb)
                {
                    case "Right":
                        {
                            Info.GoRight = MouseKeys[i];
                            break;
                        }
                    case "Left":
                        {
                            Info.GoLeft = MouseKeys[i];
                            break;
                        }
                    case "Forward":
                        {
                            Info.GoForward = MouseKeys[i];
                            break;
                        }
                    case "Back":
                        {
                            Info.GoBack = MouseKeys[i];
                            break;
                        }
                    case "Hit":
                        {
                            Info.MainHit = MouseKeys[i];
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                GameObject.Find("Settings").GetComponent<Setting>().CheckButtons();
                enabled = false;
            }
                
        }
        if (Input.anyKey && Event.current.keyCode!= KeyCode.None)
        {
            switch (currentb)
                {
                    case "Right":
                        {
                            Info.GoRight = Event.current.keyCode;
                            break;
                        }
                    case "Left":
                        {
                            Info.GoLeft = Event.current.keyCode;
                            break;
                        }
                    case "Forward":
                        {
                            Info.GoForward = Event.current.keyCode;
                            break;
                        }
                    case "Back":
                        {
                            Info.GoBack = Event.current.keyCode;
                            break;
                        }
                    case "Hit":
                        {
                            Info.MainHit = Event.current.keyCode;
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                GameObject.Find("Settings").GetComponent<Setting>().CheckButtons();
                enabled = false;
        }
        
    }
}
