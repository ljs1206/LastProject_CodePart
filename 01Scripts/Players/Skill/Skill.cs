using System;
using LJS.Animators;
using LJS.Entities;
using LJS.Players;
using UnityEngine;

public abstract class Skill : MonoBehaviour
{
    [field:SerializeField] public string Name { get; private set; }
    [field:SerializeField] public AnimParamSO SkillAnimParam {get; private set;}
    
    public event Action OnSkillFinished;
    public event Action OnSkillStarted;

    protected Player _player;
    
    public virtual void InitSkill(Player player)
    {
        _player = player;
    }

    public virtual void UseSkill()
    {
        OnSkillFinished?.Invoke();
    }

    public virtual void StopSkill()
    {
        OnSkillStarted?.Invoke();   
    }
}
