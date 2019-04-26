using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtomsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject AtomsPrefab;

    private GameObject laserpointercontroller;
    LaserPointerController script;

    private const int Lx = 2;
    private const int Ly = 2;
    private const int Lz = 2;
    private const int S = 8;
    private GameObject[,,,] atom = new GameObject[Lx,Ly,Lz,S];

    public UnityEvent OnDestroy;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        CreateAtoms(new Vector3());
        //laserpointercontroller = GameObject.Find("LaserPointer");
        laserpointercontroller = GameObject.Find("OVRCameraRig");
        script = laserpointercontroller.GetComponent<LaserPointerController>();
        if (OnDestroy == null)
            OnDestroy = new UnityEvent();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(script);
        Vector3 rotate_vector = script.get_rotate_vector();
        Debug.Log("rotate_vector : " + rotate_vector.ToString());
        Vector3 point = new Vector3(0.0f, 0.0f, 0.0f);
        float angle = rotate_vector.magnitude;
        Vector3 axis = rotate_vector.normalized;
        for(int x = 0; x < Lx; x++)
        {
            for(int y = 0; y < Ly; y++)
            {
                for(int z = 0; z < Lz; z++)
                {
                    for(int s = 0; s < S; s++)
                    {
                        atom[x, y, z, s].transform.RotateAround(point, axis, angle);
                    }
                }
            }
        }
    }

    private void CreateAtoms(Vector3 rotate_vector)
    {
        var units = new Vector3[]
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(0.5f, 0.5f, 0.0f),
            new Vector3(0.5f, 0.0f, 0.5f),
            new Vector3(0.0f, 0.5f, 0.5f),
            new Vector3(0.25f, 0.25f, 0.25f),
            new Vector3(0.75f, 0.75f, 0.25f),
            new Vector3(0.75f, 0.25f, 0.75f),
            new Vector3(0.25f, 0.75f, 0.75f)
        };
        float Scalex = 10.0f;
        float Scaley = 10.0f;
        float Scalez = 10.0f;
        var Shift = new Vector3(-5.0f, -5.0f, -5.0f);
        var color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        for(int x = 0; x < Lx; x++)
        {
            for(int y = 0; y < Ly; ++y)
            {
                for(int z = 0; z < Lz; ++z)
                {
                    for (int s = 0; s < S; ++s)
                    {
                        var x_pos = (units[s].x + x * 1.0f) * Scalex / Lx + Shift.x;
                        var y_pos = (units[s].y + y * 1.0f) * Scaley / Ly + Shift.y;
                        var z_pos = (units[s].z + z * 1.0f) * Scalez / Lz + Shift.z;
                        var position = new Vector3(x_pos, y_pos, z_pos);
                        atom[x,y,z,s] = Instantiate(AtomsPrefab, position, new Quaternion());
                        atom[x,y,z,s].GetComponent<Renderer>().material.color = color;
                        float rotate_angle = rotate_vector.magnitude;
                        Vector3 rotate_axis = rotate_vector.normalized;
                    }
                }
            }
        }
    }
}
