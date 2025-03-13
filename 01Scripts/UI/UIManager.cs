using System;
using LJS.EventSystem;
using LJS.Players;
using UnityEngine;

namespace LJS.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputSO _playerInput;
        [SerializeField] private GameEventChannelSO _uiEventChannel;

        private void Awake()
        {
            _playerInput.OpenMenuEvent += HandleOpenMenu;
        }

        private void OnDestroy()
        {
            _playerInput.OpenMenuEvent -= HandleOpenMenu;
        }

        private void HandleOpenMenu()
        {
            _uiEventChannel.RaiseEvent(UIEvents.OpenMenu);
        }
    }
}
