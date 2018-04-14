using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players
{
    public class PlayerCore : MonoBehaviour
    {
        [SerializeField]
        private float power;

        public float Power => power;

        readonly ISubject<IInputEventProvider> _inputEventProvider = new AsyncSubject<IInputEventProvider>();

        readonly ISubject<Unit> _cleared = new AsyncSubject<Unit>();

        [Inject]
        private void Initialize(IInputEventProvider inputEventProvider)
        {
            _inputEventProvider.OnNext(inputEventProvider);
            _inputEventProvider.OnCompleted();
        }

        public IObservable<bool> GetButtonAsObservable(string buttonName)
        {
            return _inputEventProvider.ContinueWith(i => i.GetButtonAsObservable(buttonName));
        }

        public IObservable<Vector2> GetAxisAsObservable(string axisXName, string axisYName)
        {
            return _inputEventProvider.ContinueWith(i => i.GetAxisAsObservable(axisXName, axisYName));
        }

        public IObservable<Unit> ClearedAsObservable()
        {
            return _cleared;
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.name == "GoalFlag")
            {
                _cleared.OnNext(Unit.Default);
                _cleared.OnCompleted();
            }
        }
    }
}
