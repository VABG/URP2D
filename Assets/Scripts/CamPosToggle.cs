using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamPosToggle : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    
    [SerializeField] Transform alternative;

    bool atStart = true;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (atStart)
            {
                transform.position = alternative.position;
                transform.rotation = alternative.rotation;                
            }
            else
            {
                transform.position = startPosition;
                transform.rotation = startRotation;                
            }
            atStart = !atStart;
        }
    }
}
