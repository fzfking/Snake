using System;

namespace Sources.Architecture.Interfaces
{
    public interface IEntity
    {
        EntityType Type { get; }
        event Action Removed;
        void Remove();
    }
}