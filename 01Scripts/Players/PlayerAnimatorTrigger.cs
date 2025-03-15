using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerAnimatorTrigger : EntityAnimatorTrigger
    {
        private Player _player;
        
        public override void Initialize(Entity entity)
        {
            _player = entity as Player;
        }

        public void PlayBlade()
        {
            _player.PlayBladeEffect();
        }
    }
}
