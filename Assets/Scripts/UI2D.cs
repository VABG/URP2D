using TMPro;
using UnityEngine;

public class UI2D : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    float timer = 0;
    public void ShowMessage(string message, float time)
    {
        text.text = message;
        timer = time;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                text.text = "";
            }
        }
    }
}
