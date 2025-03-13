using System;
using System.Collections.Generic;
using LJS.EventSystem;
using LJS.Inventories;
using LJS.Items;
using LJS.Players;
using UnityEngine;
using UnityEngine.Events;

namespace LJS.UI
{
    public class InventoryUI : MonoBehaviour
    {
        public UnityEvent<ItemDataSO> OnItemSelected;

        [SerializeField] private GameEventChannelSO _inventoryChannel;
        [SerializeField] private PlayerInputSO _playerInputSO;
        [SerializeField] private SlotSelectionUI _selectionUI;
        [SerializeField] private int _columnCount = 5;

        public RectTransform RectTrm => transform as RectTransform;

        public List<InventoryItem> inventoryData; //이게 MVP에서 Model이다.

        [SerializeField] private Transform _slotParent;
        private ItemSlotUI[] _itemSlots;

        private ItemSlotUI _selectedSlot;
        private int _selectedSlotIndex;

        private float _lastInputTime;

        private void Awake()
        {
            _itemSlots = _slotParent.GetComponentsInChildren<ItemSlotUI>();
        }
        
        public void Open()
        {
            _lastInputTime = Time.unscaledTime;
            _inventoryChannel.AddListener<InventoryDataList>(HandleDataRefresh);
            _playerInputSO.UINavigationEvent += HandleUINavigation;
            _inventoryChannel.RaiseEvent(InventoryEvents.RequestInventoryData);

            int openIndex = _selectedSlotIndex < _itemSlots.Length ? _selectedSlotIndex : 0;
            SelectItem(openIndex);
        }

        public void Close()
        {
            _inventoryChannel.RemoveListener<InventoryDataList>(HandleDataRefresh);
            _playerInputSO.UINavigationEvent -= HandleUINavigation;
        }


        private void HandleDataRefresh(InventoryDataList evt)
        {
            inventoryData = evt.items;
            UpdateSlotUI();
        }
        
        private void HandleUINavigation(Vector2 movement)
        {
            float keyDelayTime = 0.1f;
            if (_lastInputTime + keyDelayTime > Time.unscaledTime)
                return;
            
            _lastInputTime = Time.unscaledTime;
            int nextIndex = GetNextSelection(movement);
            if (nextIndex != _selectedSlotIndex)
                SelectItem(nextIndex);
        }
        
        private void SelectItem(int slotIndex)
        {
            _selectedSlotIndex = slotIndex;
            _selectedSlot = _itemSlots[slotIndex];
            MoveSelectionUI();
        }

        private void MoveSelectionUI()
        {
            Vector2 anchorPos = RectTrm.InverseTransformPoint(_selectedSlot.transform.position);
            _selectionUI.MoveAnchorPosition(anchorPos, true);
            //여기서 아이템이 선택되었음을 알려준다.
            
            OnItemSelected?.Invoke(_selectedSlot.item?.data);
        }

        private int GetNextSelection(Vector2 movement)
        {
            Debug.Log(movement);
            movement.y *= -1;
            Vector2Int currentPos = new Vector2Int(_selectedSlotIndex % _columnCount, _selectedSlotIndex / _columnCount);
            int totalRows = Mathf.CeilToInt((float)_itemSlots.Length / _columnCount); //최대 행의 수를 구해주고

            currentPos += Vector2Int.RoundToInt(movement);

            if (currentPos.x >= _columnCount || currentPos.x < 0 || currentPos.y < 0 || currentPos.y >= totalRows)
                return _selectedSlotIndex;

            return currentPos.x + currentPos.y * _columnCount;
        }
        
        private void UpdateSlotUI()
        {
            for(int i = 0; i < _itemSlots.Length; i++)
                _itemSlots[i].CleanUpSlot();
            
            for(int i = 0; i < inventoryData.Count; i++)
                _itemSlots[i].UpdateSlot(inventoryData[i]);
        }
    }
}
