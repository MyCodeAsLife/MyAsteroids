using System;

namespace Asteroids
{
    internal class LaserGun : BaseGun
    {
        public readonly SingleReactiveProperty<float> ChargingTimer = new();
        public readonly SingleReactiveProperty<int> NumberOfLaserCharges = new();
        public readonly SingleReactiveProperty<int> MaxNumberOfLaserCharges = new();

        private readonly LaserPresenter _laserBeam;
        private readonly float _shotDuration;
        private readonly float _chargingTime;

        private bool _isActiveLaserBeam = false;
        private bool _isReloading = false;

        public new event Action<bool> Shot;
        public event Action<float> Reloading;


        public LaserGun(ShipPresenter ship, LaserPresenter laser, float cooldown = Config.CooldownLaserGun, float laserBeamChargingTime = Config.LaserGunChargingTime,
                        float shotDuration = Config.LaserGunShotDuration, int maxNumberOfLaserCharges = Config.MaxNumberOfLaserCharges) : base(ship, cooldown)
        {
            _laserBeam = laser;
            _shotDuration = shotDuration;
            NumberOfLaserCharges.Value = maxNumberOfLaserCharges;
            ChargingTimer.Value = laserBeamChargingTime;
            MaxNumberOfLaserCharges.Value = maxNumberOfLaserCharges;
            _chargingTime = laserBeamChargingTime;                       // Под вопросом
            Reloading += OnReloading;
        }

        public override void Tick(float deltaTime)
        {
            if (IsPressShooting && _isActiveLaserBeam == false && NumberOfLaserCharges.Value > 0 && FireRate < Timer)
            {
                Shooting();

                if (_isReloading == false)
                {
                    _isReloading = true;
                    Reloading += OnReloading;
                }

                NumberOfLaserCharges.Value--;
                Timer = 0;
            }
            else if (_isActiveLaserBeam)
            {
                if (_shotDuration < Timer)
                    Shooting();
            }

            Reloading?.Invoke(deltaTime);
            Timer += deltaTime;
        }

        protected override void Shooting()
        {
            _isActiveLaserBeam = !_isActiveLaserBeam;
            _laserBeam.transform.parent.gameObject.SetActive(_isActiveLaserBeam);
            Shot?.Invoke(_isActiveLaserBeam);
        }

        private void OnReloading(float deltaTime)
        {
            ChargingTimer.Value -= deltaTime;

            if (NumberOfLaserCharges.Value < MaxNumberOfLaserCharges.Value && ChargingTimer.Value < 0)
            {
                NumberOfLaserCharges.Value++;
                ChargingTimer.Value = _chargingTime;
            }
            else if (NumberOfLaserCharges.Value >= MaxNumberOfLaserCharges.Value)
            {
                Reloading -= OnReloading;
                _isReloading = false;
                ChargingTimer.Value = _chargingTime;
            }
        }
    }
}
