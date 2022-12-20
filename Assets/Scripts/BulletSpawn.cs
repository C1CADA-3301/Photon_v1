using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class BulletSpawn : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject GunBarrel;
    public GameObject Gun;
    public float BulletSpeed = 10f;
    public GameObject BulletParent;
    public GameObject CrosshairCanvas;
    public Camera ShootCam;
    private Animator anim;
    private PhotonView view;
    [SerializeField] private Camera ADS_cam;
    private RaycastHit Hit;
    public float Range =100f;
    public int GunDamage = 20;
    public GameObject HitEffectVFX;
    public GameObject BloodEffectVFX;

    // GameObject Bullet;

    private void Awake()
    {
        
        anim = GetComponent<Animator>();
        BulletParent = GameObject.FindGameObjectWithTag("Bullet_Parent");
        view = GetComponent<PhotonView>();
    }
    void Start()
    {
        CrosshairCanvas.SetActive(false);
        Gun.SetActive(false);
        GunBarrel.SetActive(false);
        ShootCam.depth = -2;
        if (!view.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (view.IsMine)
        {
            ADS();

        }
            

    }

    void ADS()
    {
            if (Input.GetMouseButton(1))
            {

                CrosshairCanvas.SetActive(true);
                GunBarrel.SetActive(true);
                Gun.SetActive(true);
                ShootCam.depth = 0;
                anim.SetLayerWeight(1, 1);
                if (Input.GetMouseButtonDown(0))
                {

                RPC_Shoot();


                    //Vector3 bulletmovement = transform.forward * BulletSpeed * Time.deltaTime;
                    //Bullet.transform.Translate(bulletmovement);
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                CrosshairCanvas.SetActive(false);
                GunBarrel.SetActive(false);
                Gun.SetActive(false);
                anim.SetLayerWeight(1, 0);
                ShootCam.depth = -2;
            }
        
    }

    
    void RPC_Shoot()
    {
        ProcessRayCast();
        HitEffect_VFX();
        //GameObject Bullet = PhotonNetwork.Instantiate(BulletPrefab.name, GunBarrel.transform.position, Quaternion.identity);
        //Bullet.transform.SetParent(BulletParent.transform);
    }

    void ProcessRayCast()
    {
        if (Physics.Raycast(ADS_cam.transform.position, ADS_cam.transform.forward, out Hit, Range))
        {
            PlayerHealthDamage Target = Hit.transform.GetComponent<PlayerHealthDamage>();
            Debug.Log("I hit " + Hit.transform.name);
            if (Target == null)
                return;
            Target.HealthDamage(GunDamage);
            BloodEffect_VFX();
           


        }
        else
        {
            return;

        }

    }

    private void HitEffect_VFX()
    {
        if (Hit.transform != null)
        {
            GameObject HitEffect = PhotonNetwork.Instantiate(HitEffectVFX.name, Hit.point, Quaternion.LookRotation(Hit.normal));
            Destroy(HitEffect, 0.2f);
            
        }

    }

    private void BloodEffect_VFX()
    {
        if (Hit.transform != null)
        {
            GameObject BloodEffect = PhotonNetwork.Instantiate(BloodEffectVFX.name, Hit.point, Quaternion.LookRotation(Hit.normal));
            Destroy(BloodEffect, 0.2f);

        }

    }


}
