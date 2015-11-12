using UnityEngine;
using System.Collections;

public class CharacterInterface : MonoBehaviour
{
    private Animator anim;
    private Transform cam;

    private float animationSpeed = 1f;
    private float inputVertical;
    private float inputHorizontal;

    private float rotSpeedX = 1f;
    private float rotSpeedY = 0.5f;

    public bool mouseRight = false;
    public bool mouseLeft = false;

    private float rotX;
    private float rotY;

    private bool modiferShift = false;

    void Start()
    {
        this.anim = this.gameObject.GetComponent<Animator>();
        this.cam = GameObject.Find("CameraWrapper").transform;
    }

    void Update()
    {
        if(!mouseRight)
        {
            anim.SetFloat("MouseX", 0f);
        }

        checkMouseInputs();
        checkControlBools();
        updateInputAxes();
        updateAnimationValues();
        checkKeyInputs();
    }

    void FixedUpdate()
    {
        
    }

    private void checkKeyInputs()
    {
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            modiferShift = false;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            modiferShift = true;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            setAnimationTrigger("OnRoll");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            setAnimationTrigger("OnJump");
        }
    }

    private void checkMouseInputs()
    {
        if(Input.GetMouseButtonUp(1))
        {
            mouseRight = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if(Input.GetMouseButtonDown(1))
        {
            mouseRight = true;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetMouseButtonUp(0))
        {
            mouseLeft = false;
        }

        if(Input.GetMouseButtonDown(0))
        {
            mouseLeft = true;
        }
    }

    private void checkControlBools()
    {
        if(mouseRight)
        {
            updateYRotation();
        }

        if(!modiferShift)
        {
            anim.SetBool("isRunning", false);
        }

        if(modiferShift)
        {
            anim.SetBool("isRunning", true);
        }
    }

    private void updateInputAxes()
    {
        // Check mouse movement combo buttons and enforce axis values
        if(mouseRight && mouseLeft)
        {
            inputVertical = 1.0f;
        }
        else
        {
            inputVertical = Input.GetAxisRaw("Vertical");  
        }
        inputHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void updateAnimationValues()
    {
        anim.SetFloat("Speed", inputVertical);
        anim.SetFloat("Direction", inputHorizontal);
        anim.speed = animationSpeed;
    }

    private void setAnimationTrigger(string s)
    {
        anim.SetTrigger(s);
    }

    private void updateYRotation()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        this.rotX += x * this.rotSpeedX;
        this.rotY -= y * this.rotSpeedY;

        if(x < -0.5f)
        {
            anim.SetFloat("MouseX", -1f);
        }

        if(x > 0.5f)
        {
            anim.SetFloat("MouseX", 1f);
        }
        
        this.transform.rotation = Quaternion.Euler(0f, this.rotX, 0f);
        this.cam.rotation = Quaternion.Euler(this.rotY, this.rotX, 0f);
    }
}
