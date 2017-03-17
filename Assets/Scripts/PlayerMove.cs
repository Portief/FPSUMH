using UnityEngine;
using UnityEngine.Networking;

//[NetworkSettings(sendInterval =0.001f)]
public class PlayerMove : NetworkBehaviour {

    //Objects
    private Objects ObjectWeapons;

    //Movement
    public CharacterController controller;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;

    //Rotation
    //x
    public float sensX;
    private Vector3 moveDirection = Vector3.zero;
    private float yRot= 0;
    private Quaternion characterTargetRot;
    //y
    public float MaxCameraRotation;
    public float MinCameraRotation;
    public float sensY;

    //Animaciones
    private Animator anim;
    private float walk;
    public bool TakeWeapon = true;

    void Start()
    {

        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        anim.SetLayerWeight(1, 2f);
        anim.SetLayerWeight(2, 2f);

        if (!isLocalPlayer)
        {
            return;
        }
        ObjectWeapons = GetComponent<Objects>();
        ObjectWeapons.Cam.SetActive(true);
    }

    [ClientCallback]
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        //Rotation
        Rotation(anim,GetComponent<State>().Stunned);

        //Movement
        Move(controller,anim, GetComponent<State>().Stunned, GetComponent<State>().Slowdown);
    }
    public void Rotation(Animator anim,bool stunned)
    {
        if (!stunned)
        {
            //x
            yRot += Input.GetAxis("Mouse X") * sensX;
            transform.eulerAngles = new Vector3(0.0f, yRot, 0.0f);

            //y,Anim
            if (Input.GetAxis("Mouse Y") > 0 ? Vector3.Angle(ObjectWeapons.Cam.transform.forward, transform.up) > MaxCameraRotation : Vector3.Angle(ObjectWeapons.Cam.transform.forward, transform.up) < MinCameraRotation)
            {
                //rotatePOV (new Vector3 (-Input.GetAxis ("Mouse Y") * MouseSpeed * Time.deltaTime, 0, 0));
                anim.SetFloat(Animator.StringToHash("LongitudColumna"), Mathf.Lerp(0, 1, anim.GetFloat(Animator.StringToHash("LongitudColumna")) - Input.GetAxis("Mouse Y") * sensY * Time.deltaTime));
            }
        }
    }
    public void Move(CharacterController controller, Animator anim,bool stunned,float slowdown)
    {
        if (!stunned)
        {
            //Animation
            //Move
            walk = Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Horizontal")) ? Mathf.Abs(Input.GetAxis("Vertical")) : Mathf.Abs(Input.GetAxis("Vertical"));
            anim.SetFloat("Andando", walk);


            if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }
            moveDirection.y -= Physics.gravity.magnitude* Time.deltaTime;
            controller.Move(GlobalVariables.multiplier * moveDirection * Time.deltaTime - GlobalVariables.multiplier * slowdown * moveDirection * Time.deltaTime);
        }
    }
}
