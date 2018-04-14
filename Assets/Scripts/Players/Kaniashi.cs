using System.Collections;
using UniRx;
using System.Collections.Generic;
using UnityEngine;


namespace Players
{
    public class Kaniashi : MonoBehaviour
    {
        [SerializeField]
        private string keyCodeUp;

        [SerializeField]
        private string keyCodeDown;

        [SerializeField]
        private PlayerCore playerCore;

        [SerializeField]
        private Rigidbody2D rigidbody;

        [SerializeField]
        private bool leftSide;

        private float forwardForce;

        private float direction;


        void Start()
        {
            forwardForce = leftSide ? -700f : 700f;
            playerCore.GetButtonAsObservable(keyCodeUp).Where(pressed => pressed).Subscribe(x => rigidbody.AddTorque(forwardForce));
            playerCore.GetButtonAsObservable(keyCodeDown).Where(pressed => pressed).Subscribe(x => rigidbody.AddTorque( -forwardForce));
        }
    }
}
