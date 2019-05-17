using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public GameObject button_object = null;

    public GameObject playercontroller = null;
    PlayerController PCscript;

    public GameObject canvasscript = null;
    CanvasScript CSscript;

    //Text Buttontext;
    private float R = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        CSscript = canvasscript.GetComponent<CanvasScript>();
        //Buttontext = CSscript.FindText("Button");

        PCscript = playercontroller.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Buttontext.text = "" + Time.deltaTime.ToString();
        /*
        Vector3 forward = PCscript.getForward();
        Vector3 position = PCscript.getPosition();
        button_object.transform.position = position + R * forward;
        button_object.transform.LookAt(position);
        button_object.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        */      
        //Debug.Log("button : " + button_object.transform.position.ToString());
    }
    public void OnClick()
    {
        Debug.Log("Button click!");
    }
}
