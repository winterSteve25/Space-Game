using Combat;
using Combat.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerCombat player;
        [SerializeField] private TMP_Text totalAmmoCount;
        [SerializeField] private TMP_Text loadedAmmoCount;
        
        private Weapon _weapon;

        private void OnEnable()
        {
            player.WeaponChanged += SetWeapon;
        }

        private void OnDisable()
        {
            player.WeaponChanged -= SetWeapon;
        }

        private void SetWeapon(Weapon weapon)
        {
            // unsubscribe to the previous one
            if (_weapon != null)
            {
                _weapon.TotalAmmoCountChanged -= UpdateTotalAmmoCount;
                _weapon.LoadedAmmoCountChanged -= UpdateLoadedAmmoCount;
            }
            
            _weapon = weapon;
           
            // subscribe to the new one
            if (_weapon == null) return;
            _weapon.TotalAmmoCountChanged += UpdateTotalAmmoCount;
            _weapon.LoadedAmmoCountChanged += UpdateLoadedAmmoCount;
        }

        private void UpdateTotalAmmoCount(string amount)
        {
            totalAmmoCount.text = amount;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) totalAmmoCount.transform.parent);
        }
       
        private void UpdateLoadedAmmoCount(string amount)
        {
            loadedAmmoCount.text = amount;
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) totalAmmoCount.transform.parent);
        }
    }
}