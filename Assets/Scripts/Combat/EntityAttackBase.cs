using Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(EntityStats))]
    public class EntityAttackBase : MonoBehaviour
    {
        [SerializeField, Required] protected Weapon weapon;
        
        private EntityStats _selfStats;

        private void Start()
        {
            _selfStats = GetComponent<EntityStats>();
        }

        protected void Attack(EntityStats entityStats)
        {
            entityStats.Hurt(weapon.Damage);
        }
    }
}