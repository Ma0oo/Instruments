using Plugins.Sound.Pitch;
using Plugins.Sound.Volume;
using UnityEngine;
using UnityEngine.Audio;

namespace Plugins.Sound
{
    public interface ISound2DBase
    {
        public AudioClip Clip { get; }
        public int Priority { get; }
        public IVolume Volume { get; }
        public IPitch Pitch { get; }
        public AudioMixerGroup Group { get; }
    }
}