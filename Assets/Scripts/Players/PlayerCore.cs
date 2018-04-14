using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Players
{
    public class PlayerCore : MonoBehaviour
    {
        readonly ISubject<IInputEventProvider> _inputEventProvider = new AsyncSubject<IInputEventProvider>();

        void Start()
        {
        }

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

        IObservable<Vector2> GetAxitsAsObservable(string axisXName, string axisYName)
        {
            return _inputEventProvider.ContinueWith(i => i.GetAxisAsObservable(axisXName, axisYName));
        }
    }
}
