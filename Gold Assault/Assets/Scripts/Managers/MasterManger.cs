using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterManger : MonoBehaviour
{
    public static MasterManger Instance { get; private set; }

    public bool loaded = false;

    float e;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this);

        // loaded = true;

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        e += Time.deltaTime;

        if (e >= 4)
        {
            loaded = true;
        }
    }
}
