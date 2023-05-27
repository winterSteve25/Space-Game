using System;
using Combat.Weapons;
using UnityEngine;

namespace Combat
{
    public class PlayerCombat : EntityCombat
    {
        [SerializeField] private Transform playerCam;

        public event Action<Weapon> WeaponChanged;

        protected override void Start()
        {
            base.Start();
            WeaponChanged?.Invoke(weapon);
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                weapon.Aim();
            }

            if (!Input.GetMouseButton(0)) return;
            weapon.Attack(playerCam.position, playerCam.forward);
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(playerCam.position, playerCam.forward);
        }
    }
}