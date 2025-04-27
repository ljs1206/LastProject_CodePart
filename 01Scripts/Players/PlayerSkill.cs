using System.Collections.Generic;
using LJS.Entities;
using LJS.Players;
using UnityEngine;

public enum SkillType
{
    None = 0, DoubleSlash
}
public class PlayerSkill : MonoBehaviour, IEntityComponent
{
    [SerializeField] private PlayerSkillTable _skillTable;

    private Dictionary<string, Skill> _skillDict;
    private Player _player;
    
    private Skill _currentUseSkill = null;
    public Skill CurrentUseSkill => _currentUseSkill;
    
    public void Initialize(Entity entity)
    {
        _player = entity as Player;

        foreach (var skill in _skillTable.SkillList)
        {
            _skillDict.Add(skill.Name, skill);
            skill.InitSkill(_player);
        }
    }

    public void UseSkill(SkillType skillType)
    {
        Skill skill =  _skillDict[skillType.ToString()];
        _currentUseSkill = skill;
        _player.GetCompo<EntityRenderer>().SetParam(skill.SkillAnimParam, true);
        skill.UseSkill();
    }

    public void EndSkill()
    {
        _currentUseSkill.StopSkill();
        _currentUseSkill = null;
    }
}
