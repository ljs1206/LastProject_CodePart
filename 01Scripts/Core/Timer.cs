using System;
using System.Collections.Generic;
using UnityEngine;

namespace LJS.Core
{
    public struct TimerObj
    {
        public TimerObj(bool paused, float endTime, Action callback)
        {
            if (callback == null) 
                throw new InvalidOperationException("Callback is null, you ");
            
            _isPaused = paused;
            _startTime = Time.time;
            _endTime = endTime;
            _callback = callback;
            _currentTime = 0;
        }

        public bool _isPaused;
        private float _startTime;
        private float _endTime;
        private Action _callback;
        private float _currentTime;

        public void Update()
        {
            if(!_isPaused) return;
            _currentTime = Time.time;
            if (_currentTime - _startTime > _endTime)
            {
                _callback?.Invoke();
                Timer.Instance.Remove(this);
            }
        }

    }
    
    public class Timer : MonoSingleton<Timer>
    {
        // todo : change this to dict
        private List<TimerObj> TimerList = new(16);

        public void Remove(TimerObj timer)
        {
            TimerList.Remove(timer);
        }
        
        private void Update()
        {
            foreach (var timerObj in TimerList)
            {
                timerObj.Update();
            }
        }

        public void DelayCallBack(Action callback, float delayTime)
        {
            TimerObj timer =
                new TimerObj(false, delayTime, callback);
            TimerList.Add(timer);
        }
    }
}
