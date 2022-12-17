using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHouse : MonoBehaviour
{

    public GameObject part1;
    public GameObject part2;
    public GameObject part3;

    void Start() {
        part1.SetActive(false);
        part2.SetActive(false);
        part3.SetActive(false);
        // show part of the tutorial corresponding to the player's progress
        if (PlayerState.Instance.tutorialCompleted) {
            part3.SetActive(true);
        } else if (PlayerState.Instance.levelSystem.Experience >= PlayerState.Instance.levelSystem.GetExperienceNeededForLevel(1)) {
            part2.SetActive(true);
        } else {
            part1.SetActive(true);
        }
    }

    public void CheckIfTutorialIsComplete() {
        if (PlayerState.Instance.levelSystem.Level > 0 && PlayerState.Instance.inventory.GetDistinctItemsCount() > 0) {
            // the tutorial is completed if the player has leveled up and has some items in the inventory
            PlayerState.Instance.tutorialCompleted = true;
        }
    }
}