using System;
using System.Collections;
using UnityEngine;

namespace Sources.Architecture
{
    public class TurnPasser : MonoBehaviour
    {
        public event Action TickPassed;
        private float _speed;
        private Coroutine _routine;

        public void Init(float speed)
        {
            _speed = speed;
        }

        public void Play()
        {
            _routine = StartCoroutine(Tick());
        }
        
        public void Pause()
        {
            if (_routine != null)
            {
                StopCoroutine(_routine);
                _routine = null;
            }
        }

        public void IncreaseSpeed(float value)
        {
            _speed += value;
        }

        private IEnumerator Tick()
        {
            var passed = 0f;
            while (true)
            {
                passed += _speed * Time.deltaTime;
                if (passed >= 1f)
                {
                    TickPassed?.Invoke();
                    passed = 0f;
                }

                yield return null;
            }
        }
    }
}