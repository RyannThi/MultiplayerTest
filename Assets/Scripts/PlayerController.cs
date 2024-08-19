using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    InputAction movement, turning;
    private void Awake()
    {
        InputActionsGame inputsGame = new InputActionsGame();
        movement = inputsGame.Game.Movement;
        turning = inputsGame.Game.Turning;
    }
    private void OnEnable()
    {
        movement.Enable();
        turning.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 
            movement.ReadValue<float>()*5*Time.deltaTime);
        transform.Rotate(0,
            turning.ReadValue<float>()*180*Time.deltaTime,0);
    }
}
