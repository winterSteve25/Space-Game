using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using Stats;
using UnityEngine;
using Utils;

namespace Combat.Weapons
{
    /// <summary>
    /// Base Weapon class, holds stat and accounts for usage rate
    /// </summary>
    public abstract class Weapon : MonoBehaviour
    {
        // this is mostly for debugging, gonna be creating these
        [SerializeField, HideLabel, BoxGroup("Stats")] private Optional<StatList<WeaponStat>> stats;
        [SerializeField] protected List<WeaponPart> parts;

        // weapon name maybe we can procedurally generate this later
        [SerializeField] protected string weaponName;
        public string WeaponName => weaponName;

        protected StatList<WeaponStat> Stats;
        private float _timeSinceLastAttack;
        private Optional<UniTask> _attackSequence;
        
        public event Action<string> TotalAmmoCountChanged; 
        public event Action<string> LoadedAmmoCountChanged; 

        protected virtual void Start()
        {
            Stats = stats.OrElse(new StatList<WeaponStat>());
            foreach (var part in parts)
            {
                Stats.Modify(part.Modifiers);
            }
        }

        protected virtual void Update()
        {
            _timeSinceLastAttack += Time.deltaTime;
        }

        /// <summary>
        /// Attacks at given direction from origin
        /// </summary>
        /// <param name="origin">Attack from</param>
        /// <param name="direction">Attack direction</param>
        public void Attack(Vector3 origin, Vector3 direction)
        {
            if (_attackSequence.Map(task => task.Status != UniTaskStatus.Succeeded).OrElse(false)) return;
            if (Stats[WeaponStat.FireRate]
                .Map(fireRate => _timeSinceLastAttack <= fireRate)
                .OrElse(false)) return;

            _attackSequence = new Optional<UniTask>(AttackInternal(origin, direction));
            _timeSinceLastAttack = 0;
        }

        /// <summary>
        /// The actual attack logic of this weapon
        /// </summary>
        /// <param name="origin">Attack from</param>
        /// <param name="direction">Attack direction</param>
        protected abstract UniTask AttackInternal(Vector3 origin, Vector3 direction);

        /// <summary>
        /// Aims the weapon if it is aimable
        /// </summary>
        public abstract void Aim();

        /// <summary>
        /// Triggers the TotalAmmoCountChanged event, this is listened to by UI element right now to update ammo count UI
        /// </summary>
        /// <param name="value">The new total ammo count</param>
        protected void OnTotalAmmoCountChanged(string value)
        {
            TotalAmmoCountChanged?.Invoke(value);
        }

        /// <summary>
        /// Triggers the LoadedAmmoCountChanged event, this is listened to by UI element right now to update ammo count UI
        /// </summary>
        /// <param name="value">The new loaded ammo count</param>
        protected void OnLoadedAmmoCountChanged(string value)
        {
            LoadedAmmoCountChanged?.Invoke(value);
        }
    }
}