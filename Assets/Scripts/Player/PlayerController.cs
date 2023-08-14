using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    [SerializeField]
    private AudioSource abilitiesAudiosource;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private Joystick movementJoystick;

    public PlayerData playerData;

    [HideInInspector]
    public float aceleration;
    [HideInInspector]
    public Vector2 movementInput;

    private void Start()
    {
        mainCamera = Camera.main;

        movementJoystick = GameObject.Find("Movement Joystick").GetComponent<Joystick>();

        aceleration = playerData.speed;
    }

    private void FixedUpdate()
    {

#if UNITY_ANDROID || UNITY_EDITOR
        movementInput = new Vector2(movementJoystick.Horizontal, movementJoystick.Vertical);
#endif

#if UNITY_EDITOR
        movementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
#endif

        rb2d.velocity += movementInput * aceleration * Time.fixedDeltaTime;
    }


    public void SetAndPlaySFX(AudioClip audioClip)
    {
        abilitiesAudiosource.clip = audioClip;
        abilitiesAudiosource.Play();
    }

    public bool IsMoving()
    {
        return movementInput != Vector2.zero;
    }
}
