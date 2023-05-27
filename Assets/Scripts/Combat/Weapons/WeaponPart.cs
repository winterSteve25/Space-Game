using Stats;
using UnityEngine;

namespace Combat.Weapons
{
    [CreateAssetMenu(menuName = "Space Game/Weapons/New Weapon Part")]
    public class WeaponPart : ScriptableObject
    {
        [SerializeField] private StatModifierList<WeaponStat> modifiers;
        public StatModifierList<WeaponStat> Modifiers => modifiers;
    }
}