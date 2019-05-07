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
                atomsmanagerscript.ChangeAtoms(0);
                Debug.Log("Diamond");
                break;
            case "Button2":
                atomsmanagerscript.ChangeAtoms(1);
                Debug.Log("BCC");
                break;
            case "Button3":
                atomsmanagerscript.ChangeAtoms(2);
                Debug.Log("FCC");
                break;
        }
    }
}
