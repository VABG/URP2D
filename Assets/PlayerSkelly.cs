using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkelly : MonoBehaviour
{
    int direction = 0;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5.5f;
    ConversationSkelly convoBoy;
    List<ConversationSkelly> convoBoys = new List<ConversationSkelly>();
    [SerializeField]
    UI2D ui;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ui = FindObjectOfType<UI2D>();
        if (ui == null) Debug.LogError("NO 'UI2D' FOUND!");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 input = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) input += Vector2.up;
        if (Input.GetKey(KeyCode.A)) input += Vector2.left;
        if (Input.GetKey(KeyCode.S)) input += Vector2.down;
        if (Input.GetKey(KeyCode.D)) input += Vector2.right;
        SetInputDirection(input);

        if (Input.GetKeyDown(KeyCode.E)) TalkTo();
    }

    void SetInputDirection(Vector2 input)
    {
        input.Normalize();

        rb.velocity = input * moveSpeed;

        if (input == Vector2.zero)
        {
            anim.SetFloat("speed", 0);
        }
        else
        {
            anim.SetFloat("speed", moveSpeed * .5f);
            int direction = 0;
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0) direction = 1;
                else direction = 3;
            }
            else
            {
                if (input.y > 0) direction = 0;
                else direction = 2;
            }
            anim.SetInteger("direction", direction);
        }
    }

    private void TalkTo()
    {
        if (convoBoys.Count < 1) return;
        ConversationSkelly c;
        float closestDistance = float.MaxValue;
        int closestIndex = 0;
        for (int i = 0; i < convoBoys.Count; i++)
        {
            float d = Vector3.Distance(transform.position, convoBoys[i].transform.position);
            if (d < closestDistance)
            {
                closestDistance = d;
                closestIndex = i;
            }
        }

        ui.ShowMessage(convoBoys[closestIndex].GetResponse(), 1.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ConversationSkelly sk;
        if (collision.TryGetComponent<ConversationSkelly>( out sk))
        {
            convoBoys.Add(sk);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ConversationSkelly sk;
        if (collision.TryGetComponent<ConversationSkelly>(out sk))
        {
            convoBoys.Remove(sk);
        }
    }
}
