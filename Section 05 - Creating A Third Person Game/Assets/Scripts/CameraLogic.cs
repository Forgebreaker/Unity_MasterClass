using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraLogic : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private float Distance_Y = 1f;
    [SerializeField] private float Distance_Z = -3f;

    public float CameraRotation_X;
    public float CameraRotation_Y;

    private float MouseScroll;

    public static CameraLogic Instance;

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }

    void Update()
    {

        CameraRotation_X -= Input.GetAxis("Mouse Y");
        CameraRotation_Y += Input.GetAxis("Mouse X");

        CameraRotation_X = Mathf.Clamp(CameraRotation_X, -30f, 30f);

        MouseScroll = Input.GetAxis("Mouse ScrollWheel");

        Distance_Z += MouseScroll;

        Distance_Z = Mathf.Clamp(Distance_Z, -3f, -1.5f);
    }

    private void LateUpdate()
    {
        this.gameObject.transform.position = Player.transform.position + Quaternion.Euler(CameraRotation_X, CameraRotation_Y, 0) * new Vector3(0, Distance_Y, Distance_Z);
        transform.LookAt(Player.transform.position);
    }

    public Vector3 GetForwardVector() 
    { 
        Quaternion Rotation = Quaternion.Euler(0, CameraRotation_Y, 0);
        return Rotation * Vector3.forward;
    }
}
