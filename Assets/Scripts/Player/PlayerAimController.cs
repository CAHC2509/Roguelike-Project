using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimController : MonoBehaviour
{
    [SerializeField]
    private Joystick aimJoystick;
    [SerializeField]
    private Transform centerObject;

    public Transform weapon;

    private float lastAngle = 0f;
    private float horizontal, vertical;
    private bool joystickMoved = false;

    private Vector3 originalWeaponScale;

    void Start()
    {
        aimJoystick = GameObject.Find("Aim Joystick").GetComponent<Joystick>();

        // Store the original weapon scale
        originalWeaponScale = weapon.localScale;
    }

    void Update()
    {
        // Get joystick's axes
        horizontal = aimJoystick.Horizontal;
        vertical = aimJoystick.Vertical;

        if (horizontal != 0 || vertical != 0)
        {
            joystickMoved = true;

            // Arrow movement
            float angle = Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
            Vector3 rotation = new Vector3(0, 0, angle);
            transform.position = centerObject.position + Quaternion.Euler(rotation) * new Vector3(1, 0, 0);

            // Arrow rotation
            transform.rotation = Quaternion.Euler(0, 0, angle - 90);

            // Scale weapon in Y based on the joystick's X value
            float weaponScaleY = horizontal < 0 ? -1f : 1f;
            weapon.localScale = new Vector3(weapon.localScale.x, weaponScaleY * originalWeaponScale.y, weapon.localScale.z);

            lastAngle = angle;
        }
        else if (joystickMoved)
        {
            // Keep the last state of the arrow if the joystick is not activated
            Vector3 rotation = new Vector3(0, 0, lastAngle);
            transform.position = centerObject.position + Quaternion.Euler(rotation) * new Vector3(1, 0, 0);
        }
    }
}
