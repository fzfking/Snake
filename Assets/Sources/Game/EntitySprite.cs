using System;
using Sources.Architecture;
using Sources.Architecture.Enums;
using UnityEngine;

namespace Sources.Game
{
    [Serializable]
    public class EntitySprite
    {
        public EntityType EntityType;
        public Sprite Sprite;
    }
}