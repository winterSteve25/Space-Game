using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public struct StatValue<TType> : IEquatable<StatValue<TType>> where TType : Enum
    {
        [SerializeField]
        [HideInInspector]
        private TType type;

        [SerializeField]
        [LabelWidth(80), LabelText("$type")] 
        private float value;

        public TType Type => type;
        public float Value => value;

        public StatValue(TType type, float value)
        {
            this.type = type;
            this.value = value;
        }

        public static bool operator ==(StatValue<TType> stat1, StatValue<TType> stat2)
        {
            return stat1.type.Equals(stat2.type) && Mathf.Approximately(stat1.value, stat2.value);
        }

        public static bool operator !=(StatValue<TType> stat1, StatValue<TType> stat2)
        {
            return !(stat1 == stat2);
        }

        public override bool Equals(object obj)
        {
            return obj is StatValue<TType> other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(type, value);
        }

        public bool Equals(StatValue<TType> other)
        {
            return EqualityComparer<TType>.Default.Equals(type, other.type) && value.Equals(other.value);
        }
    }
}