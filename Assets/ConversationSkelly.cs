using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationSkelly : MonoBehaviour
{
    SkeletonAI skelly;

    [SerializeField] string[] responses;
    int responseIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        skelly = GetComponent<SkeletonAI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string GetResponse()
    {
        skelly.StopWalk();

        if (responses == null || responses.Length < 1) return "...";

        string response = responses[responseIndex];
        responseIndex++;
        if (responseIndex >= responses.Length) responseIndex = 0;
        return response;
    }
}
