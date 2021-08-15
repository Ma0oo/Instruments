using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Plugins.Customizable.Attributs;
using Plugins.Customizable.Editor.ViewAttribute;
using Plugins.HabObject.GeneralProperty;
using UnityEditor;
using UnityEngine;

namespace Plugins.Customizable.Editor
{
    public class EditorSettingHubObject : EditorWindow
    {
        private HabObject.HabObject _target;
        private List<ViewCustomizableComponent> _viewComponents;
        private Vector2 _scrollView;
        
        [MenuItem("HubObject/Setting object instance")]
        static void Init()
        {
            EditorSettingHubObject window = (EditorSettingHubObject)EditorWindow.GetWindow(typeof(EditorSettingHubObject));
            window.Show();
        }
        
        private void OnGUI()
        {
            _target = (HabObject.HabObject) EditorGUILayout.ObjectField("target", _target, typeof(HabObject.HabObject), true);
            if (GUILayout.Button("Update datas") && _target)
                UpdateData();
            if (GUILayout.Button("Save datas") && _target)
                SaveData();

            if(_viewComponents==null)
                return;
            _scrollView  = GUILayout.BeginScrollView(_scrollView, false, true, GUILayout.Height(base.position.height), GUILayout.Width(base.position.width));
            foreach (var componentView in _viewComponents)
                componentView.Render();
            GUILayout.EndScrollView();
        }

        private void SaveData()
        {
            EditorUtility.SetDirty(_target.gameObject);
        }

        private void UpdateData()
        {
            _viewComponents = new List<ViewCustomizableComponent>();
            
            var dirtyData = GetDirtyData();
            var dataWithAttribute = dirtyData.Where(x => x.GetType().GetCustomAttribute<CustomizableComponentAttribute>() != null);

            foreach (var data in dataWithAttribute)
            {
                var view = new ViewCustomizableComponent(data, data.GetType().GetCustomAttribute<CustomizableComponentAttribute>());
                view.GenerateField();
                _viewComponents.Add(view);
            }
        }

        private List<MonoBehaviour> GetDirtyData()
        {
            _target.MainDates.InitDicIfNotExit();
            List<MonoBehaviour> dirtyData = _target.MainDates.GetAll<DataProperty>().ToList<MonoBehaviour>();
            dirtyData = dirtyData.Union(_target.ComponentShell.GetAll<MonoBehaviour>()).ToList();
            return dirtyData;
        }
    }
}