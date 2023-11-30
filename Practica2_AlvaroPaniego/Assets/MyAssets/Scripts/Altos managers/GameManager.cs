using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager THIS;
    public int contador;
    public float timerVictory;
    public float timerVictoryLimit;
    public float timerOleada;
    public float timerOleadaLimit;
    public GameStates states;
    public bool estaDescargando;
    public float firstTap, timer, descargandoLimit;
    // Start is called before the first frame update
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
    void Start()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) SetState(GameStates.InitApp);
        else SetState(GameStates.Gameplay);

        timerVictory = 0;
        timerVictoryLimit = 120;
        timerOleada = 0;
        timerOleadaLimit = 3;
        descargandoLimit = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(states == GameStates.Gameplay){
            if(timerVictory < timerVictoryLimit) timerVictory += Time.deltaTime;
            else SetState(GameStates.Win);

            if(AI_manager.THIS.state == Oleadas.HoldGameplay){
                if(timerOleada < timerOleadaLimit) timerOleada += Time.deltaTime;
                else AI_manager.THIS.SetState(Oleadas.Gameplay);
            }
            if(AI_manager.THIS.state == Oleadas.Gameplay){
                for (int i = 0; i < AI_manager.THIS.currentEnemies.Length; i++) {
                    if(AI_manager.THIS.currentEnemies[i] == null) contador++;

                    if (contador == AI_manager.THIS.currentEnemies.Length)
                    {
                        AI_manager.THIS.SetState(Oleadas.HoldGameplay);
                        contador = 0;
                    }
                }//el metodo lenght no va a funcionar porque solo devuelve el tamaño del array.
                //hay que ahcer un bucle for para recorrer el array y ver si todos son nulos usando un contador
            }
        }
        //proceso de carga y descarga de la bateria de la radio
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
    public void SetState(GameStates _newState){
        states = _newState;
        Debug.Log("*** NUEVO ESTADO "+ states + " ***");
        switch(states){
            case GameStates.InitApp:
            InitAppCase();
            break;
            // ---------------------------
            // ---------------------------
            case GameStates.Gameplay:
            GameplayCase();
            break;
            // ---------------------------
            // ---------------------------
            case GameStates.GameOver_Battery:
            GameOver_BatteryCase();
            break;
            // ---------------------------
            case GameStates.GameOver_Destroyed:
            GameOver_Destroyed();
            break;
            // ---------------------------
            case GameStates.Win:
            Win();
            break;
            // ---------------------------

        }
    }
    void InitAppCase(){

    }

    void GameplayCase(){
        SceneManager.LoadScene(1);
    }

    void GameOver_BatteryCase(){

    }
    void GameOver_Destroyed(){

    }
    void Win(){

    }
    bool IsInLimit(){
        return timer - firstTap >= descargandoLimit;
    }
}
// Enumerador que contiene los distintos estados del juego
public enum GameStates{

    InitApp = 0,
    Gameplay = 1,
    GameOver_Battery = 2,
    GameOver_Destroyed = 3,
    Win = 4
}
