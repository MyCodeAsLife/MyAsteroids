using UnityEngine;

namespace Asteroids
{
    public class LaserPresenter : MonoBehaviour
    {
        private CapsuleCollider2D _collider;
        private ShipPresenter _ship;
        private Vector2 _laserColliderSize;
        private int _enemyLayer;                    // Получить от оружия?

        private void Start()
        {
            _enemyLayer = LayerMask.NameToLayer(Config.EnemyLayerName);
            _ship = transform.parent.parent.GetComponent<ShipPresenter>();
            _collider = GetComponent<CapsuleCollider2D>();
            CalculateLaserColliderSize();
        }

        protected void Update()
        {
            CollisionCheck();
        }

        public void CollisionCheck()
        {
            var hit = Physics2D.OverlapCapsule(transform.position, _laserColliderSize, _collider.direction, _ship.GetAngleRotation(), 1 << _enemyLayer);

            if (hit != null)
            {
                //Debug.Log("Check");                                                                         //++++++++++++++++++++++++++++
            }
        }

        private void CalculateLaserColliderSize()
        {
            _laserColliderSize = _collider.size;
            float deltaX = Mathf.Pow(transform.position.x - _ship.transform.position.x, 2f);
            float deltaY = Mathf.Pow(transform.position.y - _ship.transform.position.y, 2f);
            _laserColliderSize.y = Mathf.Sqrt(deltaX + deltaY) * 2;
        }
    }
}
