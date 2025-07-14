using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GraphicsQualitySetting : Settings
{
    private int curretLevelIndex = 0;
    public override bool isMinValue { get => curretLevelIndex == 0; }
    public override bool isMaxValue { get => curretLevelIndex == QualitySettings.names.Length - 1; }

    public override void SetNextValue()
    {
        if(isMaxValue == false)
        {
            curretLevelIndex++;
        }
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false)
        {
            curretLevelIndex--;
        }
    }

    public override object GetValue()
    {
        return QualitySettings.names[curretLevelIndex];
    }

    public override string GetStringValue()
    {
        return QualitySettings.names[curretLevelIndex];
    }

    public override void Apply()
    {
        QualitySettings.SetQualityLevel(curretLevelIndex);
        Save();
    }

    public override void Load()
    {
        curretLevelIndex = PlayerPrefs.GetInt(title, QualitySettings.names.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, curretLevelIndex);
    }
}
