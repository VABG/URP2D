using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegIK : MonoBehaviour
{
    [SerializeField] float maxDistance = 2.3f;
    [SerializeField] Transform target;
    [SerializeField] Transform middle;
    [SerializeField] Transform fwd;
    [SerializeField] Transform bwd;
    Vector3 targetPos;
    float lerpSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        // If too far, just reset bro
        if ((transform.position - target.position).magnitude > maxDistance)
        {
            float dFwd = Vector3.Distance(target.position, fwd.position);
            float dBwd = Vector3.Distance(target.position, bwd.position);
            targetPos = dFwd < dBwd ? bwd.position : fwd.position;
            lerpSpeed = dFwd < dBwd ? 40 : 10;
        }

        target.position = Vector3.Lerp(target.position, targetPos, lerpSpeed * Time.deltaTime);
    }
}
