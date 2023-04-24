using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManger : MonoBehaviour
{
    public static MasterManger current { get; private set; }

    public bool loaded = false;

    void Awake()
    {
        if (current != null && current != this)
        {
            Destroy(this);
            return;
        }
        else
        {
            current = this;
            DontDestroyOnLoad(this);
        }

        // loaded = true;

    }

    // Start is called before the first frame update
    void Start()
    {




        loaded = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
