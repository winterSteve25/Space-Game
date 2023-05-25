using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace WFC
{
    [Serializable]
    public class PieceInstance
    {
        private Piece _piece;
        private Transform _gameObject;
        private Dictionary<Direction, PieceInstance> _neighbors;

        public PieceInstance(Piece piece, Transform gameObject)
        {
            _piece = piece;
            _gameObject = gameObject;
            _neighbors = new Dictionary<Direction, PieceInstance>();
        }

        public bool Generate(Direction direction, bool overwrite = false)
        {
            if (_neighbors.TryGetValue(direction, out var neighbor))
            {
                // if overwrite is true we destroy the existing piece at that direction
                if (!overwrite) return false;
                Object.Destroy(neighbor._gameObject);
            }

            // if no possible pieces are found in that direction
            if (!_piece.PossibleConnections.TryGetValue(direction, out var bank))
                return false;

            var piece = bank[Random.Range(0, bank.Count)];
            var gameObject = Object.Instantiate(
                piece.Prefab,
                _gameObject.position + direction.GetAxis(piece.PrefabSize) * 0.5f,
                Quaternion.identity
            );
            var pieceInstance = new PieceInstance(piece, gameObject.transform);
            _neighbors.Add(direction, pieceInstance);
            
            return true;
        }
    }
}