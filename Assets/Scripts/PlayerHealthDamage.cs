using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerHealthDamage : MonoBehaviour
{
    public float PlayerHealth = 100;
    public PhotonView view;
    public ui_Manager Ui;
    private void Start()
    {
        PlayerHealth = 100;
        view = GetComponent<PhotonView>();
        // Ui = GetComponent<ui_Manager>();
        Ui = GameObject.FindGameObjectWithTag("UI_Manager").GetComponent<ui_Manager>();
    }

    public void HealthDamage(int Damage)
    {

        PlayerHealth -= Damage;
        Debug.Log("Player health = " + PlayerHealth);
        
        if (PlayerHealth <= 0)
        {
           
            Ui.DisplayGameOverCanvas();
           view.RPC("destroyPlayer", RpcTarget.All);

            //Destroy(gameObject);
        }
    }

    [PunRPC]
    public void destroyPlayer()
    {
        Destroy(gameObject);
    }

  
    

    
}
