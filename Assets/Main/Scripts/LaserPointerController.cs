using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerController : MonoBehaviour
{
    Vector3 vel3 = new Vector3(0.0f, 1.0f, 0.0f);
    /*
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
    private Rigidbody hitRb;
    private Rigidbody catchRb;

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
    */
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        UpdateLaser();

        if(hitRb)
        {
            if(Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                CatchObject();
            }
        }

        if(catchRb)
        {
            lastCatchObjPosition = catchRb.transform.position;
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
            hitRb = hitInfo.rigidbody;
        }
        else
        {
            laserPointerRenderer.SetPosition(1, pointerRay.origin + pointerRay.direction * maxDistance);
            hitRb = null;
        }
        */
    }

    /*
    private Ray GenerateRay()
    {
        if(OVRManager.isHmdPresent)
        {
            return new Ray(pointer.position, pointer.forward);
        }
        else
        {
            return new Ray(pointer.position + new Vector3(0, -1.4f, 0), pointer.forward + new Vector3(0, -0.15f, 0));
        }
    }
    */
    public Vector3 get_rotate_vector()
    {
        return vel3;
    }
    /*
    private void CatchObject()
    {
        FixedJoint pointerJoint = pointer.gameObject.GetComponent<FixedJoint>();
        if(pointerJoint)
        {
            pointerJoint.connectedBody = null;
            Destroy(pointerJoint);
        }
        catchRb = hitRb;

        pointerJoint = pointer.gameObject.AddComponent<FixedJoint>();
        pointerJoint.breakForce = 20000;
        pointerJoint.breakTorque = 20000;
        pointerJoint.connectedBody = catchRb;
    }

    private void ThrowObject()
    {
        FixedJoint pointerJoint = pointer.gameObject.GetComponent<FixedJoint>();
        if(pointerJoint)
        {
            pointerJoint.connectedBody = null;
            Destroy(pointerJoint);
            Vector3 catchRbVelocity = (catchRb.transform.position - lastCatchObjPosition) / Time.deltaTime;
            catchRb.AddForce(catchRbVelocity, ForceMode.Impulse);
        }
        catchRb = null;
    }
    */
}
