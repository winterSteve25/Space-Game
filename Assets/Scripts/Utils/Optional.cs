using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Utils
{
    [Serializable]
    public struct Optional<T>
    {
        [SerializeField, HorizontalGroup("Main", Width = 20), HideLabel]
        private bool exists;
        
        [SerializeField, HorizontalGroup("Main"), LabelWidth(40), EnableIf("@exists")] 
        private T value;

        public Optional(T value)
        {
            this.value = value;
            exists = value != null;
        }

        /// <summary>
        /// Returns the value regardless if its valid or not
        /// </summary>
        /// <returns>The raw value</returns>
        public T Unwrap()
        {
            return value;
        }
        
        /// <summary>
        /// Runs a callback on the value if it exists
        /// </summary>
        /// <param name="run">The callback, basically a Consumer in java</param>
        public void RunIfExists(Action<T> run)
        {
            if (exists) run(value);
        }
        
        /// <summary>
        /// Maps the value to another if it exists
        /// </summary>
        /// <param name="mapper">Mapping function a Function in java</param>
        /// <typeparam name="TR">Type to map to</typeparam>
        /// <returns>The mapped result</returns>
        public Optional<TR> Map<TR>(Func<T, TR> mapper)
        {
            return exists ? new Optional<TR>(mapper(value)) : Optional<TR>.Empty();
        }

        /// <summary>
        /// Whether or not the value is valid
        /// </summary>
        /// <returns></returns>
        public bool Exists() => exists;
        
        /// <summary>
        /// Creates an empty optional
        /// </summary>
        /// <returns></returns>
        public static Optional<T> Empty()
        {
            return new Optional<T>
            {
                exists = false
            };
        }
    }
}