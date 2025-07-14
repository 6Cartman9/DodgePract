using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISettingButton : UISelectableButton, IScriptableObjectProperty
{
    [SerializeField] private Settings setting;
    [SerializeField] private Text titleText;
    [SerializeField] private Text valueText;
    [SerializeField] private Image previousImage;
    [SerializeField] private Image nextImage;

    public void SetNextValueSetting()
    {
        setting?.SetNextValue();
        setting?.Apply();
        UpdateInfo();
    }

    public void SetPreviousValueSetting() 
    {
        setting?.SetPreviousValue();
        setting?.Apply();
        UpdateInfo();

    } 

    private void Start()
    {
        ApplyProperty(setting); 
    }

    private void UpdateInfo()
    {
        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.isMinValue;
        nextImage.enabled = !setting.isMaxValue;
    }

    public void ApplyProperty(ScriptableObject property)
    {
        if (property == null) return;

        if (property is Settings == false) return;

        setting = property as Settings;

        titleText.text = setting.Title;
        valueText.text = setting.GetStringValue();

        previousImage.enabled = !setting.isMinValue;
        nextImage.enabled = !setting.isMaxValue;
    }
}
