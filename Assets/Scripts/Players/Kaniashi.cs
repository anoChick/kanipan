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
        private PlayerCore playerCore;

        [SerializeField]
        private Rigidbody2D _rigidbody;

        [SerializeField]
        private bool leftSide;

        private float forwardForce;

        private float direction;


        void Start()
        {
            forwardForce = leftSide ? -300f : 300f;
            playerCore.GetButtonAsObservable(keyCodeUp).Where(pressed => pressed)
                .SelectMany(pressed => this.FixedUpdateAsObservable(),(pressed,_) => pressed)
                .First()
                .Repeat()
                .Subscribe(x => Open())
                .AddTo(this);

            playerCore.GetButtonAsObservable(keyCodeDown).Where(pressed => pressed)
                .SelectMany(pressed => this.FixedUpdateAsObservable(), (pressed, _) => pressed)
                .First()
                .Repeat()
                .Subscribe(x => Close())
                .AddTo(this);

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
