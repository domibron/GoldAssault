using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class PlayInteraction : MonoBehaviour
{
    //! https://www.youtube.com/watch?v=_yf5vzZ2sYE&t=229s
    //! https://www.youtube.com/watch?v=cxJnvEpwQHc&t=674s

    private Transform _currentSelection;

    private ISelector _selector;
    private IRayProvider _rayProvider;
    private ISelectionResponse _slectionResponse;

    private ItemInteract _interaction;

    private PlayerInteractionText playerInteractionText;

    private DisplayText displayText;

    // Start is called before the first frame update
    void Awake()
    {
        _selector = GetComponent<ISelector>();
        _rayProvider = GetComponent<IRayProvider>();
        _slectionResponse = GetComponent<ISelectionResponse>();

        playerInteractionText = GetComponent<PlayerInteractionText>();

        displayText = new DisplayText();

        displayText.text = "Press <color=blue>F</color> to <color=blue>interact</color>";
        displayText.priority = 1;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (_currentSelection != null)
            {
                _slectionResponse.OnDeselect(_currentSelection);
            }

            _selector.Check(_rayProvider.CreateRay());
            _currentSelection = _selector.GetSelection();

            // local - solid this pelase
            if (_currentSelection != null)
            {
                _interaction = _currentSelection.GetComponent<ItemInteract>();

                // this is to prevent multiple instance of the data, if there is more than one then it will stay on the screen forever.
                if (!playerInteractionText.IsInTheList(displayText))
                {
                    playerInteractionText.AddInteractionText(displayText);
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    _interaction.OnInteraction();
                }
            }

            if (_currentSelection != null)
            {
                _slectionResponse.OnSelect(_currentSelection);
            }

            if (_currentSelection == null)
            {
                playerInteractionText.RemoveInteractionText(displayText);
            }
        }
        catch (NullReferenceException)
        {
            // no
        }
    }
}

