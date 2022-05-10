using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum DrinkState
{
    Idle,
    MoveHandUp,
    RotatingDrink,
    Drinking,
    MoveHandDown,
}

public class Drink : MonoBehaviour
{
    [SerializeField] Transform mouthTarget;
    [SerializeField] float timeToMoveHand = 1.5f;
    [SerializeField] float rotateDrinkTime = .35f;
    [SerializeField] float drinkTime = 1.0f;
    [SerializeField] float drinkIntervals = 10.0f;
    [SerializeField] Transform ikTarget;
    Vector3 startPos;
    Quaternion startRot;
    Vector3 targetPos;
    Quaternion targetRot;
    [SerializeField] float lerpSpeed = 5;

    float timer;
    DrinkState state = DrinkState.Idle;
    // Start is called before the first frame update
    void Start()
    {
        timer = drinkIntervals;
        startPos = ikTarget.position;
        targetPos = startPos;
        startRot = ikTarget.rotation;
        targetRot = startRot;
    }

    // Update is called once per frame
    void Update()
    {
        StateSwitch();

        ikTarget.position = Vector3.Lerp(ikTarget.position, targetPos, Time.deltaTime * lerpSpeed);
        ikTarget.rotation = Quaternion.Lerp(ikTarget.rotation, targetRot, Time.deltaTime * lerpSpeed);
    }

    void StateSwitch()
    {
        timer -= Time.deltaTime;
        switch (state)
        {
            case DrinkState.Idle:
                if (timer <= 0)
                {
                    state = DrinkState.MoveHandUp;
                    timer = timeToMoveHand;
                }
                break;
            case DrinkState.MoveHandUp:
                float lerp = 1 - (timer / timeToMoveHand);
                targetPos = Vector3.Lerp(startPos, mouthTarget.position, lerp);

                if (timer <= 0)
                {
                    timer = rotateDrinkTime;
                    state = DrinkState.RotatingDrink;
                }
                break;
            case DrinkState.RotatingDrink:
                lerp = 1 - (timer / rotateDrinkTime);
                targetRot = Quaternion.Slerp(startRot, mouthTarget.rotation, lerp);

                if (timer <= 0)
                {
                    timer = drinkTime;
                    state = DrinkState.Drinking;
                }
                break;

            case DrinkState.Drinking:
                targetPos = mouthTarget.position;
                targetRot = mouthTarget.rotation;

                if (timer <= 0)
                {
                     timer = timeToMoveHand;
                    state = DrinkState.MoveHandDown;
                }
                break;

            case DrinkState.MoveHandDown:
                lerp = 1 - (timer / timeToMoveHand);
                targetPos = Vector3.Lerp(mouthTarget.position, startPos, lerp);

                // faster start of rotation
                float lerp2 = 1 - lerp;
                lerp2 *= lerp2 * lerp2;
                lerp2 = 1 - lerp2;
                targetRot = Quaternion.Lerp(mouthTarget.rotation, startRot, lerp2);

                if (timer <= 0)
                {
                    timer = drinkIntervals;
                    state = DrinkState.Idle;
                }
                break;
        }
    }
}
