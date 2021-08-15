﻿using System;
using System.Collections.Generic;
using Plugins.DIContainer;
using Plugins.GameStateMachines.States;
using Plugins.Interfaces;
using Plugins.Sound.Sound2DLoops;
using Plugins.Sound.Sound2Ds;
using Plugins.Sound.SupportPart;
using UnityEngine;

namespace Plugins.Sound
{
    public class SoundSystem
    {
        [DI()] private ICoroutineRunner _coroutineRunner;
        
        private AudioSourcePool _pool;
        private Dictionary<string, SourceLoopObserver> _poolLoopSource = new Dictionary<string, SourceLoopObserver>();

        public SoundSystem(AudioSource template, int startSize = 50)
        {
            _pool = new AudioSourcePool(startSize, new GameObject("Parent sources").transform, template);
        }
        
        public void TurnOffAll()
        {
            _pool.OffAll();
            _poolLoopSource = new Dictionary<string, SourceLoopObserver>();
        }

        public AudioSource Play(ISound2D sound)
        {
            if(sound.CountLoop<=0)
                throw new Exception("Wrong count loop");

            AudioSource source = _pool.GetFree();
            var observer = new AudioObserver(source, sound, _coroutineRunner);
            observer.End += OnEnd;
            observer.Start();

            return source;
        }

        public void Play(ISound2DLoop sound, LoopAction action,  out Transform sourceTransform)
        {
            sourceTransform = null;
            switch (action)
            {
                case LoopAction.Start:
                    if (!_poolLoopSource.ContainsKey(sound.Id))
                    {
                        var source = _pool.GetFree();
                        sourceTransform = source.transform;
                        _poolLoopSource.Add(sound.Id, new SourceLoopObserver(sound, source));
                        _poolLoopSource[sound.Id].Play();
                    }
                    break;
                case LoopAction.Stop:
                    if (_poolLoopSource.ContainsKey(sound.Id))
                    {
                        _pool.Return(_poolLoopSource[sound.Id].Stop());
                        _poolLoopSource.Remove(sound.Id);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(action), action, null);
            }
        }

        private void OnEnd(AudioSource source, AudioObserver observer)
        {
            _pool.Return(source);
            observer.End -= OnEnd;
        }
        
        public enum LoopAction
        {
            Start, Stop
        }
    }
}