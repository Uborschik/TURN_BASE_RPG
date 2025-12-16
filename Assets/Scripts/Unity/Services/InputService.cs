using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace GameUnity.Services
{
    public class InputService : IDisposable
    {
        private readonly InputActions actions;
        private readonly Camera mainCamera;

        public event Action LeftClick;
        public event Action<Vector2> WorldMousePosition;
        public event Action RightClick;

        public InputService(Camera mainCamera)
        {
            this.mainCamera = mainCamera;

            actions = new InputActions();
            actions.Enable();

            actions.Gameplay.Click.performed += ClickPerformed;
            actions.Gameplay.MousePosition.performed += WorldMousePositionPerformed;
            actions.Gameplay.RightClick.performed += RightClickPerformed;
        }

        private void ClickPerformed(InputAction.CallbackContext context) => LeftClick?.Invoke();

        private void WorldMousePositionPerformed(InputAction.CallbackContext context) =>
            WorldMousePosition?.Invoke(mainCamera.ScreenToWorldPoint(context.ReadValue<Vector2>()));

        private void RightClickPerformed(InputAction.CallbackContext context) => RightClick?.Invoke();

        public Vector2 GetScreenPosition() => mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        public void Dispose()
        {
            actions.Gameplay.Click.performed -= ClickPerformed;
            actions.Gameplay.Click.performed -= WorldMousePositionPerformed;
            actions.Gameplay.RightClick.performed -= RightClickPerformed;

            actions.Disable();
            actions.Dispose();

            LeftClick = null;
            RightClick = null;
        }
    }
}
