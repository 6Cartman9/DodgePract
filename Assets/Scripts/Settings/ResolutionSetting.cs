using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResolutionSetting : Settings
{
    [SerializeField]
    private Vector2Int[] avalibleResolution = new Vector2Int[]
    {
        new Vector2Int(800, 600),
        new Vector2Int(1280, 720),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080),
    };

    private int currentResolution = 0;

    public override bool isMinValue { get => currentResolution == 0; }
    public override bool isMaxValue { get => currentResolution == avalibleResolution.Length - 1; }

    public override void SetNextValue()
    {
        if(isMaxValue == false)
        {
            currentResolution++;
        }
    }

    public override void SetPreviousValue()
    {
        if (isMinValue == false)
        {
            currentResolution--;
        }
    }

    public override object GetValue()
    {
        return avalibleResolution[currentResolution];
    }

    public override string GetStringValue()
    {
        return avalibleResolution[currentResolution].x + "x" + avalibleResolution[currentResolution].y;
    }

    public override void Apply()
    {
        Screen.SetResolution(avalibleResolution[currentResolution].x, avalibleResolution[currentResolution].y, true);
        Save();
    }

    public override void Load()
    {
        currentResolution = PlayerPrefs.GetInt(title, avalibleResolution.Length - 1);
    }

    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolution);
    }
}
