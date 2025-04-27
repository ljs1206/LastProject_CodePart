using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillTable", menuName = "SO/Skill/Table")]
public class 
    PlayerSkillTable : ScriptableObject
{
    public List<Skill> SkillList { get; private set; } = new();
}
