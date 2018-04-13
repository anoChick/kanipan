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
            // samples
            GetButtonAsObservable("Q").Subscribe(x => Debug.Log("Q: " + x));
            GetAxitsAsObservable("Mouse X", "Mouse Y").Subscribe(x => Debug.Log("Axis: " + x)).AddTo(this);
        }

        [Inject]
        private void Initialize(IInputEventProvider inputEventProvider)
        {
            _inputEventProvider.OnNext(inputEventProvider);
            _inputEventProvider.OnCompleted();
        }

        IObservable<bool> GetButtonAsObservable(string buttonName)
        {
            return _inputEventProvider.ContinueWith(i => i.GetButtonAsObservable(buttonName));
        }

        IObservable<Vector2> GetAxitsAsObservable(string axisXName, string axisYName)
        {
            return _inputEventProvider.ContinueWith(i => i.GetAxitsAsObservable(axisXName, axisYName));
        }
    }
}
