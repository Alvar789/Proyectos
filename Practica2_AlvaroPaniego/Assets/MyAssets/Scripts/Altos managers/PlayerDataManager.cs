using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager THIS;
    public PlayerData playerData;
    void Awake()
    {
        // uso de SINGLETON:
        // patron de diseño que permite asegurar
        // la asignacion y gestion de la misma instancia
        // en la navegacion entre escenas
        if(THIS == null){
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }else Destroy(gameObject);
    }
    public void TakeDamage(){
        playerData.life--;
        if(playerData.life <= 0)GameManager.THIS.SetState(GameStates.GameOver_Destroyed);
        if(playerData.life < 0) playerData.life = 0;
        Debug.Log("Vidas: " + playerData.life);
    }
    public void Heal(){
        playerData.life++;
        if(playerData.life > 5) playerData.life = 5;
        Debug.Log("Vidas: " + playerData.life);
    }
    public void ShootBullets(){
        playerData.bullets--;
    }
    public bool OutOfBullets(){
        if(playerData.bullets == 0)return true;
        else return false;
    }
    public void SpendBattery(){
        playerData.battery -= Time.deltaTime;
    }
    public void SpendBatteryFaster(float _value){
        playerData.battery -= _value * Time.deltaTime;
    }
    public void RechargeBattery(){
        playerData.battery += Time.deltaTime;
    }
    public bool IsBatteryEmpty(){
        if(playerData.battery <= 0) return true;
        else return false;
    }
    public void RechargeBullets(){
        playerData.bullets = 20;
    }
    public void RechargeBattery(float _cantidad){
        playerData.battery += _cantidad;
    }
}
[Serializable]
public class PlayerData
{
    public int life = 5;
    public int bullets = 20;
    public  float battery = 100f;
}
