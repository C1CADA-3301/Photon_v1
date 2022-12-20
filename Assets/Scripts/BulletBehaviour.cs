using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletBehaviour : MonoBehaviour
{
    public float BulletSpeed = 10f;
    private GameObject Player;
    Vector3 bulletDirection;
    private EnemyHealth enemiesHealth;
    public GameObject BloodParticle;
    public GameObject BloodParent;
    private PhotonView view;
   
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        //BloodParticle = GameObject.Find("BloodParticleEffect");
        BloodParent = GameObject.Find("BloodParent");
        view = GetComponent<PhotonView>();
    }

    private void Start()
    {

        gameObject.SetActive(true);
        bulletDirection = Player.transform.forward;
        
    }
    // Update is called once per frame
    void Update()
    {
       if(view.IsMine)
        {
            BulletMove();
            
        }
       

    }

    private void DestroyObjects()
    {
        StartCoroutine(BulletDie());
    }
    void BulletDestroyFunction()
    {

    }
    private void BulletMove()
    {
        Vector3 BulletMovement = bulletDirection * BulletSpeed * Time.deltaTime;
        transform.Translate(BulletMovement);
        BulletDestroyFunction();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("destroy"))
        {
            //Destroy(other.gameObject);
            Vector3 BloodPosition = gameObject.transform.position;
            enemiesHealth = other.GetComponent<EnemyHealth>();
            enemiesHealth.Enemyhealth -= 1;
            GameObject BloodParticleInstance = Instantiate(BloodParticle,BloodPosition,Quaternion.identity,BloodParent.transform);
            Destroy(gameObject);
        }
    }

    IEnumerator BulletDie()
    {
        yield return new WaitForSecondsRealtime(2f);
        Destroy(gameObject);
        
    }
}
