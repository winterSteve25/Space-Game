using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Utils;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Stats
{
    [Serializable]
    public class StatList<TType> where TType : Enum
    {
        [SerializeField]
        [ValueDropdown("CustomAddStatsButton", IsUniqueList = true, DrawDropdownForListElements = false, DropdownTitle = "Modify Stats")]
        [ListDrawerSettings(DraggableItems = false, ShowFoldout = true)]
        private List<StatValue<TType>> stats;

        public StatList()
        {
            stats = new List<StatValue<TType>>();
        }

        public int Count => stats.Count;
        public Optional<float> this[TType type]
        {
            get
            {
                foreach (var stat in stats.Where(stat => stat.Type.Equals(type)))
                {
                    return new Optional<float>(stat.Value);
                }

                return Optional<float>.Empty();
            }
        }

        /// <summary>
        /// Modifies existing stats based on provided stats. Provided stats are added to current stats
        /// </summary>
        /// <param name="modifiers"></param>
        public void Modify(StatModifierList<TType> modifiers)
        {
            foreach (var mod in modifiers)
            {
                var index = stats.FindIndex(stat => stat.Type.Equals(mod.Type));
                if (index == -1)
                {
                    stats.Add(new StatValue<TType>(mod.Type, mod.Modify(0)));
                }
                else
                {
                    var stat = stats[index];
                    var modified = new StatValue<TType>(stat.Type, mod.Modify(stat.Value));
                    stats[index] = modified;
                }
            }
        }
        
#if UNITY_EDITOR
        // Finds all available stat-types and excludes the types that the statList already contains, so we don't get multiple entries of the same type.
        private IEnumerable CustomAddStatsButton()
        {
            return Enum.GetValues(typeof(TType)).Cast<TType>()
                .Except(stats.Select(x => x.Type))
                .Select(x => new StatValue<TType>(x, 0))
                .AppendWith(stats)
                .Select(x => new ValueDropdownItem(x.Type.ToString(), x));
        }
#endif
    }

#if UNITY_EDITOR

    internal class StatListValueDrawer<TType> : OdinValueDrawer<StatList<TType>> where TType : Enum
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // This would be the "private List<Stat<TType>> stats" field.
            this.Property.Children[0].Draw(label);
        }
    }
}
#endif