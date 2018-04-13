using System;
using UnityEngine;

namespace Players
{
    public interface IInputEventProvider
    {
        IObservable<bool> GetButtonAsObservable(string buttonName);
        IObservable<Vector2> GetAxitsAsObservable(string axisXName, string axisYName);
    }
}
