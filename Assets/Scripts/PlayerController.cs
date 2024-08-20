using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{

    InputAction movement, turning, shoot;
    Animator animator;

    [SerializeField]
    GameObject shotPrefab;

    private void Awake()
    {
        InputActionsGame inputsGame = new InputActionsGame();
        movement = inputsGame.Game.Movement;
        turning = inputsGame.Game.Turning;
        shoot = inputsGame.Game.Shoot;
    }
    private void OnEnable()
    {
        movement.Enable();
        turning.Enable();
        shoot.Enable();
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        transform.position += new Vector3(Random.Range(-15, 15), 0, Random.Range(0, 15));
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) { return; }
        transform.Translate(0, 0, 
            movement.ReadValue<float>()*5*Time.deltaTime);
        transform.Rotate(0,
            turning.ReadValue<float>()*180*Time.deltaTime,0);

        if (movement.ReadValue<float>() != 0) 
        {
            animator.SetBool("walking", true);
        }
        else
        {
            animator.SetBool("walking", false);
        }

        if (shoot.IsPressed() && Time.frameCount % 60 == 0)
        {
            ShootBulletServerRpc();
        }

    }

    [ServerRpc]
    void ShootBulletServerRpc()
    {
        var bullet = Instantiate(shotPrefab);
        bullet.transform.position = transform.position + (transform.forward * 2);
        bullet.transform.rotation = transform.rotation;
        NetworkObject netObj = bullet.GetComponent<NetworkObject>();
        if (netObj != null)
        {
            netObj.Spawn();
            netObj.GetComponent<Rigidbody>().AddForce(bullet.transform.forward * 10, ForceMode.Impulse);
        }
    }
}
