using System;
using System.Linq;
using System.Collections;
using UniRx;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;

namespace Players
{
    public class Kaniashi : MonoBehaviour
    {
        [SerializeField]
        private string keyCodeUp;

        [SerializeField]
        private string keyCodeDown;

        [SerializeField]
        private bool leftSide;

        [SerializeField]
        private PlayerCore playerCore;

        [SerializeField]
        private Rigidbody2D _rigidbody;


        private float forwardForce;

        private float direction;


        void Start()
        {
            var keyCodes = new[] { keyCodeUp, keyCodeDown };
            forwardForce = leftSide ? -playerCore.Power : playerCore.Power;

            keyCodes
                .Select(x => KeyPressedAsObservable(x))
                .Merge()
                .Subscribe(OnPressedKey)
                .AddTo(this);
        }

        void OnPressedKey(string keyCode)
        {
            if (keyCode == keyCodeUp)
            {
                Open();
            }
            else
            {
                Close();
            }
        }

        IObservable<string> KeyPressedAsObservable(string keyCode)
        {
            return playerCore.GetButtonAsObservable(keyCode)
                .Where(pressed => pressed)
                .Select(_ => keyCode)
                .SelectMany(c => this.FixedUpdateAsObservable(), (c, _) => c)
                .First()
                .Repeat();
        }

        void Open()
        {
            _rigidbody.AddTorque(forwardForce);
        }

        void Close()
        {
            _rigidbody.AddTorque(-forwardForce);
        }
    }
}
