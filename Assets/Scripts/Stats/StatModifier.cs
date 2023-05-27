using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct StatModifier<TType> where TType : Enum
    {
        [SerializeField]
        [HideInInspector]
        private TType type;

        [SerializeField] 
        [LabelWidth(80)] 
        private ModifierType modifierType;
        
        [SerializeField]
        [LabelWidth(80), LabelText("$type")] 
        private float value;

        public TType Type => type;

        public StatModifier(TType type) : this()
        {
            this.type = type;
        }

        public float Modify(float originalValue)
        {
            return modifierType switch
            {
                ModifierType.Additive => originalValue + value,
                ModifierType.Multiplicative => originalValue * value,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public enum ModifierType
    {
        Additive,
        Multiplicative,
    }
}