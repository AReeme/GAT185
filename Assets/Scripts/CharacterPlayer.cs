using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class CharacterPlayer : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Animator animator;
    [SerializeField] private InputRouter inputRouter;
    [SerializeField] private Inventory inventory;

    CharacterController characterController;
    Vector2 inputAxis;

    Camera mainCamera;
    Vector3 velocity = Vector3.zero;
    float inAirTime = 0;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;

        inputRouter.jumpEvent += OnJump;
        inputRouter.moveEvent += OnMove;
        inputRouter.fireEvent += OnFire;
        inputRouter.nextItemEvent += OnNextItem;
        inputRouter.fireStopEvent += OnFireStop;
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = inputAxis.x;
        direction.z = inputAxis.y;

        direction = mainCamera.transform.TransformDirection(direction);

        if(characterController.isGrounded)
        {
            velocity.x = direction.x * playerData.speed;
            velocity.z = direction.z * playerData.speed;
            inAirTime = 0;
        }
        else
        {
            inAirTime += Time.deltaTime;
            velocity.x = direction.x * playerData.speed/2;
            velocity.z = direction.z * playerData.speed/2;
            velocity.y += playerData.gravity * Time.deltaTime;
        }

        characterController.Move(velocity * Time.deltaTime);
        Vector3 look = direction;
        look.y = 0;
        if(look.magnitude > 0) 
        { 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(look), playerData.turnRate * Time.deltaTime);
        }

        // set animator parameters
        animator.SetFloat("speed", characterController.velocity.magnitude);
        animator.SetFloat("velocityY", characterController.velocity.y);
        animator.SetFloat("inAirTime", inAirTime);
        animator.SetBool("isGrounded", characterController.isGrounded);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
        {
            return;
        }

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3)
        {
            return;
        }

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * playerData.hitForce;
    }

    public void OnJump()
    {
        if (characterController.isGrounded)
        {
            animator.SetTrigger("jump");
            velocity.y = Mathf.Sqrt(playerData.jumpHeight * -3 * playerData.gravity);
        }
    }

    public void OnFire()
    {
        inventory.Use();
    }

    public void OnFireStop()
    {
        inventory.StopUse();
    }

    public void OnMove(Vector2 axis)
    {
        inputAxis = axis;
    }
    
    public void OnAnimEventItemUse()
    {
        inventory.OnAnimEventItemUse();
    }

    public void OnLeftFootSpawnFunction(GameObject go)
    {
        Transform bone = animator.GetBoneTransform(HumanBodyBones.LeftFoot);
        Instantiate(go, bone.position, bone.rotation);
    }

    public void OnLRightFootSpawnFunction(GameObject go)
    {
        Transform bone1 = animator.GetBoneTransform(HumanBodyBones.RightFoot);
        Instantiate(go, bone1.position, bone1.rotation);
    }


    private void OnNextItem()
    {
        inventory.EquipNextItem();
    }

}
