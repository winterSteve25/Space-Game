using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace WFC
{
    [CreateAssetMenu(menuName = "Space Game/ProcGen/New Piece", fileName = "New Piece")]
    public class Piece : SerializedScriptableObject
    {
        [OdinSerialize] public Dictionary<Direction, List<Piece>> PossibleConnections;
        
        [SerializeField] private GameObject prefab;
        [SerializeField] private Vector3 prefabSize;

        public GameObject Prefab => prefab;
        public Vector3 PrefabSize => prefabSize;
    }
}