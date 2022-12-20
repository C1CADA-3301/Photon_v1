using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_movement : MonoBehaviour
{
    private CharacterController CharacterController;
    [SerializeField] private float Speed = 10f;
    [SerializeField] private float JumpPower = 2f;
    [SerializeField] private float Gravity = 9.81f;
    [SerializeField] private float MouseSensi = 1f;
    [SerializeField] private float ClampUpLimit = 20f;
    [SerializeField] private float ClampDownLimit = 45f;
    [SerializeField] private bool IsGrounded;
    [SerializeField] private Transform CheckSphere;
    [SerializeField] private float CheckSphereRadius = 0.5f;
    [SerializeField] private LayerMask GroundMask;
    [Header("PHOTON")]
    [SerializeField] private PhotonView View;

    [Header("ANIMATOR")]
    [SerializeField] private Animator Anim;
    [SerializeField] Transform Camholder;
    [SerializeField] Transform ShootCamholder;
    private Vector3 Y_Velocity;
    [Header("VISUAL")]
    [SerializeField] private GameObject PlayerVisual;




    private void Awake()
    {
        CharacterController = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
        View = GetComponent<PhotonView>();
    }
    void Start()
    {
       if(!View.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        // Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        if(View.IsMine)
        {
            PlayerMove();
            MouseRotation();
            PlayerJump();

        }
        
        
    }

    void PlayerMove()
    {
        float HorizontalMov = Input.GetAxis("Horizontal");
        float VerticalMov = Input.GetAxis("Vertical");

        Vector3 Movexy = transform.forward * VerticalMov + transform.right * HorizontalMov;
        CharacterController.Move(Movexy * Speed * Time.deltaTime);

       

        if (HorizontalMov>0 || HorizontalMov<0)
        {
            
            Anim.SetBool("IsWalking", true);
        }
        else if (VerticalMov > 0 || VerticalMov < 0)
        {
            Anim.SetBool("IsWalking", true);
        }

        else if (HorizontalMov==0f && VerticalMov==0f)
        {
            Anim.SetBool("IsWalking", false);
        }

    }

    void MouseRotation()
    {
        float HorizontalRot = Input.GetAxis("Mouse X");
        float VerticalRot = Input.GetAxis("Mouse Y");

        transform.Rotate(0,HorizontalRot * MouseSensi,0);
        Camholder.Rotate(-VerticalRot*MouseSensi, 0, 0);
        Vector3 CurrentCamRotation = Camholder.localEulerAngles;
        if (CurrentCamRotation.x > 180) CurrentCamRotation.x -= 360;
        CurrentCamRotation.x = Mathf.Clamp(CurrentCamRotation.x,ClampUpLimit,ClampDownLimit);
        Camholder.localRotation = Quaternion.Euler(CurrentCamRotation);
        ShootCamholder.localRotation = Quaternion.Euler(CurrentCamRotation);


    }

    void PlayerJump()
    {
        Y_Velocity.y += Gravity * Time.deltaTime;
        CharacterController.Move(Y_Velocity * Time.deltaTime);

        IsGrounded = Physics.CheckSphere(CheckSphere.position, CheckSphereRadius, GroundMask);

        
        if (IsGrounded && Y_Velocity.y<0)
        {
            Y_Velocity.y = -2f;
            Anim.SetBool("IsJump", false);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Y_Velocity.y = Mathf.Sqrt(JumpPower * -2f * Gravity);
            Anim.SetBool("IsJump", true);
            Debug.Log("jumped");
        }

    }

    
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(CheckSphere.position, CheckSphereRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CheckSphere.position, CheckSphereRadius);
    }

    





}
