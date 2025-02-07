using UnityEngine;

namespace Asteroids
{
    public class AsteroidPresenter : Interactive
    {
        protected override void Awake()
        {
            base.Awake();
            StartInit();
        }

        private void StartInit()
        {
            var objModel = new EnemyModel();
            SetModel(objModel);
            SetModelMovement(new AsteroidMovement(objModel));
            SetOverlapLayer(LayerMask.NameToLayer(Config.PlayerLayerName));

            //SetDirectionMovement(new Vector2(1f, 1f));
            //SetMovementSpeed(0.003f);
            //SetMaxMovementSpeed(0.003f);
            //SetDirectionOfRotation(1);
            //SetDegreesPerSecond(Config.AsteroidMaxRotationSpeed);
        }
    }
}