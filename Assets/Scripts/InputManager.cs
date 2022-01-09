using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerControls playerControls;
    PlayerController playerController;
    AnimatorManager animatorManager;
    MouseHandler mouseHandler;

    public Vector2 movementInput;
    public Vector2 cameraInput;
    public Vector2 mouseInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool itemInHand;

    public bool b_Input;
    public bool jump_Input;
    public bool fire_Input;
    public float itemScroll_Input;
    public bool PickUp_Input;



    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        mouseHandler = FindObjectOfType<MouseHandler>();
        playerController = GetComponent<PlayerController>();
    }
    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerMovement.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();

            playerControls.PlayerMovement.MouseX.performed += i => mouseInput.x = i.ReadValue<float>();
            playerControls.PlayerMovement.MouseY.performed += i => mouseInput.y = i.ReadValue<float>();

            playerControls.PlayerActions.B.performed += i => b_Input = true;
            playerControls.PlayerActions.B.canceled += i => b_Input = false;

            playerControls.PlayerActions.Jump.performed += i => jump_Input = true;

            playerControls.PlayerActions.Fire.performed += i => fire_Input = true;
            playerControls.PlayerActions.ItemScroll.performed += i => itemScroll_Input = i.ReadValue<float>();
            playerControls.PlayerActions.PickUp.performed += i => PickUp_Input = true;

        }

        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void HandleAllInputs()
    {
        mouseHandler.ReceiveInput(mouseInput);

        HandleMovementInput();
        HandleSprintingInput();
        HandleJumpInput();
        HandleFireInput();
        HandleItemScroll();
        HandlePickUp();
    }

    private void HandleItemScroll()
    {


        if (itemScroll_Input > 0f)
        {
            itemInHand = true;
            playerController.ScrollItems(1);
        }
        else if (itemScroll_Input < 0f)
        {
            itemInHand = true;
            playerController.ScrollItems(-1);
        }
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputY = cameraInput.y;
        cameraInputX = cameraInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerController.isSprinting);
    }

    private void HandleSprintingInput()
    {
        if (b_Input && moveAmount > 0.5f)
        {
            playerController.isSprinting = true;
        }
        else
        {
            playerController.isSprinting = false;
        }
    }

    private void HandleJumpInput()
    {

        if (jump_Input)
        {
            jump_Input = false;  
            playerController.HandleJumping();
        }
    }

    private void HandleFireInput()
    {

        if (fire_Input && itemInHand)
        {
            fire_Input = false;
            GetComponentInChildren<Item>().Use();
        }
    }

    private void HandlePickUp()
    {

        if (PickUp_Input)
        {
            PickUp_Input = false;
        }
    }
}
