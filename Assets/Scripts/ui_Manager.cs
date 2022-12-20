using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ui_Manager : MonoBehaviour
{
    [SerializeField] private GameObject GameOver_Panel;
    public PhotonView View;
    public GameObject Player;

    private void Awake()
    {
        GameOver_Panel = GameObject.FindGameObjectWithTag("GameOverPanel");
    }
    private void Start()
    {
        Time.timeScale = 1;
        GameOver_Panel.SetActive(false);
        View = GetComponent<PhotonView>();
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        //if(Player==null)
        //{
        //    Time.timeScale = 0;
        //    DisplayGameOverCanvas();
        //}
    }
    //public void CallDisplayOverCanvas()
    //{
    //    View.RPC("DisplayGameOverCanvas", RpcTarget.All);
    //}
    public void DisplayGameOverCanvas()
    {
        if (!View.IsMine)
        {
            GameOver_Panel.SetActive(true);

        }

    }


    public void Quit_btn()
    {
        if (!View.IsMine)
        {
            Application.Quit();

        }
       
    }

    public void MainMenu()
    {
        if (!View.IsMine)
        {
            SceneManager.LoadScene(0);

        }
       
    }
}
