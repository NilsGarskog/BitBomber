using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SettingsMenu : MonoBehaviour
{
    [Header("First selected")]
    [SerializeField] private GameObject _backMenuFirstSelected;
    [SerializeField] private GameObject _swedenMenuFirstSelected;
    [SerializeField] private GameObject _japanMenuFirstSelected;
    [SerializeField] private GameObject _denmarkMenuFirstSelected;
    [SerializeField] private GameObject _ukMenuFirstSelected;

    public void OpenUKMenu()
    {
        EventSystem.current.SetSelectedGameObject(_ukMenuFirstSelected);
    }
    public void OpenDenmarkMenu()
    {
        EventSystem.current.SetSelectedGameObject(_denmarkMenuFirstSelected);
    }
    public void OpenJapanMenu()
    {
        EventSystem.current.SetSelectedGameObject(_japanMenuFirstSelected);
    }
    public void OpenSwedenMenu()
    {
        EventSystem.current.SetSelectedGameObject(_swedenMenuFirstSelected);
    }
    public void OpenBackMenu()
    {
        EventSystem.current.SetSelectedGameObject(_backMenuFirstSelected);
    }

}
