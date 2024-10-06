using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportItem : MonoBehaviour
{
    [SerializeField] private GameObject TeleportTarget;


    void Update()
    {
        gameObject.transform.Rotate(1, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController player = other.GetComponent<CharacterController>();
            if (player)
            {
                if (PlayerLogic.instance.CurrentTeleportCoolDown <= 0)
                {
                    player.enabled = false; // mitigate issues related to collision detection, physics calculations, and character state inconsistencies in Unity
                    player.transform.position = TeleportTarget.transform.position;
                    player.enabled = true;
                    PlayerLogic.instance.CurrentTeleportCoolDown = PlayerLogic.instance.TeleportCoolDown;
                }
            }
        }
    }

}

