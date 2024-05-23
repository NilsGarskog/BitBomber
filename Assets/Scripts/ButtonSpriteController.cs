using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ButtonSpriteController : MonoBehaviour
{
    public GameObject bombObject;
    public GameObject explosionObject;
    public SFXcontroller audioManagerSfx;

    public void OnSelect(BaseEventData eventData)
    {
        bombObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        bombObject.SetActive(false);
    }

    public void OnClick()
    {
        explosionObject.SetActive(true);
        bombObject.SetActive(false);
        audioManagerSfx.GetComponent<SFXcontroller>().PlayExplosion();
    }
}
