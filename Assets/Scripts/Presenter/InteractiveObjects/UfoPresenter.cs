using UnityEngine;

namespace Asteroids
{
    public class UfoPresenter : Interactive
    {
        protected override void Awake()
        {
            base.Awake();
            StartInit();
        }

        public void SetAtackTarget(ShipPresenter playerShip) => ((UfoMovement)ModelMovement).SetTarget(playerShip);

        private void StartInit()
        {
            var objModel = new EnemyModel();
            SetModel(objModel);
            var modelMovement = new UfoMovement(objModel);
            SetModelMovement(modelMovement);
            SetOverlapLayer(LayerMask.NameToLayer(Config.PlayerLayerName));
        }
    }
}