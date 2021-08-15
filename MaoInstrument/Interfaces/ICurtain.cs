using System;

namespace Plugins.Interfaces
{
    public interface ICurtain
    {
        public abstract void Fade(Action callback);
        public abstract void Unfade();
        public abstract void Transit(Action callback);
    }
}