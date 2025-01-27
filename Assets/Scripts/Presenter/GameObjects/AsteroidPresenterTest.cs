using UnityEngine;

namespace Asteroids
{
    public class AsteroidPresenterTest : PresenterTest
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

            var viewMovement = new ViewMovement(transform);
            SetViewMovement(viewMovement);
            var objModel = new AsteroidModelTest();
            SetModel(objModel);
            SetModelMovement(new AsteroidMovementTest(objModel));
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            objModel.Position = startPosition;
            objModel.RotationAngle = 0f;
            objModel.DegreesPerSecond = 280f;
            objModel.MovementSpeed = 0.01f;
            objModel.MaxMovementSpeed = 0.01f;
            objModel.DirectionOfRotation = 1f;
            objModel.Direction = new Vector2(1, -1);
        }
    }
}