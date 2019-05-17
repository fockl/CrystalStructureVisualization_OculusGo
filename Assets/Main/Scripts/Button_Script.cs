using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Script : MonoBehaviour
{
    private GameObject atomsmanager;
    AtomsManager atomsmanagerscript;

    // Start is called before the first frame update
    void Start()
    {
        atomsmanager = GameObject.Find("AtomsManager");
        atomsmanagerscript = atomsmanager.GetComponent<AtomsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        switch (transform.name)
        {
            case "Button1":
                atomsmanagerscript.structure_type = 0;
                atomsmanagerscript.ChangeAtoms();
                Debug.Log("Diamond");
                break;
            case "Button2":
                atomsmanagerscript.structure_type = 1;
                atomsmanagerscript.ChangeAtoms();
                Debug.Log("BCC");
                break;
            case "Button3":
                atomsmanagerscript.structure_type = 2;
                atomsmanagerscript.ChangeAtoms();
                Debug.Log("FCC");
                break;
        }
    }
}
