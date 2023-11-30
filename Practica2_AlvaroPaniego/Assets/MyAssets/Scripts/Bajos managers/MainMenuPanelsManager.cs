using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuPanelsManager : MonoBehaviour
{
    public static MainMenuPanelsManager THIS;
    void Awake()
    {
        THIS = this;
    }
    public void ShowPanel(GameObject _panel){
        _panel.SetActive(true);
    } 
    public void HidePanel(GameObject _panel){
        _panel.SetActive(false);
    }
    public void StartGame(){
        GameManager.THIS.SetState(GameStates.Gameplay);
    }
    public void ExitGame(){
        Application.Quit();
    }
}
