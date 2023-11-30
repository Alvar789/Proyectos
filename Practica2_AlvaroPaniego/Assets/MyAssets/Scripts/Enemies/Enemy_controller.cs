using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_controller : MonoBehaviour
{
    public EnemyStates state;
    public Rigidbody2D rb;
    public Transform rayOrigin;
    [Range (0f, 10.0f)]
    public float rayLenght;
    public float direccionRot;
    public  float stateTimerLimit;
    public float stateTimer;
    public float shootTimerLimit;
    public float shootTimer;
    // Start is called before the first frame update
    void Start()
    {
        shootTimerLimit = 0.2f;
        shootTimer = 0;
        stateTimerLimit = 0.2f;
        stateTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == EnemyStates.Quieto){
            if(stateTimer < stateTimerLimit) stateTimer += Time.deltaTime;
            else SetState(EnemyStates.Girando);
        }
        if(state == EnemyStates.Girando)transform.Rotate(-Vector3.forward);
        if(state == EnemyStates.PlayerDetectado){
            if(stateTimer < stateTimerLimit) stateTimer += Time.deltaTime;
            else{
                stateTimer = 0;
                if(shootTimer < shootTimerLimit) shootTimer += Time.deltaTime;
                else{
                    Shoot();
                    shootTimer = 0;
            }
        }
    }
    }
    void FixedUpdate()
    {
        UpRaycast();
    }
    void UpRaycast(){
        RaycastHit2D _hit = Physics2D.Raycast(rayOrigin.position, transform.up, rayLenght);
        
        if(_hit.collider == null){
            Debug.DrawRay(rayOrigin.position, transform.up * rayLenght, Color.green);
            if(state != EnemyStates.Girando) SetState(EnemyStates.Girando);
        }
        else {
            Debug.DrawRay(rayOrigin.position, transform.up * _hit.distance, Color.red);
            if(_hit.collider.gameObject.CompareTag("Player") && state != EnemyStates.PlayerDetectado) SetState(EnemyStates.PlayerDetectado);
        }
    }
    void SetState(EnemyStates _newState){
        state = _newState;
        Debug.Log("*** NUEVO ESTADO " + state + " ***");
        switch (state)
        {
            case EnemyStates.Quieto:
                QuietoCase();
            break;
            case EnemyStates.Girando:
            break;
            case EnemyStates.PlayerDetectado:
            break;
            
        }

    }
    void QuietoCase(){
        int _random = Random.Range(0,2);
        if(_random == 1) direccionRot = 1f;
        else if(_random == 0) direccionRot = -1f;
    }
    void Shoot(){
        Rigidbody2D _clone = Instantiate(rb, rayOrigin.position, rayOrigin.rotation);
        
        _clone.AddForce(transform.up * 5, ForceMode2D.Impulse);
        Destroy(_clone.gameObject, 5);
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.collider.CompareTag("PlayerProjectile")){
            Destroy(collision2D.gameObject);
            Destroy(gameObject);
        }
    }
}
public enum EnemyStates
{
    Quieto = 0,
    Girando = 1,
    PlayerDetectado = 2    
}

