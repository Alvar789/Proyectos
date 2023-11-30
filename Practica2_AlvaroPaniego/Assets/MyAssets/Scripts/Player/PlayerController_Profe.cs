using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController_Profe : MonoBehaviour
{
    public Transform vidas;
    public float shootTimer;
    public float shootTimerLimit;
    public Vector3 mousePosition;
    public Transform cannonL;
    public Transform cannonR;
    public Rigidbody2D laser;
    // Start is called before the first frame update
    void Start()
    {
        shootTimerLimit = 0.2f;
        shootTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton (0)){

            PlayerRotation ();
            if(shootTimer < shootTimerLimit) shootTimer += Time.deltaTime;
            else{
                if(!PlayerDataManager.THIS.OutOfBullets()){
                    Shoot();
                }
            }
        }
        if(Input.GetKeyDown(KeyCode.R)){
            PlayerDataManager.THIS.RechargeBullets();
            GameplayPanelsManager.THIS.RefreshBulletsText(PlayerDataManager.THIS.playerData.bullets);
        }
    }

    void PlayerRotation (){
        
        mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 _directionToWorldPos = mousePosition - transform.position;
        transform.up = _directionToWorldPos;
    }


    void Shoot (){
        //calculo de la direccion del cañon L
        mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 _directionCannonL = mousePosition - cannonL.position;

        //calculo de la direccion del cañon R
        mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        mousePosition.z = transform.position.z;

        Vector3 _directionCannonR = mousePosition - cannonR.position;
        
        // Creacion de balas en el cañon L
        Rigidbody2D _cloneL = Instantiate(laser, cannonL.position, cannonL.rotation);
        _cloneL.transform.up = _directionCannonL;
        _cloneL.AddForce(_cloneL.transform.up * 5, ForceMode2D.Impulse);
        Destroy(_cloneL.gameObject, 5);
        // Creacion de balas en el cañon R
        Rigidbody2D _cloneR = Instantiate(laser, cannonR.position, cannonR.rotation);
        _cloneR.transform.up = _directionCannonR;
        _cloneR.AddForce(transform.up * 5, ForceMode2D.Impulse);
        Destroy(_cloneR.gameObject, 5);

        PlayerDataManager.THIS.ShootBullets();
        shootTimer = 0;
        GameplayPanelsManager.THIS.RefreshBulletsText(PlayerDataManager.THIS.playerData.bullets);
    }
    void OnCollisionEnter2D(Collision2D collision2D){
        if(collision2D.collider.CompareTag("EnemyProjectile")){
            for(int i = 0; i < PlayerDataManager.THIS.playerData.life; i++){
                if(i == (PlayerDataManager.THIS.playerData.life - 1)){
                    vidas.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }
            PlayerDataManager.THIS.TakeDamage();
            Destroy(collision2D.collider.gameObject);
        }
    }
}
