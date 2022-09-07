using System;
using Sources.Architecture;
using UnityEngine;

namespace Sources.Game
{
    public class Input : MonoBehaviour
    {
        public event Action SpacePressed;
        public event Action<Direction> DirectionChanged;

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                SpacePressed?.Invoke();
            }

            var horizontalAxis = UnityEngine.Input.GetAxis("Horizontal");
            if (horizontalAxis != 0)
            {
                if (horizontalAxis > 0)
                {
                    DirectionChanged?.Invoke(Direction.Right);
                }
                else
                {
                    DirectionChanged?.Invoke(Direction.Left);
                }
            }

            var verticalAxis = UnityEngine.Input.GetAxis("Vertical");
            if (verticalAxis != 0)
            {
                if (verticalAxis > 0)
                {
                    DirectionChanged?.Invoke(Direction.Up);
                }
                else
                {
                    DirectionChanged?.Invoke(Direction.Down);
                }
            }
            {
                
            }
        }
    }
}