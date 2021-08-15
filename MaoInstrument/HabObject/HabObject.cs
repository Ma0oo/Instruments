using System;
using Plugins.HabObject.CommunicatorParts;
using Plugins.HabObject.GeneralProperty;
using UnityEngine;

namespace Plugins.HabObject
{
    public abstract class HabObject : MonoBehaviour
    {
        public abstract Communicator Communicator { get; }
        public abstract ComponentShell ComponentShell { get; }
        public abstract MainDates MainDates { get; }

        private void OnDestroy() => Communicator.Clear();
        
    }
}