using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



/// <summary>
/// Base pawn defines a gameobject that has a movement component, and a health component
/// All pawns have a movement component (animator root motion) and a health component
/// 
/// For now all pawns will be player humanoid pawns
/// </summary>
public class HumanoidPawn : MonoBehaviour
{
    public Animator AnimationController { get; private set; }
    public CharacterStatus status { get; private set; }

    Vector2 _movement = Vector2.zero;
    bool sprinting = false;
    public float sprintBlendingTime = 0.5f;
    Coroutine sprintBlending;

    float maxSpeed = 0.67f;

    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
    }

    public void run(Vector2 input)
    {
        _movement = input.normalized;

        UpdateAnimator();
    }

    public void toggleSprint()
    {
        sprinting = !sprinting;
        Debug.Log("Sprint toggled");
        sprintBlending = StartCoroutine("SprintToggleTimer");

        
    }

    private void UpdateAnimator()
    {
        Vector2 actualMovement;


        actualMovement = Vector2.ClampMagnitude(_movement, maxSpeed);

        AnimationController.SetFloat("Right", actualMovement.x);
        AnimationController.SetFloat("Forward", actualMovement.y);
        Debug.Log("updating animator with input: " + actualMovement);
    }

    IEnumerator SprintToggleTimer()
    {
        float timeElapsed = 0f;
        Debug.Log("Starting timer coroutine");

        while (sprintBlendingTime > timeElapsed)
        {
            timeElapsed += Time.deltaTime;
            maxSpeed = Mathf.Lerp(0.67f, 1, timeElapsed / sprintBlendingTime);
            Debug.Log("Ticking coroutine, maxspeed is: " + maxSpeed);
            UpdateAnimator();
            yield  return null;
        }
        Debug.Log("Exiting timer coroutine");
        UpdateAnimator();
    }
}
