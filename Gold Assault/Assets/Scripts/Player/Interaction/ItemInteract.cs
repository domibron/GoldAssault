using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemInteract : MonoBehaviour
{
    [SerializeField] public UnityEvent OnInteract;

    public void OnInteraction()
    {
        OnInteract.Invoke();
    }

}
