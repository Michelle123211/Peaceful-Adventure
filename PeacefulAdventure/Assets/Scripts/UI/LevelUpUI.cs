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
        AudioManager.Instance.PlaySoundEffect(SoundType.UIOpen); // TODO: Mozna rovnou odebrat
        AudioManager.Instance.PlaySoundEffect(SoundType.LevelUp);
        gameObject.TweenAwareEnable();
    }

    public void Close() {
        PlayerBehaviour.playerInputActions.UI.Disable();
        PlayerBehaviour.playerInputActions.Player.Enable();
        AudioManager.Instance.PlaySoundEffect(SoundType.UIClose);
        gameObject.TweenAwareDisable();
    }

    public void IncreaseAttackDamage() {
        if (initialized) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            float newDamage = PlayerState.Instance.attackDamage.BaseValue * 1.1f;
            PlayerState.Instance.attackDamage.ChangeBaseValue(newDamage);
            Close();
        }
    }
    private void IncreaseAttackDamage(InputAction.CallbackContext context)
        => IncreaseAttackDamage();

    public void IncreaseAttackSpeed() {
        if (initialized) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            float newCooldown = PlayerState.Instance.attackCooldown.BaseValue * 0.9f;
            PlayerState.Instance.attackCooldown.ChangeBaseValue(newCooldown);
            Close();
        }
    }
    private void IncreaseAttackSpeed(InputAction.CallbackContext context)
        => IncreaseAttackSpeed();

    public void IncreaseMaxHealth() {
        if (initialized) {
            AudioManager.Instance.PlaySoundEffect(SoundType.UIPress);
            int newMaxHealth = (int)(PlayerState.Instance.MaxHealth * 1.1f);
            PlayerState.Instance.UpdateMaxHealth(newMaxHealth);
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
