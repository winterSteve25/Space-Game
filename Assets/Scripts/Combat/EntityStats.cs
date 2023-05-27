using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Component that holds stats for an entity
    /// Also contains methods to interact with the stats
    /// </summary>
    public class EntityStats : MonoBehaviour, IDamageable
    {
        [SerializeField] private float health;
        public float Health => health;
        
        [SerializeField] private float defence;
        public float Defence => defence; // will mitigate all damage when reaching 2000

        private const float DmgReductionPerDef = 0.0005f; // 0.05%
        
        public void Damage(float amount)
        {
            health -= amount * Mathf.Max(0, 1 - defence * DmgReductionPerDef);
        }

        public void Heal(float amount)
        {
            health += amount;
        }
    }
}