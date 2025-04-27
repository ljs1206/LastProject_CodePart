using LJS.Animators;
using LJS.Entities;
using LJS.FSM;
using LJS.Players;
using UnityEngine;

public class PlayerSkillState : EntityState
{
    private Player _player;
    public PlayerSkillState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
    {
        _player = entity as Player;
    }
    
    public override void Enter()
    {
        // _player.GetCompo<PlayerSkill>().UseSkill();
        _renderer.SetParam(_stateAnimParam, true);
        _isTriggered = false;
        _animTrigger.OnAnimationEnd += AnimationEndTrigger;
    }
    
    public override void Exit()
    {
        _renderer.SetParam(_stateAnimParam, false);
        _animTrigger.OnAnimationEnd -= AnimationEndTrigger;
    }

    public override void AnimationEndTrigger()
    {
        _isTriggered = true;
    }
}
