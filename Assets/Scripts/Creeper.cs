using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creeper : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] float forwardTime = 10;
    [SerializeField] float backwardTime = .5f;
    bool fwd = true;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = fwd ? backwardTime : forwardTime;
            fwd = !fwd;            
        }

        float lerp = fwd ? 1- (timer / forwardTime) : timer / backwardTime;
        lerp = Mathf.SmoothStep(0, 1, lerp);

        transform.position = Vector3.Lerp(start.position, end.position, lerp);
        transform.rotation = Quaternion.Lerp(start.rotation, end.rotation, lerp);
    }
}
