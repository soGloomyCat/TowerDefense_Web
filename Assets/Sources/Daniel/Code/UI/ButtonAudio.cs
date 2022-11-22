using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense.Daniel
{
    public class ButtonAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, ISelectHandler, IDeselectHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        public void OnPointerClick(PointerEventData eventData)
        {
            AudioController.TryPlayUIClick();
        }

        public void OnSelect(BaseEventData eventData)
        {

        }

        public void OnDeselect(BaseEventData eventData)
        {

        }
    }
}