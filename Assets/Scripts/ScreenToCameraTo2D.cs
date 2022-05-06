using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class ScreenToCameraTo2D : MonoBehaviour
{
    [SerializeField] Camera Cam3D;
    [SerializeField] Camera Cam2D;
    [SerializeField] GameObject objectToMove;
    [SerializeField] LookAtScreen onlooker;
    PixelPerfectCamera ppCam;
    [SerializeField] LayerMask mask;
    [SerializeField] FollowHandMovement handTarget;

    // Start is called before the first frame update
    void Start()
    {
        ppCam = Cam2D.GetComponent<PixelPerfectCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray r = Cam3D.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out RaycastHit hit, 5, mask))
            {
                //Debug.Log("First hit successful at: " + hit.textureCoord.ToString());
                Vector2 hitUV = hit.textureCoord;
                hitUV *= 2;
                hitUV -= Vector2.one;
                // Send to hand target
                handTarget.RecieveMousePosition(hitUV);

                Vector3 pos = hitUV * ppCam.orthographicSize;
                objectToMove.transform.position = pos;
                onlooker.SetLookTarget(hit.point);
            }
        }
    }


}
