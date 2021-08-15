using System;
using UnityEngine;

namespace Plugins.PhysicShell
{
    public class ColliderShell : MonoBehaviour
    {
        [SerializeField] private Collider _collider2D; 
        
        public event Action<Collision> Enter;
        public event Action<Collision> Stay;
        public event Action<Collision> Exit;

        private void OnEnable() => _collider2D.enabled = true;

        private void OnDisable() => _collider2D.enabled = false;

        private void OnCollisionEnter(Collision other) => Enter?.Invoke(other);

        private void OnCollisionStay(Collision other) => Stay?.Invoke(other);

        private void OnCollisionExit(Collision other) => Exit?.Invoke(other);
    }
}