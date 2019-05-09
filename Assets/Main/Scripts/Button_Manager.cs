using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Demo : MonoBehaviour
{
    Button cube;
    Button sphere;
    // Start is called before the first frame update

    void Start()
    {
        sphere = GameObject.Find("/Buttons/Canvas/Button1").GetComponent<Button>();
        cube = GameObject.Find("/Buttons/Canvas/Button2").GetComponent<Button>();

        sphere.Select();
    }

    // Update is called once per frame
    void Update()
    {
    }

}
