using System;

namespace GameUnity.Infrastructure.Services.Interfaces
{
    public interface IInputService<T>
    {
        event Action LeftClick;
        event Action RightClick;

        T GetScreenPosition();
    }
}
