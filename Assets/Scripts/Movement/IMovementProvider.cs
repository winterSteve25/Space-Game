using UnityEngine;

namespace Movement
{
    public interface IMovementProvider
    {
        Vector3 Direction { get; }
        float Speed { get; }
        Vector3 Rotation { get; }
        
        bool Jump { get; }
        float JumpForce { get; }
        float AirSpeedMultiplier { get; }
    }
}