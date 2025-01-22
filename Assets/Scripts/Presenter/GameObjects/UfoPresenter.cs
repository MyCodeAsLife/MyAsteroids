using UnityEngine;

namespace Asteroids
{
    public class UfoPresenter : Presenter
    {
        // Почти все переместить в Presenter и зависимые методы
        //[SerializeField] private Canvas _canvas;
        [SerializeField] private ShipPresenter _playerShip;
        //// Этот блок переметить в Presenter?
        //[SerializeField] private Transform _objectView;                  // Временно цепляем вручную
        //private Transformable _objectModel;
        //private Movement _objectMovement;


        //private Vector2 _displaySize;
        //private Vector2 _offsetPosition;

        private void Awake()
        {
            //_displaySize = _canvas.renderingDisplaySize / _canvas.scaleFactor;
            //_offsetPosition = _displaySize / 2 * Config.ScaleWindowSize;
            var center = new Vector2(0.1f, 0.1f);
            var startPosition = center * Config.ScaleWindowSize;
            var model = new UfoModel(startPosition, 0f);
            this.SetModel(model);
            this.SetMovement(new UfoMovement(model, /*_displaySize,*/ _playerShip));
            //_enemyView = Resources.Load<Transform>("Prefabs/Ship");                     // EnemyPresenter переделать под конкретный Presenter?
            //_enemyView = Instantiate(_enemyView, transform.parent);
            //_enemyView.gameObject.SetActive(true);
            SetOverlapLayer(LayerMask.NameToLayer("Player"));
        }

        //private void Update()        // Переместить в Presenter?
        //{
        //    float deltaTime = Time.deltaTime;

        //    MoveObjectModel(deltaTime);
        //    MoveObjectView(deltaTime);
        //}

        //private void MoveObjectModel(float deltaTime)        // Переместить в Presenter?
        //{
        //    _objectMovement.Tick(deltaTime);
        //}

        //private void MoveObjectView(float deltaTime)        // Переместить в Presenter
        //{
        //    var correctPosition = (_displaySize * _objectModel.Position) - _offsetPosition;
        //    _objectView.localPosition = correctPosition;
        //    _objectView.rotation = Quaternion.Euler(0f, 0f, _objectModel.RotationAngle);
        //}
    }
}