using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class gestionOuverture : MonoBehaviour
{
    // Angle Door
    public bool changeStat = false;
    public bool isClose = false;
    private float angle;
    private Vector3 vector;
    // Click Listener
    private Camera camera;
    void Start()
    {
        this.angle = this.transform.localRotation.y;
        this.vector = this.transform.localPosition;

        this.camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (this.changeStat)
        {
            this.angle = changeAngle();
            Vector3 vector = changeVector(this.angle);

            this.transform.localRotation = Quaternion.Euler(this.transform.localRotation.x, this.angle, this.transform.localRotation.z);
            this.transform.localPosition = vector;
        }
    }
    private void OnMouseDown()
    {
        this.changeStat = true;
    }
    void click()
    {
        Vector3 pos = Vector3.zero;
        if (Input.GetMouseButtonDown(0))
        {
            pos = Input.mousePosition;
        }
        if (Input.touches.Length > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            pos = Input.touches[0].position;
        }
        if (pos == Vector3.zero) // Isnt click
            return;

        Ray r = camera.ScreenPointToRay(pos);
        RaycastHit rch;
        if (Physics.Raycast(r, out rch, 100f, 0x100f))
        {
            this.changeStat = true;
        }
    }
    Vector3 changeVector(float angle)
    {
        Vector3 vector = new Vector3();
        vector.y = this.vector.y;

        if (angle == 90.0f)
        {
            // Position : [5;0.01;0]
            // Rotation : [0;90;0]
            vector.x = this.vector.x - 5f;
            vector.z = this.vector.z - 5f;
        }
        else
        {
            // Position : [0;0.01;5]
            // Rotation : [0;0;0]
            vector.x = this.vector.x;
            vector.z = this.vector.z;
        }

        return vector;
    }
    float changeAngle()
    {
        float angle = this.angle;

        if (this.isClose)
        {
            angle = 90.0f;
            change();
        }
        else
        {
            change();
            angle = 0;
        }

        return angle;
    }
    void change()
    {
        this.isClose = !this.isClose;
        this.changeStat = false;
    }
}