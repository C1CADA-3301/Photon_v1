using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject Player;
    public GameObject UI_Manager;
    public GameObject Canvas;
    public float MinX, MaxX, MinZ, MaxZ;
    private void Start()
    {
        Vector3 RandomPosition = new Vector3(Random.Range(MinX,MaxX),0,Random.Range(MinZ,MaxZ));
        PhotonNetwork.Instantiate(Player.name,RandomPosition,Quaternion.identity);
        SpawnUIManager();
    }

    void SpawnUIManager()
    {
        PhotonNetwork.Instantiate(Canvas.name, Vector3.zero, Quaternion.identity);
        PhotonNetwork.Instantiate(UI_Manager.name, Vector3.zero, Quaternion.identity);
        
    }
}

