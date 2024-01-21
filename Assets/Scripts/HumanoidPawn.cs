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
    Vector2 _targetMovement = Vector2.zero;

    float currSpeed
    {
        get { return _movement.magnitude; }
    }
    float targetSpeed
    {
        get { return _targetMovement.magnitude; }
    }

    Coroutine accelBlending;
    bool isAccelBlending = false;

    public float accelTime = 1.5f; //How many seconds it takes to accel from 0 to max speed

    bool sprinting = false;
    bool isSprinting
    {
        get { return currSpeed > 0.67f; }
    }

    public float sprintBlendingTime = 0.5f; //How long it takes for the max speed percent to increase to 1 when initiating a sprint
    
    Coroutine sprintBlending;
    bool isSprintBlending = false;
    
    float _sprintBlendingTimer = 0f;
    float sprintBlendingTimer { 
        get { return _sprintBlendingTimer; } 
        set { _sprintBlendingTimer = Mathf.Clamp(value, 0, sprintBlendingTime);  }
    }

    float _maxSpeedPercent = 1f;
    float maxSpeedPercent
    {
        get { return _maxSpeedPercent; }
        set { _maxSpeedPercent = Mathf.Clamp(value, 0, 1);  }
    }


    bool crouched = false;


    private void Awake()
    {
        AnimationController = GetComponent<Animator>();
        status = GetComponent<CharacterStatus>();
    }

    public void run(Vector2 input)
    {
        _targetMovement = input.normalized;
        if (!isAccelBlending) { accelBlending = StartCoroutine("AccelerationBlending"); }

        UpdateAnimator();
    }

    public void toggleSprint(bool isButtonHeld)
    {
        if (!isButtonHeld && crouched) { 
            toggleCrouch();
            return; } // early out

        if (!isButtonHeld && !sprinting)
        {
            return; // early out
        }

        sprinting = !sprinting;
        Debug.Log("Sprinting is :" + sprinting + ", button is held is: " + isButtonHeld);
        //if (!isSprintBlending) { sprintBlending = StartCoroutine("SprintToggleTimer"); }
        if (sprinting && crouched) { crouched = false; }

        UpdateAnimator();
    }

    public void toggleCrouch()
    {
        crouched = !crouched;
        sprinting = false;

        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        Vector2 actualMovement;


        actualMovement = Vector2.ClampMagnitude(_movement, maxSpeedPercent);

        AnimationController.SetFloat("Right", actualMovement.x);
        AnimationController.SetFloat("Forward", actualMovement.y);

        AnimationController.SetBool("Crouching", crouched);
        AnimationController.SetBool("Sprinting", sprinting);
        //Debug.Log("updating animator with input: " + actualMovement);
    }

    IEnumerator SprintToggleTimer()
    {
        isSprintBlending = true;

        while ((sprinting && sprintBlendingTimer < 1 ) || ( !sprinting && sprintBlendingTimer > 0))
        {
            if (sprinting)
            {
                sprintBlendingTimer += Time.deltaTime;
                maxSpeedPercent = Mathf.Lerp(0.67f, 1, sprintBlendingTimer / sprintBlendingTime);
            } else
            {
                sprintBlendingTimer -= Time.deltaTime;
                maxSpeedPercent = Mathf.Lerp(0.67f, 1, sprintBlendingTimer / sprintBlendingTime);
            }

            UpdateAnimator();
            yield return null;
        }
        
        UpdateAnimator();

        isSprintBlending = false;
    }

    IEnumerator AccelerationBlending()
    {
        isAccelBlending = true;

        while (_movement != _targetMovement)
        {
            Vector2 accelVec = (_targetMovement - _movement).normalized;
            Vector2 newMovement = _movement + (accelVec * Time.deltaTime * (1 / accelTime));

            if (Vector2.Distance(newMovement, _targetMovement) > Vector2.Distance(_movement, _targetMovement))
            {
                _movement = _targetMovement;
            } else
            {
                _movement = newMovement;
            }

            UpdateAnimator();

            yield return null;
        }

        //Debug.Log("Ending accelblending");
        UpdateAnimator();

        isAccelBlending = false;
    }
}
