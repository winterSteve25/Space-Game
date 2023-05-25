using UnityEngine;

namespace Combat.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponStats weaponStats;
        public WeaponStats WeaponStats => weaponStats;
    }
}