using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISelectableContainer : MonoBehaviour
{
    [SerializeField] private Transform buttonsContainer;

    public bool Interavtable = true;
    public void SetInteractable(bool interactable) => Interavtable = interactable;

    private UISelectableButton[] buttons;

    private int selectButtonIndex = 0;

    private void Start()
    {
        buttons = buttonsContainer.GetComponentsInChildren<UISelectableButton>();

        if (buttons == null)
            Debug.LogError("Button list is empty");

        for(int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter += OnPointerEnter;
        }

        if (Interavtable == false) return;

        buttons[selectButtonIndex].SetFocuse();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].PointerEnter -= OnPointerEnter;
        }
    }

    private void OnPointerEnter(UIButton button)
    {
        SelectButton(button);
    }

    private void SelectButton(UIButton button)
    {
        if (Interavtable == false) return;

        buttons[selectButtonIndex].SetUnFocuse();

        for(int i = 0; i < buttons.Length; i++)
        {
            if(button == buttons[i])
            {
                selectButtonIndex = i;
                button.SetFocuse();
                break;
            }
        }
    }

    public void SelectNext() { }

    public void SelectPrevious() { }
}
