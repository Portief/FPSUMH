using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerMove : NetworkBehaviour {
    //Movimiento
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float multiplier;

    //Rotacion
    //x
    public float sensX;
    public GameObject cam;
    private Vector3 moveDirection = Vector3.zero;
    private float yRot= 0;
    private Quaternion characterTargetRot;
    //y
    public float MaxCameraRotation;
    public float MinCameraRotation;
    public float sensY;

    //Animaciones
    private float andar;
    private int sg;
    private int d;
    public bool SacarGuardar;
    public bool Disparar;

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        cam.SetActive(true);
        CharacterController controller = GetComponent<CharacterController>();
        Animator anim = GetComponent<Animator>();
        Rpcsetlayer();

        //Animacion
        andar = Mathf.Abs(Input.GetAxis("Horizontal"))> Mathf.Abs(Input.GetAxis("Horizontal"))? Mathf.Abs(Input.GetAxis("Vertical")): Mathf.Abs(Input.GetAxis("Vertical"));
        SacarGuardar = Input.GetButton("Fire2") ? true : SacarGuardar;
        Disparar = Input.GetButton("Fire1") ? true:false;
        anim.SetFloat("Andando",andar);
        anim.SetBool(Animator.StringToHash("SacarGuardarArma"), SacarGuardar);
        anim.SetBool(Animator.StringToHash("Disparar"), Disparar);

        //Rotacion
        //x
        yRot += Input.GetAxis("Mouse X") * sensX;
        transform.eulerAngles = new Vector3(0.0f, yRot, 0.0f);
        //y
        if (Input.GetAxis("Mouse Y") > 0 ? Vector3.Angle(cam.transform.forward, transform.up) > MaxCameraRotation : Vector3.Angle(cam.transform.forward, transform.up) < MinCameraRotation)
        {
            Debug.Log(Input.GetAxis("Mouse Y"));
            //rotatePOV (new Vector3 (-Input.GetAxis ("Mouse Y") * MouseSpeed * Time.deltaTime, 0, 0));
               anim.SetFloat(Animator.StringToHash("LongitudColumna"), Mathf.Lerp(0, 1, anim.GetFloat(Animator.StringToHash("LongitudColumna")) - Input.GetAxis("Mouse Y") * sensY * Time.deltaTime));
        }
        //Movimiento
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
               moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(multiplier*moveDirection * Time.deltaTime);
    }

    [ClientRpc]
    void Rpcsetlayer()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetLayerWeight(1, 2f);
        anim.SetLayerWeight(2, 2f);
    }
}
