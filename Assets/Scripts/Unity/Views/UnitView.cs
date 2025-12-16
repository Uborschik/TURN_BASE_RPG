using GameCore.Models;
using UnityEngine;

namespace GameUnity.Views
{
    public class UnitView
    {
        public GridObjectId Id { get; }
        public Transform Transform { get; }
        public SpriteRenderer Renderer { get; }

        public UnitView(GridObjectId id, Transform transform, SpriteRenderer renderer)
        {
            Id = id;
            Transform = transform;
            Renderer = renderer;
        }
    }
}
