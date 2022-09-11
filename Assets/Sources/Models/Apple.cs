using System;
using Sources.Architecture;
using Sources.Architecture.Enums;
using Sources.Architecture.Interfaces;

namespace Sources.Models
{
    public class Apple: IEntity
    {
        public EntityType Type => EntityType.Apple;
        public event Action Removed;
        public void Remove()
        {
            Removed?.Invoke();
        }
    }
}