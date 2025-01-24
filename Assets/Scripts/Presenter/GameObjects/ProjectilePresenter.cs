using UnityEngine;

namespace Asteroids
{
    public class ProjectilePresenter : Presenter
    {
        // ѕочти все переместить в Presenter и зависимые методы
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Transform _shipView;
        //[SerializeField] private ProjectilePresenter _prefabProjectile;

        private ShipModel _shipModel;
        private ShipMovement _shipMovement;

        private Vector2 _displaySize;
        private Vector2 _offsetPosition;

        private void Awake()
        {
            _canvas = GetComponentInParent<Canvas>();

            _displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            _offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;
            _shipModel = new ShipModel(startPosition, 0f);
            _shipMovement = new ShipMovement(_shipModel/*, _displaySize*/);
            _shipMovement.SetDisplaySize(_displaySize);
            //_shipView = Resources.Load<Transform>("Prefabs/Ship");
            //_shipView = Instantiate(_shipView, transform.parent);
            //_shipView.gameObject.SetActive(true);
            SetOverlapLayer(LayerMask.NameToLayer("Enemy"));
        }
    }
}