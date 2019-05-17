using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasButton : MonoBehaviour
{
    private GameObject atomsmanager;
    AtomsManager AMscript;

    public GameObject playercontroller = null;
    PlayerController PCscript;

    private float R = 1.0f;

    float X = 0.0f;
    float Y = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        atomsmanager = GameObject.Find("AtomsManager");
        AMscript = atomsmanager.GetComponent<AtomsManager>();

        PCscript = playercontroller.GetComponent<PlayerController>();

        switch (transform.name)
        {
            case "Button1":
                X = 0.32f;
                Y = 0.32f;
                break;
            case "Button2":
                X = 0.32f;
                Y = 0.264f;
                break;
            case "Button3":
                X = 0.32f;
                Y = 0.204f;
                break;
            case "Button4":
                X = 0.155f;
                Y = 0.315f;
                break;
            case "Button5":
                X = 0.155f;
                Y = 0.26f;
                break;
            case "Button6":
                X = 0.155f;
                Y = 0.2f;
                break;
            case "Button7":
                X = 0.48f;
                Y = 0.375f;
                break;
            case "Button8":
                X = -0.044f;
                Y = 0.375f;
                break;
            case "Button9":
                X = 0.48f;
                Y = 0.09f;
                break;
            case "Button10":
                X = -0.044f;
                Y = 0.09f;
                break;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        Vector3 forward = PCscript.getForward();
        Vector3 position = PCscript.getPosition();
        Vector3 up = PCscript.getUp();
        Vector3 right = PCscript.getRight();
        transform.position = position + (R*1.01f) * forward + X*right + Y*up;
        transform.LookAt(position + R * forward + X * right + Y * up);
        transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
    }

    public void Action()
    {
        switch(transform.name)
        {
            case "Button1":
                AMscript.addLx();
                break;
            case "Button2":
                AMscript.addLy();
                break;
            case "Button3":
                AMscript.addLz();
                break;
            case "Button4":
                AMscript.minusLx();
                break;
            case "Button5":
                AMscript.minusLy();
                break;
            case "Button6":
                AMscript.minusLz();
                break;
            case "Button7":
                AMscript.forward_structure_type();
                break;
            case "Button8":
                AMscript.backward_structure_type();
                break;
            case "Button9":
                AMscript.addBL();
                break;
            case "Button10":
                AMscript.minusBL();
                break;
        }

        switch(transform.name)
        {
            case "Button1":
            case "Button2":
            case "Button3":
            case "Button4":
            case "Button5":
            case "Button6":
            case "Button7":
            case "Button8":
            case "Button9":
            case "Button10":
                AMscript.ChangeAtoms();
                break;
        }
    }
}
