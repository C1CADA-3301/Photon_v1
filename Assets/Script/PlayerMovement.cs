using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float Speed = 5f;
    PhotonView View;
    void Start()
    {
        View = GetComponent<PhotonView>();
    }

   
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {


        if(View.IsMine)
        {
            Vector2 PlayerInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            transform.Translate(PlayerInput * Speed * Time.deltaTime);
        }
       

    }
}
