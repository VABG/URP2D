using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScreen : MonoBehaviour
{
    [SerializeField] Transform body;
    Quaternion bodyInitialRotation;
    [SerializeField] Transform head;
    [SerializeField] Transform[] eyes;
    Quaternion headInitialRotation;
    Vector3 lookTarget;
    // Start is called before the first frame update
    void Start()
    {
        bodyInitialRotation = body.rotation;
        headInitialRotation = head.rotation;

        lookTarget = head.position + head.forward;
    }

    // Update is called once per frame
    void Update()
    {
        // Find target
        Vector3 direction = (lookTarget - head.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);


        for (int i = 0; i < eyes.Length; i++)
        {
            eyes[i].rotation = Quaternion.Slerp(eyes[i].rotation, targetRotation, 25.0f*Time.deltaTime);
        }

        // Lerp body a bit to look slightly at the target.
        Vector3 direction2D = new Vector3(direction.x, 0, direction.z).normalized;
        Quaternion yRotationTarget = Quaternion.LookRotation(direction2D, Vector3.up);
        Quaternion yRotationTargerReduced = Quaternion.Lerp(bodyInitialRotation, yRotationTarget, .25f);
        body.rotation = Quaternion.Slerp(body.rotation, yRotationTargerReduced, Time.deltaTime * 2.0f);

        Quaternion targetReduced = Quaternion.Lerp(headInitialRotation, targetRotation, .5f);
        head.rotation = Quaternion.Slerp(head.rotation, targetReduced, Time.deltaTime * 4.0f);



        // Lerp head a bit more to look at target (x and y)
    }
    // Get target from other code!!!
    public void SetLookTarget(Vector3 position)
    {
        lookTarget = position;
    }
}
