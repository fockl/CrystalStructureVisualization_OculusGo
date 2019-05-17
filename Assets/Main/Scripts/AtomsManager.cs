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
    private int[] LineBeginNum;
    private int[] LineEndNum;
    public int Lx;
    public int Ly;
    public int Lz;
    public float BOND_LENGTH;
    public int structure_type;
    public string structure_name;
    private Vector3[] units;
    private int S;
    private float Scale = 5.0f;

    public UnityEvent OnDestroy;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Start");
        laserpointercontroller = GameObject.Find("OVRCameraRig");
        script = laserpointercontroller.GetComponent<LaserPointerController>();

        if (OnDestroy == null)
            OnDestroy = new UnityEvent();
        Lx = 2;
        Ly = 2;
        Lz = 2;
        BOND_LENGTH = 0.6f;
        structure_type = 0;
        SetAtoms();
        CreateAtoms();
        CreateLines();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(script);
        Vector3 rotate_vector = script.Get_rotate_vector();
        //Debug.Log("rotate_vector : " + rotate_vector.ToString());
        Vector3 point = new Vector3(0.0f, 0.0f, 0.0f);
        float angle = rotate_vector.magnitude;
        Vector3 axis = rotate_vector.normalized;
        foreach(var a in atom) a.transform.RotateAround(point, axis, angle);
        //foreach (var l in line) l.transform.RotateAround(point, axis, angle);
        for(int i=0; i<line.Length; ++i)
        {
            Vector3 newBeginPos = atom[LineBeginNum[i]].transform.position;
            Vector3 newEndPos = atom[LineEndNum[i]].transform.position;

            var lRend = line[i].GetComponent<LineRenderer>();
            //lRend.transform.RotateAround(point, axis, angle);
            lRend.SetPosition(0, newBeginPos);
            lRend.SetPosition(1, newEndPos);
        }
    }
    
    private void SetAtoms()
    {
        while (structure_type < 0) structure_type += 3;
        structure_type = structure_type % 3;
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
                structure_name = "Diamond Structure";
                break;
            case 1:
                units = new Vector3[]
                {
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(0.5f, 0.5f, 0.5f)
                };
                structure_name = "bcc Structure";
                break;
            case 2:
                units = new Vector3[]
                {
                    new Vector3(0.0f, 0.0f, 0.0f),
                    new Vector3(0.5f, 0.5f, 0.0f),
                    new Vector3(0.5f, 0.0f, 0.5f),
                    new Vector3(0.0f, 0.5f, 0.5f)
                };
                structure_name = "fcc Structure";
                break;
        }
        S = units.Length;
    }

    private void CreateAtoms()
    {
        atom = new GameObject[Lx*Ly*Lz*S];

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
                        var x_pos = (units[s].x + x * 1.0f) * Scale + Shift.x;
                        var y_pos = (units[s].y + y * 1.0f) * Scale + Shift.y;
                        var z_pos = (units[s].z + z * 1.0f) * Scale + Shift.z;
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
        foreach (var a in atom) Destroy(a);
        OnDestroy.Invoke();
    }

    private void CreateLines()
    {
        line = new GameObject[0];
        LineBeginNum = new int[0];
        LineEndNum = new int[0];
        var color = new Color(0.3f, 0.3f, 1.0f, 0.5f);
        for (int index1 = 0; index1 < atom.Length; ++index1)
        {
            for(int index2 = index1 + 1; index2 < atom.Length; ++index2)
            {
                Vector3 tmp1 = atom[index1].transform.position;
                Vector3 tmp2 = atom[index2].transform.position;
                Vector3 diff = tmp1 - tmp2;
                if(diff.magnitude <= BOND_LENGTH*Scale)
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

                    int[] LBN_copy = new int[LineBeginNum.Length + 1];
                    System.Array.Copy(LineBeginNum, LBN_copy, LineBeginNum.Length);
                    LBN_copy[LineBeginNum.Length] = index1;
                    LineBeginNum = LBN_copy;

                    int[] LEN_copy = new int[LineEndNum.Length + 1];
                    System.Array.Copy(LineEndNum, LEN_copy, LineEndNum.Length);
                    LEN_copy[LineEndNum.Length] = index2;
                    LineEndNum = LEN_copy;
                }
            }
        }
        //Debug.Log("line.Length = " + line.Length.ToString());
    }

    private void DestroyLines()
    {
        foreach (var l in line) Destroy(l);
        OnDestroy.Invoke();
    }

    public void ChangeAtoms()
    {
        DestroyAtoms();
        DestroyLines();
        SetAtoms();
        CreateAtoms();
        CreateLines();
        Debug.Log("num Atoms = " + atom.Length.ToString());
    }

    public void addLx()
    {
        Lx++;
        if (Lx > 10) Lx = 10;
    }

    public void minusLx()
    {
        Lx--;
        if (Lx <= 0) Lx = 1;
    }

    public void addLy()
    {
        Ly++;
        if (Ly > 10) Ly = 10;
    }

    public void minusLy()
    {
        Ly--;
        if (Ly <= 0) Ly = 1;
    }

    public void addLz()
    {
        Lz++;
        if (Lz > 10) Lz = 10;
    }

    public void minusLz()
    {
        Lz--;
        if (Lz <= 0) Lz = 1;
    }

    public void forward_structure_type()
    {
        structure_type++;
        structure_type %= 3;
    }

    public void backward_structure_type()
    {
        structure_type--;
        structure_type += 3;
        structure_type %= 3;
    }

    public void addBL()
    {
        BOND_LENGTH += 0.1f;
    }

    public void minusBL()
    {
        BOND_LENGTH -= 0.1f;
        if (BOND_LENGTH < 0.05f) BOND_LENGTH = 0.1f;
    }

}
