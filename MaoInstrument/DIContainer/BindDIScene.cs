using System.Collections.Generic;
using UnityEngine;

namespace Plugins.DIContainer
{
    public class BindDIScene : MonoBehaviour
    {
        [SerializeField] private  List<ObjectToDi> _objects = new List<ObjectToDi>();
        [SerializeField] private FactoryDI[] _manualBind;
        
        private void Awake()
        {
            foreach (var manual in _manualBind) manual.Create(DiBox.MainBox);
            foreach (var obj in _objects) DiBox.MainBox.RegisterSingleType(obj.Instance, obj.id);
        }

        private void OnDestroy()
        {
            foreach (var manual in _manualBind) manual.DestroyDi(DiBox.MainBox);
            foreach (var obj in _objects)
                if (obj.IsUnbind)
                    DiBox.MainBox.RemoveSingelType(obj.Instance.GetType(), obj.id);
        }

        [System.Serializable]
        private class ObjectToDi
        {
            public bool IsUnbind= true;
            public string id = "";
            public Component Instance;
        }
    }
}