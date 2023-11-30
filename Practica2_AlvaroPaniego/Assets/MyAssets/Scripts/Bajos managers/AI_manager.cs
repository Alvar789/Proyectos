using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_manager : MonoBehaviour
{
    public static AI_manager THIS;
    public Oleadas state;
    public GameObject enemyPrefab;
    public float distanciaEnemigos;
    public int oleadaActual;
    public int maxEnemigos;
    public  GameObject[] currentEnemies;
    void Awake()
    {
        THIS = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        oleadaActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) CrearOleada();
        if(Input.GetKeyDown(KeyCode.Q)) EliminarOleada();
    }
    public void SetState(Oleadas _newState){
        state = _newState;

        switch(state){
            case Oleadas.HoldGameplay:
            break;

            case Oleadas.Gameplay:
            break;
        }
    }
    public void HoldGameplayCase(){

    }
    public void GameplayCase(){
        CrearOleada();
        GameManager.THIS.timerOleada = 0;
    }
    public void CrearOleada(){
        oleadaActual++;
        currentEnemies = new GameObject[maxEnemigos];
        for (int i = 0; i < maxEnemigos; i++)
        {
            Vector3 posicionInicial = Vector3.right * i * (distanciaEnemigos) - Vector3.right * (maxEnemigos -1) * distanciaEnemigos * 0.5f;
            GameObject _clone = Instantiate(enemyPrefab, posicionInicial, enemyPrefab.transform.rotation);
            currentEnemies[i] = _clone;

            _clone.transform.SetParent(transform);// este metodo emparenta el clon a quien tenga asignado el script.
        }
    }
    void EliminarOleada(){
        for (int i = transform.childCount - 1; i >= 0; i--) // Bucle for invertido para que vaya del ultimo al primero.
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
public enum Oleadas
{
    HoldGameplay = 0,
    Gameplay = 1
}
