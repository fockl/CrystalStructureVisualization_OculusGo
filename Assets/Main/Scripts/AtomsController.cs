using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtomsController : MonoBehaviour
{
    public UnityEvent OnDestroy;

    // Start is called before the first frame update
    void Start()
    {
        if (OnDestroy == null)
            OnDestroy = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
