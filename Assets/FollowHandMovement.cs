using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHandMovement : MonoBehaviour
{
    [SerializeField] float handMoveMagnitute = .2f;
    [SerializeField] float lerpSpeed = 20.0f;
    Vector3 startPosition;
    Vector3 targetPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * lerpSpeed);
    }

    public void RecieveMousePosition(Vector2 pos)
    {
        targetPosition = startPosition + new Vector3(pos.x, 0, pos.y)* handMoveMagnitute;
    }
}
