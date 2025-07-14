using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    [SerializeField] private Settings[] allSettings;

    private void Awake()
    {
        for(int i = 0; i < allSettings.Length; i++)
        {
            allSettings[i].Load();
            allSettings[i].Apply();
        }
    }
}
