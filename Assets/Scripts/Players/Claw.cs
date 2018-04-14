using UniRx;
using UnityEngine;

namespace Players
{
    public class Claw : MonoBehaviour
    {
        [SerializeField] private PlayerCore _playerCore;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _centorTransform;
        [SerializeField] private Vector2 _centorOffset;
        [SerializeField] private float _range;

        private const string AxisXName = "Mouse X";
        private const string AxisYName = "Mouse Y";

        private static readonly Color gizmoColor = Color.yellow;


        void Start()
        {
            _playerCore.GetAxisAsObservable(AxisXName, AxisYName)
                .ObserveOn(Scheduler.MainThreadFixedUpdate)
                .Select(x => LimitedPosition(_rigidbody.position + x, Centor(), _range))
                .Subscribe(Move)
                .AddTo(this);
        }

        private void Move(Vector2 position)
        {
            _rigidbody.MovePosition(position);
        }

        private static Vector2 LimitedPosition(Vector2 position, Vector2 offset, float range)
        {
            var relative = position - offset;
            if (relative.sqrMagnitude > Mathf.Pow(range, 2))
            {
                return relative.normalized * range + offset;
            }
            return position;
        }

        Vector2 Centor()
        {
            return _centorTransform.position + _centorTransform.rotation * _centorOffset;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            var gizmoSize = _range;
            Gizmos.DrawWireSphere(Centor(), gizmoSize);
        }
    }
}
