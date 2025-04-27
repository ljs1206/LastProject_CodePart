using System;
using System.Collections.Generic;
using LJS.Combat;
using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerAttack : EntityAttack
    {
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void AfterInit()
        {
            base.AfterInit();
        }

        protected override void HandleAttackTrigger(string animationName)
        {
            throw new NotImplementedException();
        }
    }
}
