using System;
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
        private float _timeSinceLastAttack;

        private void Start()
        {
            _selfStats = GetComponent<EntityStats>();
        }

        protected virtual void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        protected void Attack(EntityStats entityStats)
        {
            if (!(_timeSinceLastAttack > weapon.WeaponStats.FireRate)) return;
            entityStats.Hurt(weapon.WeaponStats.Damage);
            _timeSinceLastAttack = 0;
        }
    }
}