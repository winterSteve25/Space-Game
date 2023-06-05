using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Combat.Weapons
{
    public class RangedWeapon : Weapon
    {
        [SerializeField] private int totalAmmoCount;
        [SerializeField] private int loadedAmmoCount;

        private int _magazineSize;
        private float _damage;
        private int _reloadSpeed;

        protected override void Start()
        {
            base.Start();
            _magazineSize = Mathf.RoundToInt(Stats[WeaponStat.MagazineSize].OrElse(30));
            _damage = Stats[WeaponStat.Damage].OrElse(0);
            // from seconds to milliseconds
            _reloadSpeed = Mathf.RoundToInt(Stats[WeaponStat.ReloadSpeed].OrElse(0) * 1000);
            OnTotalAmmoCountChanged(totalAmmoCount.ToString());
            OnLoadedAmmoCountChanged(loadedAmmoCount.ToString());
        }

        protected override async UniTask AttackInternal(Vector3 origin, Vector3 direction)
        {
            if (loadedAmmoCount <= 0)
            {
                // no loaded ammo and have no ammo to reload
                if (totalAmmoCount <= 0) return;
                await Reload();
            }

            loadedAmmoCount--;
            OnLoadedAmmoCountChanged(loadedAmmoCount.ToString());
            
            if (!Physics.Raycast(origin, direction, out var hitInfo, Mathf.Infinity)) return;
            if (hitInfo.collider.TryGetComponent<IDamageable>(out var damageable))
            {
                damageable.Damage(_damage);
            }
            
            
            if (loadedAmmoCount <= 0)
            {
                await Reload();
            }
        }

        public override void Aim()
        {
        }

        private async UniTask Reload()
        {
            // if we have less than the amount needed to fill a magazine
            if (totalAmmoCount - _magazineSize < 0)
            {
                loadedAmmoCount = totalAmmoCount;
                totalAmmoCount = 0;
            }
            else
            {
                loadedAmmoCount = _magazineSize;
                totalAmmoCount -= _magazineSize;
            }

            await UniTask.Delay(_reloadSpeed);
            OnTotalAmmoCountChanged(totalAmmoCount.ToString());
            OnLoadedAmmoCountChanged(loadedAmmoCount.ToString());
        }
    }
}