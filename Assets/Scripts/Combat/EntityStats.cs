using UnityEngine;

namespace Combat
{
    /// <summary>
    /// Component that holds stats for an entity
    /// Also contains methods to interact with the stats
    /// </summary>
    public class EntityStats : MonoBehaviour
    {
        [SerializeField] private int health;
        public int Health => health;
        
        [SerializeField] private int defence;
        public int Defence => defence;

        private const float DmgReductionPerDef = 0.05f;
        
        public void Hurt(int amount)
        {
            health -= Mathf.RoundToInt(amount * (1 - defence * DmgReductionPerDef));
        }

        public void Heal(int amount)
        {
            health += amount;
        }
    }
}