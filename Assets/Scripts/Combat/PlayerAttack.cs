using UnityEngine;

namespace Combat
{
    public class PlayerAttack : EntityAttackBase
    {
        [SerializeField] private Transform playerCam;
        [SerializeField] private Transform test;

        private void Update()
        {
            if (!Input.GetMouseButton(0)) return;
            if (!Physics.Raycast(playerCam.position, playerCam.forward, out var hitInfo, Mathf.Infinity)) return;
            test.position = hitInfo.point;
            if (hitInfo.collider.TryGetComponent<EntityStats>(out var enemy))
            {
                Attack(enemy);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(playerCam.position, playerCam.forward);
        }
    }
}