using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class LaserPointerController : MonoBehaviour
{
    [SerializeField]
    private Transform rightHandAnchor;
    [SerializeField]
    private Transform leftHandAnchor;
    [SerializeField]
    private Transform centerEyeAnchor;
    [SerializeField]
    private LineRenderer laserPointerRenderer;

    private float maxDistance = 30.0f;

    private Transform pointer;
    private Vector3 lastCatchObjPosition;
    private bool rotate_flag = false;

    private Vector3 before_vec;
    private Vector3 after_vec;

    public Vector3 rotate_velocity;

    private GameObject hitRb;

    private CanvasButton CB;
    private bool onClickflag = false;

    private Transform Pointer
    {
        get
        {
            var controller = OVRInput.GetActiveController();
            if (controller == OVRInput.Controller.RTrackedRemote)
            {
                return rightHandAnchor;
            }
            else if(controller == OVRInput.Controller.LTrackedRemote)
            {
                return leftHandAnchor;
            }
            return centerEyeAnchor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        UpdateLaser();

        if(Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            //Debug.Log("KeyCode.Space");

            if (hitRb != null)
            {

                ButtonScript buttonscript = hitRb.GetComponent<ButtonScript>();
                if (buttonscript != null)
                {
                    Debug.Log("ButtonScript : " + buttonscript.ToString());
                    buttonscript.OnClick();
                    hitRb.GetComponent<Button>().Select();
                }
                else
                {
                    rotate_flag = true;
                }
            }
            else
            {
                rotate_flag = true;
            }

            if(CB != null)
            {
                CB.Action();
                CB = null;
            }
            onClickflag = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            rotate_flag = false;
            onClickflag = false;
        }

    }

    /*
    private GameObject[] GetChildren(string parentName)
    {
        //http://meokz.hatenablog.com/entry/2015/101611
        // 検索し、GameObject型に変換
        var parent = GameObject.Find(parentName) as GameObject;
        // 見つからなかったらreturn
        if (parent == null) return null;
        // 子のTransform[]を取り出す
        var transforms = parent.GetComponentsInChildren<Transform>();
        // 使いやすいようにtransformsからgameObjectを取り出す
        var gameObjects = from t in transforms select t.gameObject;
        // 配列に変換してreturn
        return gameObjects.ToArray();
    }*/

    private void UpdateLaser()
    {
        pointer = Pointer;
        CB = null;

        Ray pointerRay = GenerateRay();

        laserPointerRenderer.SetPosition(0, pointerRay.origin);
        RaycastHit hitInfo;
        if(Physics.Raycast(pointerRay, out hitInfo, maxDistance))
        {
            laserPointerRenderer.SetPosition(1, hitInfo.point);
            hitRb = hitInfo.transform.root.gameObject;
            Debug.Log("hitRb : " + hitRb.ToString());
            /*
            var uiObjects = GetChildren(hitRb.ToString());
            if (uiObjects != null)
            {
                foreach (var uiObject in uiObjects) Debug.Log("hitRb children : " + uiObject.ToString());
            }
            */
            var canvasbutton = hitRb.GetComponent<CanvasButton>();
            if (canvasbutton != null)
            {
                switch (canvasbutton.transform.name)
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
                        if (!onClickflag) CB = canvasbutton;
                        break;
                }
            }
        }
        else
        {
            laserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * maxDistance);
            hitRb = null;
        }
        if (!rotate_flag)
        {
            before_vec = pointerRay.direction;
        }
        after_vec = pointerRay.direction;

    }


    private Ray GenerateRay()
    {
        //return new Ray(pointer.position, pointer.position);
        if(OVRManager.isHmdPresent)
        {
            return new Ray(pointer.position, pointer.forward);
        }
        else
        {
            return new Ray(pointer.position + new Vector3(0, -1.4f, 0), pointer.forward + new Vector3(0, -0.15f, 0));
        }
    }

    public Vector3 Get_rotate_vector()
    {
        Vector3 vec_tmp = new Vector3( 0.0f, 0.0f, 0.0f);
        if (rotate_flag)
        {
            vec_tmp = Vector3.Cross(before_vec, after_vec);
            //Debug.Log("before_vec : " + before_vec.ToString());
            //Debug.Log("after_vec : " + after_vec.ToString());
            //Debug.Log("vec_tmp : " + vec_tmp.ToString());
        }
        before_vec = after_vec;
        const float C = 100.0f;
        vec_tmp.x *= C;
        vec_tmp.y *= C;
        vec_tmp.z *= C;
        return -vec_tmp;
    }
}
