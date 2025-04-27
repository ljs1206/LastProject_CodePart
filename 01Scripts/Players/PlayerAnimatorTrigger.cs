using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerAnimatorTrigger : EntityAnimatorTrigger
    {
        private Player _player;
        
        public override void Initialize(Entity entity)
        {
            base.Initialize(entity);
            _player = entity as Player;
        }

        public void PlayBlade() 
            => _player.PlayBladeEffect();
        protected override void AnimationEnd()
            => base.AnimationEnd();
        protected override void AttackTriggerStart()
            => base.AttackTriggerStart();
        protected override void AttackTriggerEnd()
            => base.AttackTriggerEnd();

        // public void EndSkill()
        //     => _player.GetCompo<PlayerSkill>().CurrentUseSkill.OnSkillFinished;
    }
}
