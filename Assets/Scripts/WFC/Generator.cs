using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace WFC
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private List<Piece> pieceBank;

        private Random _random;

        private void Start()
        {
            _random = new Random(UnityEngine.Random.Range(0, int.MaxValue));
        }

        public void GeneratePiece(Vector3 from, Direction direction)
        {
        }
    }
}