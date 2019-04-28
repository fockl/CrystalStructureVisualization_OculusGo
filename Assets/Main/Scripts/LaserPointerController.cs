using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerController : MonoBehaviour
{
    Vector3 vel3 = new Vector3(0.0f, 1.0f, 0.0f);

    [SerializeField]
    private Transform rightHandAnchor;
    [SerializeField]
    private Transform leftHandAnchor;
    [SerializeField]
    private Transform centerEyeAnchor;
    [SerializeField]
    private LineRenderer laserPointerRenderer;

    private float maxDistance = 10.0f;

    private Transform pointer;
    private Vector3 lastCatchObjPosition;
    private bool rotate_flag = false;

    private Vector3 before_vec;
    private Vector3 after_vec;

    public Vector3 rotate_velocity;

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
            Debug.Log("KeyCode.Space");
            rotate_flag = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) || OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            rotate_flag = false;
        }

    }

    private void UpdateLaser()
    {
        pointer = Pointer;

        Ray pointerRay = GenerateRay();

        laserPointerRenderer.SetPosition(0, pointerRay.origin);
        RaycastHit hitInfo;
        if(Physics.Raycast(pointerRay, out hitInfo, maxDistance))
        {
            laserPointerRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            laserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * maxDistance);
        }
        if (!rotate_flag)
        {
            before_vec = pointerRay.direction;
        }
        after_vec = pointerRay.direction;

    }


    private Ray GenerateRay()
    {
        return new Ray(pointer.position, pointer.position);
        if(OVRManager.isHmdPresent)
        {
            return new Ray(pointer.position, pointer.forward);
        }
        else
        {
            return new Ray(pointer.position + new Vector3(0, -1.4f, 0), pointer.forward + new Vector3(0, -0.15f, 0));
        }
    }

    public Vector3 get_rotate_vector()
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
        const float C = 100;
        vec_tmp.x *= C;
        vec_tmp.y *= C;
        vec_tmp.z *= C;
        return -vec_tmp;
    }
}
