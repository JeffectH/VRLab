using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Print : MonoBehaviour
{
    public GameObject _extruder;
    public Vector3[] points;
    public float step;
    public float speed=0.01f;
    private int stepPoint;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q)) 
        {
            
            Vector3.MoveTowards(new Vector3(points[stepPoint].x,_extruder.transform.position.y, points[stepPoint].z),
                new Vector3(points[stepPoint+1].x, _extruder.transform.position.y, points[stepPoint+1].z), speed);

            if (Vector3.Distance(new Vector3(points[stepPoint].x, _extruder.transform.position.y, points[stepPoint].z),
                new Vector3(points[stepPoint + 1].x, _extruder.transform.position.y, points[stepPoint + 1].z)) <= 1) 
            {
                stepPoint=+1;
                if (stepPoint == points.Length) 
                {
                    stepPoint = 0;
                    _extruder.transform.position = new Vector3(_extruder.transform.position.x,
                        _extruder.transform.position.y + step, _extruder.transform.position.z);
                }
            }
        }
    }
}
