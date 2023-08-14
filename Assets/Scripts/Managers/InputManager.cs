using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Take weapon")]
    [SerializeField]
    private InputActionReference takeWeaponAction;
    [Header("Switch weapons")]
    [SerializeField]
    public InputActionReference switchWeaponAction;
    [SerializeField]
    private PlayerWeaponsController playerWeaponsController;
    [Header("Normal ability")]
    [SerializeField]
    public InputActionReference normalAbilityAction;
    [SerializeField]
    private NormalAbilityHolder normalAbilityHolder;
    [Header("Ultimate ability")]
    [SerializeField]
    public InputActionReference ultimateAbilityAction;
    [SerializeField]
    private UltimateAbilityHolder ultimateAbilityHolder;    

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        playerWeaponsController = player.GetComponent<PlayerWeaponsController>();
        normalAbilityHolder = player.GetComponent<NormalAbilityHolder>();
        ultimateAbilityHolder = player.GetComponent<UltimateAbilityHolder>();
    }

    private void OnEnable()
    {
        takeWeaponAction.action.Enable();
        takeWeaponAction.action.performed += OnTakeWeaponButtonPressed;

        switchWeaponAction.action.Enable();
        switchWeaponAction.action.performed += OnSwitchWeaponButtonPressed;

        normalAbilityAction.action.Enable();
        normalAbilityAction.action.performed += OnNormalAbilityButtonPressed;

        ultimateAbilityAction.action.Enable();
        ultimateAbilityAction.action.performed += OnUltimateAbilityButtonPressed;
    }

    private void OnDisable()
    {
        takeWeaponAction.action.Disable();
        takeWeaponAction.action.performed -= OnTakeWeaponButtonPressed;

        switchWeaponAction.action.Disable();
        switchWeaponAction.action.performed -= OnSwitchWeaponButtonPressed;

        normalAbilityAction.action.Disable();
        normalAbilityAction.action.performed -= OnNormalAbilityButtonPressed;

        ultimateAbilityAction.action.Disable();
        ultimateAbilityAction.action.performed -= OnUltimateAbilityButtonPressed;
    }

    private void OnTakeWeaponButtonPressed(InputAction.CallbackContext context)
    {
        playerWeaponsController.TakeWeapon();
    }

    private void OnSwitchWeaponButtonPressed(InputAction.CallbackContext context)
    {
        playerWeaponsController.SwitchWeapon();
    }

    private void OnNormalAbilityButtonPressed(InputAction.CallbackContext context)
    {
        normalAbilityHolder.activateNormalAbility = true;
    }

    private void OnUltimateAbilityButtonPressed(InputAction.CallbackContext context)
    {
        ultimateAbilityHolder.activateUltimateAbility = true;
    }
}