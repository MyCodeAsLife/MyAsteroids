using UnityEngine;

namespace Asteroids
{
    public class AsteroidPresenter : Presenter
    {
        private const string LayerEnemy = "Player";

        private void Awake()
        {
            StartInit();
        }

        private void StartInit()
        {
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;
            var objModel = new AsteroidModel();

            SetModel(objModel);
            SetModelMovement(new AsteroidMovement(objModel));
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            objModel.Position = startPosition;
            objModel.DegreesPerSecond = 280f;
            objModel.MovementSpeed = 0.001f;
            objModel.MaxMovementSpeed = 0.001f;
            objModel.DirectionOfRotation = 1f;
            objModel.Direction = new Vector2(0, 1);
        }
    }
}