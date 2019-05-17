using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasText : MonoBehaviour
{
    public GameObject score_object = null;

    private GameObject atomsmanager;
    AtomsManager AMscript;

    public GameObject playercontroller = null;
    PlayerController PCscript;

    //GameObject viewer;

    int Lx = 0;
    int Ly = 0;
    int Lz = 0;
    float BOND_LENGTH = 0.0f;
    string structure_name;

    private float R = 1.0f;

    double TextScale = 0.01;

    // Start is called before the first frame update
    void Start()
    {
        atomsmanager = GameObject.Find("AtomsManager");
        AMscript = atomsmanager.GetComponent<AtomsManager>();

        //viewer = GameObject.Find("OVRCameraRig");

        //playercontroller = GameObject.Find("PlayerController");
        PCscript = playercontroller.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Lx = AMscript.Lx;
        Ly = AMscript.Ly;
        Lz = AMscript.Lz;
        BOND_LENGTH = AMscript.BOND_LENGTH;
        structure_name = AMscript.structure_name;
        Text score_text = score_object.GetComponent<Text>();
        string text = structure_name + "\n";
        text = text + "Lx          : " + Lx.ToString() + "\n";
        text = text + "Ly          : " + Ly.ToString() + "\n";
        text = text + "Lz          : " + Lz.ToString() + "\n";
        text = text + "\n";
        text = text + "Bond Length : " + BOND_LENGTH.ToString("f2");
        score_text.text = text;
        Vector3 forward = PCscript.getForward();
        Vector3 position = PCscript.getPosition();
        score_text.transform.position = position + R*forward;
        score_text.transform.LookAt(position);
        score_text.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
        //Debug.Log(score_text.transform.position.x.ToString() + " , " + score_text.transform.position.y.ToString() + " , " + score_text.transform.position.z.ToString());
        //Debug.Log(score_text.transform.position.ToString() + " , " + score_text.transform.forward.ToString());
    }

    public Vector3 getPosition()
    {
        return score_object.transform.position;
    }
}
