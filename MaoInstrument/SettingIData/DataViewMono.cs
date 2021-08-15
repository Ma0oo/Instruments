using System;
using System.Collections.Generic;
using System.Reflection;
using Plugins.Interfaces;
using Plugins.SettingIData.Attribute;
using Plugins.SettingIData.MonoInput;
using UnityEngine;

namespace Plugins.SettingIData
{
    public class DataViewMono : MonoBehaviour
    {
        [SerializeField] private Transform _parentOfInputs;
        [SerializeField] private DataSliderInput _sliderInputTemplate;
        [SerializeField] private DataButtonInput _buttonInputTemplate;
        [SerializeField] private DataKeyboardInput _keyboardInput;
        
        private List<GameObject> _inputObjects = new List<GameObject>();
        private Dictionary<FieldInfo, DataAttribute> _currentListNameAndAttribute;

        private void Generate<T>(T data) where  T : class, IData, new()
        {
            _currentListNameAndAttribute = FieldAndAttribute(data);
            SpawnInputFieldInput(data, _currentListNameAndAttribute);
        }

        public void Clear()
        {
            for (int i = 0; i < _inputObjects.Count; i++) Destroy(_inputObjects[i].gameObject);
            _inputObjects = new List<GameObject>();
        }

        #region SpawnInputFieldsByData

        private void SpawnInputFieldInput(IData data, Dictionary<FieldInfo, DataAttribute> dictionary)
        {
            foreach (var pair in dictionary)
            {
                var currentValue = pair.Key.GetValue(data);
                if (pair.Value is DataSlider) SpawnSliderInput(pair, currentValue, data);
                else if (pair.Value is DataButton) SpawnButtonInput(pair, currentValue, data);
                else if (pair.Value is DataKeyboar) SpawnKeyBoardInut(pair, currentValue, data);
                else throw new Exception($"Unknowns DataAttribute - {pair.Value.GetType()}");
            }
        }

        #endregion

        #region Other

        private static Dictionary<FieldInfo, DataAttribute> FieldAndAttribute(IData data)
        {
            var fields = data.GetType().GetFields();
            var nameAndAttribute = new Dictionary<FieldInfo, DataAttribute>();
            foreach (var prop in fields)
                if (prop.GetCustomAttribute<DataAttribute>() != null)
                    nameAndAttribute.Add(prop, prop.GetCustomAttribute<DataAttribute>());
            return nameAndAttribute;
        }

        #endregion
        
        #region SpawnInputFieldMethod
        
        private void SpawnKeyBoardInut(KeyValuePair<FieldInfo, DataAttribute> pair, object currentValue, IData data)
        {
            var dataButton = pair.Value as DataKeyboar;

            DataKeyboardInput dataKeyboardInput = Instantiate(_keyboardInput, _parentOfInputs);
            dataKeyboardInput.Init(
                dataButton,
                currentValue,
                v => pair.Key.SetValue(data, v));

            _inputObjects.Add(dataKeyboardInput.gameObject);
        }

        private void SpawnButtonInput(KeyValuePair<FieldInfo, DataAttribute> pair, object currentValue, IData data)
        {
            var dataButton = pair.Value as DataButton;

            DataButtonInput dataButtonInput = Instantiate(_buttonInputTemplate, _parentOfInputs);
            dataButtonInput.Init(
                dataButton,
                currentValue,
                v => pair.Key.SetValue(data, v));

            _inputObjects.Add(dataButtonInput.gameObject);
        }

        private void SpawnSliderInput(KeyValuePair<FieldInfo, DataAttribute> pair, object currentValue, IData data)
        {
            var dataSlider = pair.Value as DataSlider;

            DataSliderInput newInstance = Instantiate(_sliderInputTemplate, _parentOfInputs);
            newInstance.Init(dataSlider,
                currentValue,
                v =>
                {
                    if(pair.Key.GetValue(data) is int)
                        pair.Key.SetValue(data, Convert.ChangeType(v, typeof(int)));
                    else
                        pair.Key.SetValue(data, v);
                });

            _inputObjects.Add(newInstance.gameObject);
        }
        
        #endregion
    }
}