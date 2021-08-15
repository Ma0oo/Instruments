using Plugins.Sound.Pitch;
using Plugins.Sound.Volume;
using UnityEngine;
using UnityEngine.Audio;

namespace Plugins.Sound.Sound2DLoops
{
    [CreateAssetMenu]
    public class SoundLoop2DSO : ScriptableObject, ISound2DLoop
    {
        public AudioClip Clip => _clip;
        public int Priority => _priority;
        public IVolume Volume => _volume;
        public IPitch Pitch => _pitch;
        public AudioMixerGroup Group => _group;
        public string Id => _id;

        [SerializeField] private AudioClip _clip;
        [SerializeField] private int _priority;
        [SerializeField] private string _id;
        [SerializeField] private VolumeSo _volume;
        [SerializeField] private PitchSo _pitch;
        [SerializeField] private AudioMixerGroup _group;
    }
}