using System;
using System.Collections.Generic;
using System.Text;

namespace GameUnity.Adapters.Interfaces
{
    public interface IGridRenderer
    {
        void DrawCell(int x, int y, bool isWalkable);
        void Clear();
    }
}
