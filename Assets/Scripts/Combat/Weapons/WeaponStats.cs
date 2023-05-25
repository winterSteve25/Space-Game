using System;
using UnityEngine;

namespace Combat.Weapons
{
    [Serializable]
    public struct WeaponStats
    {
        [SerializeField]
        private int damage;
        public int Damage => damage;

        [SerializeField, Tooltip("The cooldown between each attack in Seconds")] 
        private float fireRate;
        public float FireRate => fireRate;

        public WeaponStats(int damage, float fireRate)
        {
            this.damage = damage;
            this.fireRate = fireRate;
        }
    }
}