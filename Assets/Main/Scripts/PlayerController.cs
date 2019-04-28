using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    private float speed = 1.0f;
    private float rotateSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || OVRInput.Get(OVRInput.Button.Back))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if(OVRManager.isHmdPresent)
        {
            moveOculusGo();
        }
        else
        {
            movePc();
        }
    }

    private Vector3 getCameraForward()
    {
        //Vector3 cameraDir = Camera.main.transform.forward;
        //return Vector3.ProjectOnPlane(cameraDir, Vector3.up);
        return Camera.main.transform.forward;
    }

    private void moveOculusGo()
    {
        if(OVRInput.Get(OVRInput.Button.One))
        {
            Vector2 touchPadPt = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            //Debug.Log("touchPadPt: " + touchPadPt.ToString());
            if(Mathf.Abs(touchPadPt.y) > 0.2)
            {
                Vector3 direction = getCameraForward();
                if(touchPadPt.y > 0)
                {
                    rb.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
                }
                else
                {
                    rb.MovePosition(transform.position - direction * speed * Time.fixedDeltaTime);
                }
            }
        }
    }

    private void movePc()
    {
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        }
        if(Mathf.Abs(Input.GetAxis("Vertical")) > 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                transform.Rotate(Input.GetAxis("Vertical") * rotateSpeed, 0, 0);
            }
            else
            {
                if (Input.GetAxis("Vertical") > 0)
                    rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
                else
                    rb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
            }
        }
    }
}
