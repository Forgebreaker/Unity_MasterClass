using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private float CameraOffset = 10f;
    [SerializeField] private Camera CameraThatLookPlayer;
    private GameObject Player;
    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnEnable()
    {
        CameraThatLookPlayer = Camera.main;
    }


    void Update()
    {
        UpdateCameraPosition();

        if (Input.GetKey(KeyCode.Space))
        {
            CameraThatLookPlayer.transform.position = new Vector3(  Player.transform.position.x + 7.5f, 
                                                                    CameraThatLookPlayer.transform.position.y, 
                                                                    Player.transform.position.z);
        }
    }

    void UpdateCameraPosition() 
    {
        if (Input.mousePosition.x >= Screen.width) // this means that your mouse is in the right border of the screen
        {
            CameraThatLookPlayer.transform.position = new Vector3(this.gameObject.transform.position.x,
                                                                  this.gameObject.transform.position.y,
                                                                  this.gameObject.transform.position.z + CameraOffset * Time.deltaTime);
        }

        if (Input.mousePosition.x <= 0) 
        {
            CameraThatLookPlayer.transform.position = new Vector3(this.gameObject.transform.position.x,
                                                                  this.gameObject.transform.position.y,
                                                                  this.gameObject.transform.position.z + CameraOffset * (-1) * Time.deltaTime);
        }
        
        if (Input.mousePosition.y >= Screen.height) 
        {
            CameraThatLookPlayer.transform.position = new Vector3(this.gameObject.transform.position.x + CameraOffset * (-1) * Time.deltaTime,
                                                                  this.gameObject.transform.position.y,
                                                                  this.gameObject.transform.position.z);
        }
        
        if (Input.mousePosition.y <= 0) 
        {
            CameraThatLookPlayer.transform.position = new Vector3(this.gameObject.transform.position.x + CameraOffset * Time.deltaTime,
                                                                  this.gameObject.transform.position.y,
                                                                  this.gameObject.transform.position.z);
        }

    }
}
