using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Players.ImputEventImpls
{
    public class KeyboardMouseInputEventProvider : MonoBehaviour, IInputEventProvider
    {
        public IObservable<bool> GetButtonAsObservable(string buttonName)
        {
            return this.ObserveEveryValueChanged(_ => Input.GetButton(buttonName));
        }

        public IObservable<Vector2> GetAxisAsObservable(string axisXName, string axisYName)
        {
            return this.UpdateAsObservable()
                .Select(_ => new Vector2(Input.GetAxis(axisXName), Input.GetAxis(axisYName)));
        }
    }
}
