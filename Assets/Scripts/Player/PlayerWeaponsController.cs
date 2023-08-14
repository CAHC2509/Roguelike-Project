using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerWeaponsController : MonoBehaviour
{
    [SerializeField]
    private Joystick aimJoystick;
    [SerializeField]
    private float triggerThreshold = 0.75f;
    [SerializeField]
    private Transform weaponParentObject;
    [SerializeField]
    private PlayerAimController playerAimController;
    [SerializeField]
    private List<GameObject> weaponsList;

    private WeaponController currentWeaponController;

    private GameObject canvas;
    private Transform controlsPanel;

    private GameObject takeWeaponButton;
    private Image takeWeaponImage;

    private GameObject switchWeaponsUI;
    private Image actualWeaponImage, secondaryWeaponImage;
    private Image[] weaponsImages = new Image[3];

    private GameObject weaponOnGround;

    private int currentWeaponIndex = 0;

    private float newWeaponScaleY = 1f;
    private float oldWeaponScaleY = 1f;

    private void Start()
    {
        // Find the cntrols panel object and cache it
        canvas = GameObject.Find("Canvas");
        controlsPanel = canvas.transform.Find("Controls Panel");

        aimJoystick = controlsPanel.Find("Aim Joystick").GetComponent<Joystick>();

        // Set the weapons icons for the UI switch weapon button
        switchWeaponsUI = controlsPanel.Find("Switch Weapons").gameObject;

        weaponsImages = switchWeaponsUI.GetComponentsInChildren<Image>();
        actualWeaponImage = weaponsImages[1];
        secondaryWeaponImage = weaponsImages[2];

        // Set the takeWeapon button and image
        takeWeaponButton = controlsPanel.Find("Take Weapon").gameObject;
        takeWeaponButton.SetActive(false);
        takeWeaponImage = takeWeaponButton.transform.Find("Image").GetComponent<Image>();

        // Instantiate the initial weapon
        EquipWeapon(currentWeaponIndex);
    }

    private void Update()
    {
        // Get joystick's axes
        float horizontal = aimJoystick.Horizontal;
        float vertical = aimJoystick.Vertical;

        // Check if the joystick has been extended enough to shoot
        bool shouldShoot = Mathf.Abs(horizontal) > triggerThreshold || Mathf.Abs(vertical) > triggerThreshold;

        // Notify the current weapon controller about shooting
        if (currentWeaponController != null)
        {
            currentWeaponController.HandleShootInput(shouldShoot);
        }
    }

    public void SwitchWeapon()
    {
        // Switch to the next weapon in the list
        currentWeaponIndex = (currentWeaponIndex + 1) % weaponsList.Count;
        EquipWeapon(currentWeaponIndex);
    }

    private void EquipWeapon(int index)
    {
        newWeaponScaleY = weaponsList[index].transform.localScale.y;

        // Destroy the current weapon if there's one
        if (currentWeaponController != null)
        {
            Destroy(currentWeaponController.gameObject);

            // Get the previous weapon Y scale
            oldWeaponScaleY = playerAimController.weapon.transform.localScale.y < 0 ? -1f : 1f;
        }

        // Instantiate and equip the new weapon as a child of the weaponParentObject
        GameObject weaponPrefab = weaponsList[index];
        GameObject weaponObject = Instantiate(weaponPrefab, weaponParentObject);
        playerAimController.weapon = weaponObject.transform;
        currentWeaponController = weaponObject.GetComponent<WeaponController>();

        // Adjust weapon's Y scale based on previous weapon
        weaponObject.transform.localScale = new Vector3(weaponObject.transform.localScale.x, newWeaponScaleY * oldWeaponScaleY, weaponObject.transform.localScale.z);

        SetWeaponsIcons();
    }

    public void TakeWeapon()
    {
        if (weaponOnGround != null)
        {
            // Get the prefab from the WeaponData and store it
            GameObject weaponPrefab = weaponOnGround.GetComponent<WeaponController>().weaponData.weaponPrefab;

            // Destroy the weapon on the ground and spawn the current weapon
            Destroy(weaponOnGround);

            // Spawn the current weapon on the ground
            Instantiate(weaponsList[currentWeaponIndex], transform.position, Quaternion.identity);

            // Replace the current weapon in the inventory for the ground weapon
            weaponsList[currentWeaponIndex] = weaponPrefab;

            // Equip the new weapon to updatae the sprites an weapon in hand
            EquipWeapon(currentWeaponIndex);
        }
    }

    private void SetWeaponsIcons()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weaponsList.Count)
        {
            // Set the sprite for the current weapon in hand
            actualWeaponImage.sprite = weaponsList[currentWeaponIndex].GetComponent<WeaponController>().weaponData.weaponSprite;

            // Calculate the index of the other weapon
            int otherWeaponIndex = (currentWeaponIndex + 1) % weaponsList.Count;

            // Set the sprite for the secondary weapon
            secondaryWeaponImage.sprite = weaponsList[otherWeaponIndex].GetComponent<WeaponController>().weaponData.weaponSprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionedObject = collision.gameObject;

        if (collisionedObject.CompareTag("Weapon"))
        {
            takeWeaponButton.SetActive(true);
            weaponOnGround = collisionedObject; // Store the weapon on the ground
            takeWeaponImage.sprite = collisionedObject.GetComponent<WeaponController>().weaponData.weaponSprite; // Set the button sprite based onthe ground weapon
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject uncollisionedObject = collision.gameObject;

        if (uncollisionedObject.CompareTag("Weapon"))
        {
            takeWeaponButton.SetActive(false);
            weaponOnGround = null; // Clear the stored weapon reference
        }
    }
}
