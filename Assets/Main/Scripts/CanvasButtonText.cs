using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasButtonText : MonoBehaviour
{
    public GameObject playercontroller = null;
    PlayerController PCscript;

    private float R = 1.0f;

    float X = 0.0f;
    float Y = 0.0f;

    double TextScale = 0.01;

    // Start is called before the first frame update
    void Start()
    {
        PCscript = playercontroller.GetComponent<PlayerController>();

        switch (transform.name)
        {
            case "Text1":
                X = 0.3f;
                Y = 0.285f;
                break;
            case "Text2":
                X = 0.3f;
                Y = 0.23f;
                break;
            case "Text3":
                X = 0.3f;
                Y = 0.17f;
                break;
            case "Text4":
                X = 0.15f;
                Y = 0.285f;
                break;
            case "Text5":
                X = 0.15f;
                Y = 0.23f;
                break;
            case "Text6":
                X = 0.15f;
                Y = 0.17f;
                break;
            case "Text7":
                X = 0.45f;
                Y = 0.34f;
                break;
            case "Text8":
                X = -0.05f;
                Y = 0.34f;
                break;
            case "Text9":
                X = 0.45f;
                Y = 0.057f;
                break;
            case "Text10":
                X = -0.05f;
                Y = 0.057f;
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
        transform.position = position + R * forward + X * right + Y * up;
        transform.LookAt(position + R * forward + X * right + Y * up);
        //transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        //Debug.Log(score_text.transform.position.x.ToString() + " , " + score_text.transform.position.y.ToString() + " , " + score_text.transform.position.z.ToString());
        //Debug.Log(score_text.transform.position.ToString() + " , " + score_text.transform.forward.ToString());
    }
}
