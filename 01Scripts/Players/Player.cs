using System;
using System.Collections.Generic;
using LJS.Core.StatSystem;
using LJS.Entities;
using LJS.FSM;
using UnityEngine;

namespace LJS.Players
{
    public class Player : Entity
    {
        [field:SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        [Header("Stats")]
        public StatSO JumpPowerStat;
        
        public List<StateSO> states;

        private StateMachine _stateMachine;

        protected override void AfterInitialize()
        {
            base.AfterInitialize();
            _stateMachine = new StateMachine(states, this);

            GetStatsFromComponent();
        }

        private void GetStatsFromComponent()
        {
            EntityStat statCompo = GetCompo<EntityStat>();
        }

        private void Start()
        {
            _stateMachine.Initialize("IDLE"); //IDLE상태로 시작
        }

        private void Update()
        {
            _stateMachine.UpdateFSM();
        }

        public void ChangeState(string newStateName)
        {
            _stateMachine.ChangeState(newStateName);
        }

        public void PlayBladeEffect()
        {
            GetCompo<PlayerEffect>().
                PlayBladeEffect(GetCompo<PlayerAttack>().CurrentComboCount);
        }
        
    }
}
