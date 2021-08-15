using Plugins.SettingIData.Attribute;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Plugins.SettingIData.MonoInput
{
    public class DataButtonInput : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _labelField;
        [SerializeField] private TextMeshProUGUI _labelValue;
        [SerializeField] private Button _buttonNext;
        [SerializeField] private Button _buttonPrev;
        
        public void Init(DataButton dataButton, object currentValue, UnityAction<object> changeValue)
        {
            InitText(dataButton, currentValue);
            
            _buttonNext.onClick.AddListener(() =>
            {
                var obj = dataButton.Next();
                changeValue?.Invoke(obj);
                UpdateValueText(obj);
            });
            
            _buttonPrev.onClick.AddListener(() =>
            {
                var obj = dataButton.Prev();
                changeValue?.Invoke(obj);
                UpdateValueText(obj);
            });
        }

        private void InitText(DataButton dataButton, object currentValue)
        {
            _labelField.text = dataButton.NameProperty;
            UpdateValueText(currentValue);
        }

        private void UpdateValueText(object obj)
        {
            Debug.Log(obj);
            _labelValue.text = obj.ToString();
        }
    }
}