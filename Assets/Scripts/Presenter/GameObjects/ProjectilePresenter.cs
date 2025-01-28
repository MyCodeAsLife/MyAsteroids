using UnityEngine;

namespace Asteroids
{
    public class ProjectilePresenter : Presenter
    {
        private const string LayerEnemy = "Enemy";

        private void Awake()
        {
            StartInit();
        }

        private void StartInit()
        {
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.ScaleWindowSize;

            var objModel = new ProjectileModel();
            SetModel(objModel);
            SetModelMovement(new ProjectileMovement(objModel));
            SetOverlapLayer(LayerMask.NameToLayer(LayerEnemy));

            //objModel.Position = startPosition;
            //objModel.MovementSpeed = 0.001f;
            //objModel.MaxMovementSpeed = 0.001f;
            //objModel.Direction = new Vector2(-1, 1);
            
            // Взять угол поворота у коробля
        }
    }
}