using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat.Weapons
{
    public class MeleeWeapon : Weapon
    {
        protected override async UniTask AttackInternal(Vector3 origin, Vector3 direction)
        {
        }

        public override void Aim()
        {
        }
    }
}