using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadLookAt : MonoBehaviour
{
    [SerializeField] Transform[] eyes;
    [SerializeField] Transform target;
    Quaternion start;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion at = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Lerp(start, at, 1.0f);

        foreach (Transform t in eyes)
        {
            t.LookAt(target, Vector3.up);
        }
    }
}
