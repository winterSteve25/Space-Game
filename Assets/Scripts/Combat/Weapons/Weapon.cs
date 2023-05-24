using UnityEngine;

namespace Combat.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private int damage;
        public int Damage => damage;

        [SerializeField] private float fireRate;
        public float FireRate => fireRate;
    }
}