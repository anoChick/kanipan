using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Players
{
    public class Finger : MonoBehaviour
    {
        [SerializeField] private PlayerCore _playerCore;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Rigidbody2D _immovableRigidbody;
        [SerializeField] private float _targetRelativeAngle;

        private const string MouseClick = "Mouse 0";

        void Start()
        {
            this.FixedUpdateAsObservable().WithLatestFrom(_playerCore.GetButtonAsObservable(MouseClick), (_, b) => b)
                .Where(x => x)
                .Subscribe(_ => Clench())
                .AddTo(this);
        }

        void Clench()
        {
            _rigidbody.MoveRotation(_immovableRigidbody.rotation + _targetRelativeAngle);
        }
    }
}
