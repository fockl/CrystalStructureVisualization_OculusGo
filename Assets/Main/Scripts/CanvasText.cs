using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasText : MonoBehaviour
{
    public GameObject score_object = null;

    private GameObject atomsmanager;
    AtomsManager script;

    GameObject viewer;

    int Lx = 0;
    int Ly = 0;
    int Lz = 0;
    string structure_name;

    private float R = 1.0f;

    Vector3 TextPos = new Vector3(-1.5f, 0.4f, 0.0f);
    double TextScale = 0.01;

    // Start is called before the first frame update
    void Start()
    {
        atomsmanager = GameObject.Find("AtomsManager");
        script = atomsmanager.GetComponent<AtomsManager>();

        viewer = GameObject.Find("OVRCameraRig");
    }

    // Update is called once per frame
    void Update()
    {
        Lx = script.Lx;
        Ly = script.Ly;
        Lz = script.Lz;
        structure_name = script.structure_name;
        Text score_text = score_object.GetComponent<Text>();
        score_text.text = structure_name + "\nLx : " + Lx.ToString() + "\nLy : " + Ly.ToString() + "\nLz : " + Lz.ToString();
        score_text.transform.position = viewer.transform.position + R*viewer.transform.forward + TextPos;
    }
}
