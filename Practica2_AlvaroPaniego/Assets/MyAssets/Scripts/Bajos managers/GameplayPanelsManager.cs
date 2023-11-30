using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayPanelsManager : MonoBehaviour
{
    public static GameplayPanelsManager THIS;
    public Image batteryImg;
    public Text bulletTxt;
    // Start is called before the first frame update
    void Awake()
    {
        THIS = this;
    }
    void Start()
    {
        RefreshBatteryImg(PlayerDataManager.THIS.playerData.battery / 100);
        RefreshBulletsText(PlayerDataManager.THIS.playerData.bullets);
    }
    public void RefreshBatteryImg(float _value){
        batteryImg.fillAmount = _value;
        if(batteryImg.fillAmount > 0.75) batteryImg.color = Color.green;
        else if(batteryImg.fillAmount < 0.75 && batteryImg.fillAmount > 0.25) batteryImg.color = Color.yellow;
        else if(batteryImg.fillAmount < 0.25) batteryImg.color = Color.red;     
    }
    public void RefreshBulletsText(int _value){
        bulletTxt.text = "X" + _value;
    }
}
