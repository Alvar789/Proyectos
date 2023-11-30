using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VidasTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O)){
            
            for(int i = 0; i < PlayerDataManager.THIS.playerData.life; i++){
                if(i == (PlayerDataManager.THIS.playerData.life - 1)){
                    transform.GetChild(i).GetComponent<Image>().color = Color.black;
                }
            }
            PlayerDataManager.THIS.TakeDamage();
        }
        if(Input.GetKeyDown(KeyCode.H)){
            
            for(int i = PlayerDataManager.THIS.playerData.life; i >= 0; i--){
                if(i == PlayerDataManager.THIS.playerData.life && PlayerDataManager.THIS.playerData.life < 5){
                    transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }
            PlayerDataManager.THIS.Heal();
        }
    }
}
