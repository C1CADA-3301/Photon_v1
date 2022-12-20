using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class connectphoton : MonoBehaviourPunCallbacks
{
    [Header("PANELS")] 
    [Space]
    [SerializeField] private GameObject Game_Panel;
    [SerializeField] private GameObject Connecting_Panel;
    [SerializeField] private GameObject Lobby_Panel;
    
    [Header("CREATE/JOIN INPUT")]
    [Space]
    [SerializeField] private InputField CreateInput;
    [SerializeField] private InputField JoinInput;

    void Start()
    {
        Game_Panel.SetActive(true);
        Connecting_Panel.SetActive(false);
        Lobby_Panel.SetActive(false);
    }
    public void Play_btn()
    {
        PhotonNetwork.ConnectUsingSettings();
        Game_Panel.SetActive(false);
        Connecting_Panel.SetActive(true);

    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Connecting_Panel.SetActive(false);
        Lobby_Panel.SetActive(true);
    }
    
    public void CreateRoon()
    {
        PhotonNetwork.CreateRoom(CreateInput.text);
    }
    
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(JoinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("SampleScene");
    }
    void Update()
    {
        
    }
}
