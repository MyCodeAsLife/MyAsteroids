using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids
{
    public class RootController : MonoBehaviour
    {
        private UserInputActions _inputActions;

        public bool IsPaused { get; private set; }              // Положение форматирования

        public event Action MovementStarted;
        public event Action MovementCanceled;
        public event Action<float> RotationStarted;
        public event Action RotationCanceled;
        public event Action ShootingFromFirstGunStarted;
        public event Action ShootingFromFirstGunCanceled;
        public event Action ShootingFromSecondGunStarted;
        public event Action ShootingFromSecondGunCanceled;
        public event Action PauseMenuPressed;

        private void OnEnable()
        {
            _inputActions = new UserInputActions();
            _inputActions.Enable();

            _inputActions.Controls.Move.started += OnMovementStart;
            _inputActions.Controls.Move.canceled += OnMovementCancel;
            _inputActions.Controls.Rotate.started += OnRotationStart;
            _inputActions.Controls.Rotate.canceled += OnRotationCancel;
            _inputActions.Controls.ShootFromFirstGun.started += OnShootingFromFirstGunStart;
            _inputActions.Controls.ShootFromFirstGun.canceled += OnShootingFromFirstGunCancel;
            _inputActions.Controls.ShootFromSecondGun.started += OnShootingFromSecondGunStart;
            _inputActions.Controls.ShootFromSecondGun.canceled += OnShootingFromSecondGunCancel;
            _inputActions.Controls.PauseMenu.started += OnPauseMenuPress;
            PauseMenuPressed += OnPausePress;
        }

        private void OnDisable()
        {
            _inputActions.Disable();

            _inputActions.Controls.Move.started -= OnMovementStart;
            _inputActions.Controls.Move.canceled -= OnMovementCancel;
            _inputActions.Controls.Rotate.started -= OnRotationStart;
            _inputActions.Controls.Rotate.canceled -= OnRotationCancel;
            _inputActions.Controls.ShootFromFirstGun.started -= OnShootingFromFirstGunStart;
            _inputActions.Controls.ShootFromFirstGun.canceled -= OnShootingFromFirstGunCancel;
            _inputActions.Controls.ShootFromSecondGun.started -= OnShootingFromSecondGunStart;
            _inputActions.Controls.ShootFromSecondGun.canceled -= OnShootingFromSecondGunCancel;
            _inputActions.Controls.PauseMenu.started -= OnPauseMenuPress;
        }

        private void OnMovementStart(InputAction.CallbackContext context) => MovementStarted?.Invoke();
        private void OnMovementCancel(InputAction.CallbackContext context) => MovementCanceled?.Invoke();
        private void OnRotationStart(InputAction.CallbackContext context) => RotationStarted?.Invoke(-context.action.ReadValue<Vector2>().x);
        private void OnRotationCancel(InputAction.CallbackContext context) => RotationCanceled?.Invoke();
        private void OnShootingFromFirstGunStart(InputAction.CallbackContext context) => ShootingFromSecondGunStarted?.Invoke();
        private void OnShootingFromFirstGunCancel(InputAction.CallbackContext context) => ShootingFromSecondGunCanceled?.Invoke();
        private void OnShootingFromSecondGunStart(InputAction.CallbackContext context) => ShootingFromFirstGunStarted?.Invoke();
        private void OnShootingFromSecondGunCancel(InputAction.CallbackContext context) => ShootingFromFirstGunCanceled?.Invoke();
        private void OnPauseMenuPress(InputAction.CallbackContext context) => PauseMenuPressed?.Invoke();
        private void OnPausePress() => IsPaused = !IsPaused;
    }
}