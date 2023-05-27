using Combat.Weapons;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Combat
{
    [RequireComponent(typeof(EntityStats))]
    public class EntityCombat : MonoBehaviour
    {
        [SerializeField, Required] protected Weapon weapon;
        
        private EntityStats _selfStats;

        protected virtual void Start()
        {
            _selfStats = GetComponent<EntityStats>();
        }
    }
}