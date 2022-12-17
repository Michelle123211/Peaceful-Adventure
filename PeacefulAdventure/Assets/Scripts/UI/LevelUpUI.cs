using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AppearHideComponent))]
public class LevelUpUI : MonoBehaviour
{

    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        GetComponent<AppearHideComponent>().Do();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        GetComponent<AppearHideComponent>().Undo();
    }

    public void IncreaseAttackDamage() {
        float newDamage = PlayerState.Instance.attackDamage.BaseValue * 1.1f;
        PlayerState.Instance.attackDamage.ChangeBaseValue(newDamage);
        Debug.Log("Attack damage increased.");
        Close();
    }

    private void IncreaseAttackDamage(InputAction.CallbackContext context)
        => IncreaseAttackDamage();

    public void IncreaseAttackSpeed() {
        float newCooldown = PlayerState.Instance.attackCooldown.BaseValue * 0.9f;
        PlayerState.Instance.attackCooldown.ChangeBaseValue(newCooldown);
        Debug.Log("Attack speed increased.");
        Close();
    }
    private void IncreaseAttackSpeed(InputAction.CallbackContext context)
        => IncreaseAttackSpeed();

    public void IncreaseMaxHealth() {
        int newMaxHealth = (int)(PlayerState.Instance.MaxHealth * 1.1f);
        PlayerState.Instance.UpdateMaxHealth(newMaxHealth);
        Debug.Log("Max health increased.");
        Close();
    }
    private void IncreaseMaxHealth(InputAction.CallbackContext context)
        => IncreaseMaxHealth();

    private void OnEnable() {
        // register input callbacks
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed += IncreaseAttackDamage;
        PlayerBehaviour.playerInputActions.UI.Action2_I.performed += IncreaseAttackSpeed;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed += IncreaseMaxHealth;
    }

    private void OnDisable() {
        // unregister input callbacks
        PlayerBehaviour.playerInputActions.UI.Action1_J.performed -= IncreaseAttackDamage;
        PlayerBehaviour.playerInputActions.UI.Action2_I.performed -= IncreaseAttackSpeed;
        PlayerBehaviour.playerInputActions.UI.Action3_L.performed -= IncreaseMaxHealth;
    }
}
