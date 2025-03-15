using UnityEngine;

namespace LJS
{
    public class DamageCaster : MonoBehaviour
    {
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private Vector2 _size;
        [SerializeField] private LayerMask _whatIsTarget;
        
        public void CastDamage(int damage)
        {
            Vector2 pos = transform.position + (Vector3)_offSet;

            Collider2D[] result
                = Physics2D.OverlapBoxAll(pos, _size, 0, _whatIsTarget);

            if (result.Length > 0)
            {
                foreach (var col in result)
                {
                    // 체력 감소 및 넉백 효과 적용 나중에 이펙트도 부여해야됨
                }
            }
        }
        
        #if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position + (Vector3)_offSet,
                _size);
        }
        
        #endif
    }
}
