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

    /*
    private const int Lx = 2;
    private const int Ly = 2;
    private const int Lz = 2;
    private const int S = 8;
    private GameObject[,,,] atom = new GameObject[Lx,Ly,Lz,S];
    */
    private GameObject[] atom;
    private GameObject[] line;
    public int Lx;
    public int Ly;
    public int Lz;
    private Vector3[] units;
    private int S;

    public UnityEvent OnDestroy;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        laserpointercontroller = GameObject.Find("OVRCameraRig");
        script = laserpointercontroller.GetComponent<LaserPointerController>();

        if (OnDestroy == null)
            OnDestroy = new UnityEvent();

        SetAtoms(0);
        CreateAtoms();
        CreateLines();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(script);
        Vector3 rotate_vector = script.get_rotate_vector();
        //Debug.Log("rotate_vector : " + rotate_vector.ToString());
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
                        int index = x * Ly * Lz * S + y * Lz * S + z * S + s;
                        atom[index].transform.RotateAround(point, axis, angle);
                    }
                }
            }
        }
        DestroyLines();
        CreateLines();
    }

    private void SetAtoms(int structure_type)
    {
        switch (structure_type)
        {
            case 0:
                units = new Vector3[]
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
                break;
            case 1:
                units = new Vector3[]
                {
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(0.5f, 0.5f, 0.5f)
                };
                break;
            case 2:
                units = new Vector3[]
                {
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(0.5f, 0.5f, 0.0f),
                    new Vector3(0.5f, 0.0f, 0.5f),
                    new Vector3(0.0f, 0.5f, 0.5f)
                };
                break;
        }
        S = units.Length;
    }

    private void CreateAtoms()
    {
        Lx = 2;
        Ly = 2;
        Lz = 2;
        atom = new GameObject[Lx*Ly*Lz*S];

        float Scalex = 10.0f;
        float Scaley = 10.0f;
        float Scalez = 10.0f;
        var Shift = new Vector3(-5.0f, -5.0f, -5.0f);
        var color = new Color(1.0f, 1.0f, 1.0f, 0.5f);

        for(int x = 0; x < Lx; ++x)
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
                        int index = x * Ly * Lz * S + y * Lz * S + z * S + s;
                        atom[index] = Instantiate(AtomsPrefab, position, new Quaternion());
                        atom[index].GetComponent<Renderer>().material.color = color;
                        //float rotate_angle = rotate_vector.magnitude;
                        //Vector3 rotate_axis = rotate_vector.normalized;
                    }
                }
            }
        }
    }

    private void DestroyAtoms()
    {
        for(int x=0; x<Lx; ++x)
        {
            for(int y=0; y<Ly; ++y)
            {
                for(int z=0; z<Lz; ++z)
                {
                    for(int s=0; s<S; ++s)
                    {
                        int index = x * Ly * Lz * S + y * Lz * S + z * S + s;
                        Destroy(atom[index]);
                    }
                }
            }
        }
        OnDestroy.Invoke();
    }

    private void CreateLines()
    {
        line = new GameObject[0];
        var BOND_LENGTH = 3.0f;
        var color = new Color(0.3f, 0.3f, 1.0f, 0.5f);
        for (int index1 = 0; index1 < atom.Length; ++index1)
        {
            for(int index2 = index1 + 1; index2 < atom.Length; ++index2)
            {
                Vector3 tmp1 = atom[index1].transform.position;
                Vector3 tmp2 = atom[index2].transform.position;
                Vector3 diff = tmp1 - tmp2;
                if(diff.magnitude <= BOND_LENGTH)
                {
                    GameObject newLine = new GameObject("Line");
                    LineRenderer lRend = newLine.AddComponent<LineRenderer>();
                    lRend.positionCount = 2;
                    lRend.startWidth = 0.5f;
                    lRend.endWidth = 0.5f;
                    lRend.SetPosition(0, tmp1);
                    lRend.SetPosition(1, tmp2);
                    newLine.GetComponent<Renderer>().material.color = color;

                    GameObject[] line_copy = new GameObject[line.Length + 1];
                    System.Array.Copy(line, line_copy, line.Length);
                    line_copy[line.Length] = newLine;
                    line = line_copy;    
                }
            }
        }
        //Debug.Log("line.Length = " + line.Length.ToString());
    }

    private void DestroyLines()
    {
        for(int i=0; i<line.Length; ++i)
        {
            Destroy(line[i]);
        }
        OnDestroy.Invoke();
    }


    public void ChangeAtoms(int structure_type)
    {
        DestroyAtoms();
        SetAtoms(structure_type);
        CreateAtoms();
    }
}
