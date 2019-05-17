using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour
{
    static Canvas _canvas;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Text FindText(string name)
    {
        foreach(Transform child in _canvas.transform)
        {
            if(child.name == name)
            {
                foreach(Transform child2 in child.transform)
                {
                    if (child2.name == "Text")
                        return child2.GetComponent<Text>();
                }
            }
        }
        return null;
    }
}
