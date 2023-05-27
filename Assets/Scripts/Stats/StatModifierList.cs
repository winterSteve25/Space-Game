using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace Stats
{
    [Serializable]
    public class StatModifierList<TType> : IEnumerable<StatModifier<TType>> where TType : Enum
    {
        [SerializeField]
        [ValueDropdown("CustomAddStatsButton", IsUniqueList = true, DrawDropdownForListElements = false, DropdownTitle = "Modify Stats")]
        [ListDrawerSettings(DraggableItems = false, ShowFoldout = true)]
        private List<StatModifier<TType>> stats;

        public int Count => stats.Count;

        public IEnumerator<StatModifier<TType>> GetEnumerator()
        {
            return stats.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable) stats).GetEnumerator();
        }

#if UNITY_EDITOR
        // Finds all available stat-types and excludes the types that the statList already contains, so we don't get multiple entries of the same type.
        private IEnumerable CustomAddStatsButton()
        {
            return Enum.GetValues(typeof(TType)).Cast<TType>()
                .Except(stats.Select(x => x.Type))
                .Select(x => new StatModifier<TType>(x))
                .AppendWith(stats)
                .Select(x => new ValueDropdownItem(x.Type.ToString(), x));
        }
#endif
    }

#if UNITY_EDITOR

    internal class StatModifierListValueDrawer<TType> : OdinValueDrawer<StatModifierList<TType>> where TType : Enum
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            // This would be the "private List<Stat<TType>> stats" field.
            this.Property.Children[0].Draw(label);
        }
    }
}
#endif