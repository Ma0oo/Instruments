using System.Collections;

namespace Plugins.Interfaces
{
    public interface ICoroutineRunner
    {
        ICurtain StartCoroutine(IEnumerator curtaint);
    }
}