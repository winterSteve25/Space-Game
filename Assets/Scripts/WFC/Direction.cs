using System;
using UnityEngine;

namespace WFC
{
    public enum Direction
    {
        North,
        South,
        East,
        West,
        Up,
        Down
    }

    public static class DirectionExtension
    {
        public static Vector3 GetVector3(this Direction direction)
        {
            return direction switch
            {
                Direction.North => Vector3.forward,
                Direction.South => Vector3.right,
                Direction.East => Vector3.right,
                Direction.West => Vector3.left,
                Direction.Up => Vector3.up,
                Direction.Down => Vector3.down,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        }

        public static Vector3 GetAxis(this Direction direction, Vector3 vector3)
        {
            return direction switch
            {
                Direction.North or Direction.South => new Vector3(0, 0, vector3.z),
                Direction.West or Direction.East => new Vector3(vector3.x, 0, 0),
                Direction.Down or Direction.Up => new Vector3(0, vector3.y, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
        } 
    }
}