using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
  public void reload()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }  
   public void Play()
  {
    SceneManager.LoadScene("SampleScene");
  }  
  public void Setting()
  {
    SceneManager.LoadScene("Settings");
  } 
  public void Menu()
  {
    if (GameObject.Find("Settings").GetComponent<Setting>().CanExit())
    {SceneManager.LoadScene("Menu");}
    else 
    {
      GameObject.Find("Settings/Exit").GetComponent<FadeText>().Show = true;
    }
  }   
  public void SimpleMenu()
  {
    SceneManager.LoadScene("Menu");
  }
}
