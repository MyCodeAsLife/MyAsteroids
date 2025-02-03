using UnityEngine;

namespace Asteroids
{
    public class ProjectilePresenter : Presenter
    {
        private void Awake()
        {
            StartInit();
        }

        private void StartInit()
        {
            var center = new Vector2(0.5f, 0.5f);
            var startPosition = center * Config.PlayerExistenceLimit;

            var objModel = new ProjectileModel();
            SetModel(objModel);
            SetModelMovement(new ProjectileMovement(objModel));
            SetOverlapLayer(LayerMask.NameToLayer(Config.EnemyLayerName));
        }
    }
}