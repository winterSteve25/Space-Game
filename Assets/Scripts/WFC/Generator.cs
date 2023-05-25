using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace WFC
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private List<Piece> pieceBank;

        private Random _random;
        private Dictionary<Vector3, Piece> _map;

        private void Start()
        {
            _random = new Random(UnityEngine.Random.Range(0, int.MaxValue));
            _map = new Dictionary<Vector3, Piece>();
        }

        public void GeneratePiece(Vector3 from, Direction direction)
        {
            var bank = pieceBank;
            
            if (_map.TryGetValue(from, out var value))
            {
                bank = value.PossibleConnections[direction];
            }

            var piece = bank[_random.Next(0, bank.Count)];
        }
    }
}