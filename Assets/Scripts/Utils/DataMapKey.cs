using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct DataMapKey<T>
    {
        [SerializeField, ReadOnly] private string key;

        public string Key => key;

        public DataMapKey(string key)
        {
            this.key = key;
        }
    }
}