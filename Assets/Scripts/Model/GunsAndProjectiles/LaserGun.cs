using System;

namespace Asteroids
{
    internal class LaserGun : BaseGun
    {
        private readonly LaserPresenter _laserBeam;
        private readonly float _shotDuration;
        private readonly float _chargingTime;
        private readonly int _maxNumberOfLaserCharges;

        private int _numberOfLaserCharges;                  // Если сделать реактивным свойством, то удобно будет значения выводить на экран, а также запускать и останавливать подписку на перезарядку
        private float _chargingTimer;
        private bool _isActiveLaserBeam = false;
        private bool _isReloading = false;

        public override event Action Shot;
        public event Action<float> Reloading;

        public LaserGun(ShipPresenter ship, LaserPresenter laser, float cooldown = Config.CooldownLaserGun, float laserGunChargingTime = Config.LaserGunChargingTime,
                        float shotDuration = Config.LaserGunShotDuration, int maxNumberOfLaserCharges = Config.MaxNumberOfLaserCharges) : base(ship, cooldown)
        {
            _laserBeam = laser;
            _shotDuration = shotDuration;
            _numberOfLaserCharges = maxNumberOfLaserCharges;
            _maxNumberOfLaserCharges = maxNumberOfLaserCharges;
            _chargingTime = laserGunChargingTime;
        }

        public override void Tick(float deltaTime)
        {
            if (IsPressShooting && _isActiveLaserBeam == false && _numberOfLaserCharges > 0 && FireRate < Timer)
            {
                Shooting();

                if (_isReloading == false)
                {
                    _isReloading = true;
                    Reloading += OnReloading;
                }

                _numberOfLaserCharges--;
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
            Shot?.Invoke();
        }

        private void OnReloading(float deltaTime)
        {
            // Выводить на экран перезарядку?
            // Выводить на экран кол-во зарядов лазера

            _chargingTimer += deltaTime;

            if (_numberOfLaserCharges < _maxNumberOfLaserCharges && _chargingTime < _chargingTimer)
            {
                _numberOfLaserCharges++;
                _chargingTimer = 0;
            }
            else if (_numberOfLaserCharges >= _maxNumberOfLaserCharges)
            {
                Reloading -= OnReloading;
                _isReloading = false;
            }
        }
    }
}
