using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Localization.Settings;
public class LocaleSelector : MonoBehaviour
{


    private void Start()
    {
        int ID = PlayerPrefs.GetInt("localeID", 0);
        ChangeLocale(ID);
    }
    private bool active = false;
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("localeID", _localeID);
        active = false;
    }
}
