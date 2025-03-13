using System;
using DG.Tweening;
using LJS.EventSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LJS.UI
{
    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private GameEventChannelSO _systemChannel;
        [SerializeField] private Image _fadeImage;
        
        private readonly int _valueHash = Shader.PropertyToID("_Value");

        private void Awake()
        {
            _fadeImage.material = new Material(_fadeImage.material);
            _systemChannel.AddListener<FadeScreenEvent>(HandleFadeScreen);
        }

        private void OnDestroy()
        {
            _systemChannel.RemoveListener<FadeScreenEvent>(HandleFadeScreen);
        }

        private void HandleFadeScreen(FadeScreenEvent evt)
        {
            float fadeValue = evt.isFadeIn ? 2.5f : 0f;
            float startValue = evt.isFadeIn ? 0f : 2.5f;
            
            _fadeImage.material.SetFloat(_valueHash, startValue);

            if (evt.isFadeIn) //새로운 씬으로 들어오는 경우
            {
                LoadGameEvent loadEvt = SystemEvents.LoadGameEvent;
                loadEvt.isLoadFromFile = false;
                _systemChannel.RaiseEvent(loadEvt);
            }

            _fadeImage.material.DOFloat(fadeValue, _valueHash, 0.8f).OnComplete(() =>
            {
                _systemChannel.RaiseEvent(SystemEvents.FadeComplete);
            });
        }
    }
}
