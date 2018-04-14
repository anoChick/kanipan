using System;
using Players;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class ResultUi : MonoBehaviour
    {
        [SerializeField] private GameObject _resultGameObject;
        [SerializeField] private Text _timeText;

        [Inject] private PlayerCore _playerCore;

        void Start()
        {
            _timeText.color = Color.white;
            _playerCore.ClearedAsObservable()
                .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(2)))
                .Do(_ => ShowResult())
                .SelectMany(_ => Observable.Timer(TimeSpan.FromSeconds(5)))
                .Subscribe(_ => SceneManager.LoadScene("Main"))
                .AddTo(this);
        }

        void ShowResult()
        {
            _resultGameObject.SetActive(true);

            _timeText.color = Color.black;
        }
    }
}
