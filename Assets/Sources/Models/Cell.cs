using System;
using Sources.Architecture.Interfaces;
using UnityEngine;

namespace Sources.Models
{
    public class Cell : ICell
    {
        public event Action EntityChanged;
        public Vector2 Position { get; }

        public IEntity Entity
        {
            get => _entity;
            private set
            {
                _entity = value;
                EntityChanged?.Invoke();
            }
        }

        private IEntity _entity;


        public Cell(Vector2 position)
        {
            Position = position;
        }

        public bool TryToPutNewEntity(IEntity newEntity)
        {
            if (Entity == null)
            {
                Entity = newEntity;
                Entity.Removed += RemoveEntity;
                return true;
            }

            return false;
        }

        private void RemoveEntity()
        {
            Entity.Removed -= RemoveEntity;
            Entity = null;
        }
    }
}