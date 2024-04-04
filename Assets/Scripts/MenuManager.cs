using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{

    [Header("First selected")]
    [SerializeField] private GameObject _playMenuFirstSelected;
    [SerializeField] private GameObject _settingsMenuFirstSelected;
    [SerializeField] private GameObject _quitMenuFirstSelected;



    public void OpenSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(_settingsMenuFirstSelected);
    }
    public void OpenPlayMenu()
    {
        EventSystem.current.SetSelectedGameObject(_playMenuFirstSelected);
    }

    public void OpenQuitMenu()
    {
        EventSystem.current.SetSelectedGameObject(_quitMenuFirstSelected);
    }


    public void OnPlayPressed()
    {
        OpenPlayMenu();
    }
    public void OnSettingsPressed()
    {
        OpenSettingsMenu();
    }
    public void OnQuitPressed()
    {
        OpenQuitMenu();
    }
}
