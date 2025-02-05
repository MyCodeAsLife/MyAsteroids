using UnityEngine;

namespace Asteroids
{
    public class AsteroidPresenter : Interactive
    {
        private void Awake()
        {
            StartInit();
        }

        private void StartInit()
        {
            var objModel = new EnemyModel();
            SetModel(objModel);
            SetModelMovement(new AsteroidMovement(objModel));
            SetOverlapLayer(LayerMask.NameToLayer(Config.PlayerLayerName));
        }
    }
}