using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    /*
    Writen by Windexglow 11-13-10.  Use it, edit it, steal it I don't care.  
    Converted to C# 27-02-13 - no credit wanted.
    Simple flycam I made, since I couldn't find any others made public.  
    Made simple to use (drag and drop, done) for regular keyboard layout  
    wasd : basic movement
    shift : Makes camera accelerate
    space : Moves camera on X and Z axis only.  So camera doesn't gain any height*/

    [Range(0, 100)]
    public float mainSpeed = 100.0f; //regular speed
    float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    [Range(0, 1000)]
    public float maxShift = 1000.0f; //Maximum speed when holdin gshift
    [Range(0, 1)]
    public float camSens = 0.25f; //How sensitive it with mouse
    private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;
    Vector3[] camPoints = {
        //new Vector3(0,110,0),
        new Vector3(60,100,70),
        //new Vector3(60,40,100),
        new Vector3(-60,-100,-70),
         //new Vector3(0,110,0),
        new Vector3(-60,90,60),
         //new Vector3(-60,50,90),
        new Vector3(60,-90,-60),
         //new Vector3(0,110,0),
        new Vector3(50,90,75),
         //new Vector3(50,35,90),
        new Vector3(-50,-90,-75),
         //new Vector3(0,110,0),
        new Vector3(43,100,75),
         //new Vector3(43,35,100),
        new Vector3(-43,-100,-75),
         //new Vector3(0,110,0),
        };
    // Vector3[] camRotatePoints = {
    //     // new Vector3(90,0,0),
    //     new Vector3(70,180,0),
    //     // new Vector3(-20,-180,0),
    //     new Vector3(90,0,0),
    //     new Vector3(70,180,0),
    //     new Vector3(90,0,0),
    //     new Vector3(70,180,0),
    //     new Vector3(90,0,0),
    //     new Vector3(70,180,0),
    //     new Vector3(90,0,0),
    //     };
    int camPointCounter = 0;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;
            //Mouse  camera angle done.  

            //Keyboard commands
            float f = 0.0f;
            Vector3 p = GetBaseInput();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space))
            { //If player wants to move on X and Z axis only
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(p);
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log(camPoints);
            if(camPointCounter==0||camPointCounter<=7)
            {
                Debug.Log("camPointCounter==0||camPointCounter<=7");
                Camera.main.transform.Translate(camPoints[camPointCounter]);
                // Camera.main.transform.Rotate(camRotatePoints[camPointCounter]);
                camPointCounter++;
                Debug.Log(camPointCounter);
            }
            if(camPointCounter==8)
            {
                Debug.Log("camPointCounter==100");
                Debug.Log(camPointCounter);
                camPointCounter=0;
                Debug.Log(camPointCounter);
            }
            // Camera.main.transform.position = cameraPos;
            // Camera.main.transform.LookAt(cameraLootAkPos);
        }
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        // if (Input.GetMouseButton(0))
        // Debug.Log("Pressed primary button 0.");
        // if (Input.GetMouseButton(1))
        // Debug.Log("Pressed primary button 1.");
        // if (Input.GetMouseButton(2))
        // Debug.Log("Pressed primary button 2.");
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.E))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            p_Velocity += new Vector3(0, -1, 0);
            Debug.Log(p_Velocity);
        }
        return p_Velocity;
    }
}
