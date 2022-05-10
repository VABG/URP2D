using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKSolver3D : MonoBehaviour
{
    [SerializeField] Transform[] bones;
    [SerializeField] float tolerance = .2f;
    [SerializeField] Transform target;
    [SerializeField] Transform poleVector;
    [SerializeField] int maxIterations = 10;
    int bCount;
    Vector3[] b;
    Vector3 t;
    float[] bL;
    float bTotL;

    // Start is called before the first frame update
    void Start()
    {
        bTotL = GetAllBonesLength();
        b = new Vector3[bones.Length];
        bCount = bones.Length;
        for (int i = 0; i < bones.Length; i++)
        {
            b[i] = bones[i].position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        b[0] = bones[0].position;
        SolveIK();
        RotateBonesToIKPoints();
    }

    float GetAllBonesLength()
    {
        float l = 0;
        bL = new float[bones.Length];
        for (int i = 0; i < bones.Length-1; i++)
        {
            float bL = Vector3.Magnitude(bones[i + 1].position - bones[i].position);
            l += bL;
            this.bL[i] = bL;
        }
        return l;
    }

    /// <summary>
    /// FABRIK method
    /// </summary>
    void SolveIK()
    {
        if (target == null || bones == null || bones.Length < 1) return;
        t = target.position;

        float dist = Vector3.Magnitude(t- b[0]);
        // Out of range
        if (dist > bTotL)
        {
            for (int i = 0; i < bCount-1; i++)
            {
                float r = Vector3.Magnitude(t - b[i]);
                float l = bL[i] / r;
                b[i+1] = (1.0f-l) * b[i] + l * t;
            }
        }
        // In range
        else
        {
            Vector3 b0 = b[0];
            float dif = Vector3.Magnitude(b[bCount - 1] - t);
            int iterations = 0;
            while(dif > tolerance || iterations < maxIterations)
            {
                // Forward reaching
                b[bCount - 1] = t;
                for (int i = bCount-2; i >= 0; i--)
                {
                    float r = Vector3.Magnitude(b[i + 1] - b[i]);
                    float l = bL[i] / r;
                    b[i] = (1.0f - l) * b[i + 1] + l * b[i];
                }

                b[0] = b0;
                // Backward reaching
                for (int i = 0; i < bCount-1; i++)
                {
                    float r = Vector3.Magnitude(b[i + 1] - b[i]);
                    float l = bL[i] / r;
                    b[i + 1] = (1.0f - l) * b[i] + l * b[i + 1];
                }
                dif = Vector3.Magnitude(b[bCount - 1] - t);
                iterations++;
            }
        }

        if (poleVector != null)
        {
            for (int i = 1; i < bCount-1; i++)
            {
                Plane p = new Plane(b[i + 1] - b[i - 1], b[i - 1]);
                Vector3 projPole = p.ClosestPointOnPlane(poleVector.position);
                Vector3 projBone = p.ClosestPointOnPlane(b[i]);
                float angle = Vector3.SignedAngle(projBone - b[i - 1], projPole - b[i - 1], p.normal);
                b[i] = Quaternion.AngleAxis(angle, p.normal) * (b[i] - b[i - 1]) + b[i - 1];
            }
        }
    }

    void RotateBonesToIKPoints()
    {
        // Fix look direction of all bones!
        for (int i = 0; i < bones.Length - 1; i++)
        {
            
            bones[i].rotation = Quaternion.LookRotation((b[i + 1] - b[i]).normalized);
        }
    }
}
