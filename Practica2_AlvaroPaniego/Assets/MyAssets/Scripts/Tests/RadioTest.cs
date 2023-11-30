using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTest : MonoBehaviour
{
    public bool estaDescargando;
    public float firstTap, timer, descargandoLimit;
    // Start is called before the first frame update
    void Start()
    {
        descargandoLimit = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        if(estaDescargando){
            if(PlayerDataManager.THIS.playerData.battery > 0f){
                PlayerDataManager.THIS.SpendBatteryFaster(2f);
                GameplayPanelsManager.THIS.RefreshBatteryImg(PlayerDataManager.THIS.playerData.battery / 100f);
            }else {
                PlayerDataManager.THIS.playerData.battery = 0;
                if(GameManager.THIS.states != GameStates.GameOver_Battery)GameManager.THIS.SetState(GameStates.GameOver_Battery);
            }
        }else{
            if(PlayerDataManager.THIS.playerData.battery < 100f) PlayerDataManager.THIS.RechargeBattery();
            else{
                PlayerDataManager.THIS.playerData.battery = 100f;
            }
        }
        timer += Time.deltaTime;
        if(estaDescargando != IsInLimit()) estaDescargando = IsInLimit();
        if(Input.GetKeyDown(KeyCode.E)) firstTap = timer;


        /*PlayerDataManager.THIS.SpendBattery();
        if(Input.GetKeyDown(KeyCode.L)) PlayerDataManager.THIS.RechargeBattery(10f);
        if(PlayerDataManager.THIS.IsBatteryEmpty()) GameManager.THIS.SetState(GameStates.GameOver_Battery);
        Debug.Log(PlayerDataManager.THIS.playerData.battery);*/
    }
    bool IsInLimit(){
        return timer - firstTap >= descargandoLimit;
    }
}
