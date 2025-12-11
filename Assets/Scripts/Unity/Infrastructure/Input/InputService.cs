using GameUnity.Infrastructure.Services.Interfaces;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameUnity.Infrastructure.Services
{
    public class InputService : IInputService<Vector2>, IDisposable
    {
        private readonly InputActions actions;

        public event Action LeftClick;
        public event Action RightClick;

        public InputService()
        {
            actions = new InputActions();
            actions.Enable();

            actions.Gameplay.Click.performed += ClickPerformed;
            actions.Gameplay.RightClick.performed += RightClickPerformed;
        }

        private void ClickPerformed(InputAction.CallbackContext obj) => LeftClick?.Invoke();

        private void RightClickPerformed(InputAction.CallbackContext obj) => RightClick?.Invoke();

        public Vector2 GetScreenPosition() => Mouse.current.position.ReadValue();

        public void Dispose()
        {
            actions.Gameplay.Click.performed -= ClickPerformed;
            actions.Gameplay.RightClick.performed -= RightClickPerformed;

            actions.Disable();
            actions.Dispose();

            LeftClick = null;
            RightClick = null;
        }
    }
}
