using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Players;
using Zenject;


public class Timer : MonoBehaviour {

    [SerializeField]
    private Text timeLabel;

    private PlayerCore playerCore;

    [Inject]
    private void Initialize(PlayerCore playerCore)
    {
        this.playerCore = playerCore;
    }

    void Start () {
        Observable.Interval(TimeSpan.FromMilliseconds(500))
            .TakeUntil(playerCore.ClearedAsObservable())
            .Subscribe(_ =>
            {
                timeLabel.text = (int)Time.realtimeSinceStartup + "秒";
            })
            .AddTo(this);
    }
}
