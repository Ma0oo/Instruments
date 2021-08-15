using System;
using UnityEngine;

namespace Plugins.PhysicShell
{
    public class TriggerShell : MonoBehaviour
    {
        [SerializeField] private Collider _collider2D; 
        
        public event Action<Collider> Enter;
        public event Action<Collider> Stay;
        public event Action<Collider> Exit;

        private void OnEnable() => _collider2D.enabled = true;

        private void OnDisable() => _collider2D.enabled = false;

        private void OnTriggerEnter(Collider other) => Enter?.Invoke(other);

        private void OnTriggerStay(Collider other) => Stay?.Invoke(other);

        private void OnTriggerExit(Collider other) => Exit?.Invoke(other);
    }
}