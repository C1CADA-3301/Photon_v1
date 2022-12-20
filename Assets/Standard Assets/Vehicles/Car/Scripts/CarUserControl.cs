using Photon.Pun;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Photon.Pun;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]

  
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use


        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();

            m_Car.Anim = GetComponent<Animator>();
            m_Car.View = GetComponent<PhotonView>();
        }


        private void FixedUpdate()
        {
            if (m_Car.View.IsMine)
            {
                float h = CrossPlatformInputManager.GetAxis("Horizontal");
                float v = CrossPlatformInputManager.GetAxis("Vertical");
#if !MOBILE_INPUT
                float handbrake = CrossPlatformInputManager.GetAxis("Jump");
                m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif

            }
            // pass the input to the car!

        }
    }
}
