using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelUpUI : MonoBehaviour
{
    private bool initialized = false;
    public void Open() {
        PlayerBehaviour.playerInputActions.Player.Disable();
        PlayerBehaviour.playerInputActions.UI.Enable();
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        gameObject.TweenAwareDisable();
    }

    public void IncreaseAttackDamage() {
        if (initialized) {
            float newDamage = PlayerState.Instance.attackDamage.BaseValue * 1.1f;
            PlayerState.Instance.attackDamage.ChangeBaseValue(newDamage);
            Debug.Log("Attack damage increased.");
            Close();
        }
    }
    private void IncreaseAttackDamage(InputAction.CallbackContext context)
        => IncreaseAttackDamage();

    public void IncreaseAttackSpeed() {
        if (initialized) {
            float newCooldown = PlayerState.Instance.attackCooldown.BaseValue * 0.9f;
            PlayerState.Instance.attackCooldown.ChangeBaseValue(newCooldown);
            Debug.Log("Attack speed increased.");
            Close();
        }
    }
    private void IncreaseAttackSpeed(InputAction.CallbackContext context)
        => IncreaseAttackSpeed();

    public void IncreaseMaxHealth() {
        if (initialized) {
            int newMaxHealth = (int)(PlayerState.Instance.MaxHealth * 1.1f);
            PlayerState.Instance.UpdateMaxHealth(newMaxHealth);
            Debug.Log("Max health increased.");
            Close();
        }
    }
    private void IncreaseMaxHealth(InputAction.CallbackContext context)
        => IncreaseMaxHealth();

    public void SetInitialized() {
        initialized = true;
    }

    private void OnEnable() {
        initialized = false;
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
