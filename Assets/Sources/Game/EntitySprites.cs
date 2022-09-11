using System;
using System.Linq;
using Sources.Architecture;
using Sources.Architecture.Enums;
using UnityEngine;

namespace Sources.Game
{
    public class EntitySprites : MonoBehaviour
    {
        [SerializeField] 
        private EntitySprite[] Values;

        public Sprite GetSpriteByType(EntityType type)
        {
            var sprite = Values.FirstOrDefault(e => e.EntityType == type)?.Sprite;
            if (sprite != null)
            {
                return sprite;
            }

            throw new Exception($"Entity {type} doesn't have associated sprite.");
        }
    }
}