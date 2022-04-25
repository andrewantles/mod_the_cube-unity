using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Project hosted at: https://play.unity.com/mg/other/webgl-builds-167856 */

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;

    private int coinFlip;

    //Declare class-wide variable for random rotation range.
    private float randomRotationX;
    private float randomRotationY;
    private float randomRotationZ;

    //Color value ranges
    public static float maxValue = 0.75f;
    public static float minValue = 0.25f;
    private float valueRange = maxValue - minValue;

    //Set starting color values
    private float red = minValue;//0.25f;
    private float green = minValue;//0.25f;
    private float blue = maxValue;//0.65f;

    private float timer = 6;
    private float timerMultiplier;

    void Start()
    {
        SetRotation();

        // Set starting color
        Material material = Renderer.material;
        material.color = new Color(red, green, blue, 0.999f);
    }

    void Update()
    {
        // Begin rotating according to the random rotation set in SetRotation()
        transform.Rotate(
            randomRotationX * Time.deltaTime,
            randomRotationY * Time.deltaTime,
            randomRotationZ * Time.deltaTime
            );

        // Start a six second time, loop it, and set a new rotation after each timer loop.
        // timerMultiplier is a value between 0 and 1, equal to where exactly within the 
        // current second the timer is; e.g. 5.245 = 0.245; 3.786 = 0.786; etc. 
        // Debug.Log(timer);
        timer -= Time.deltaTime;
        timerMultiplier = timer - (float)System.Math.Floor(timer);
        
        if (timer <= 0) { 
            timer = 6;
            SetRotation();
        }

        /*
            At various stages in the timer, rotate colors for RGB effect.
            E.g.: As timer approaches the bottom of the current second, 5.4, 5.3, 5.2,...
            The slider is approaching its max or min value: minValue + 0.4, minValue + 0.3,...
            maxValue - 0.4, maxValue - 0.3, maxValue - 0.2,...
        */
        if (timer < 6 && timer > 5 && green < 0.65f) { 
            green = maxValue - (timerMultiplier * valueRange);
            //Debug.Log("Green: " + green); 
        }
        if (timer < 5 && timer > 4) { 
            blue = minValue + (timerMultiplier * valueRange); 
            //Debug.Log("Blue: " + blue); 
        }
        if (timer < 4 && timer > 3) { 
            red = maxValue - (timerMultiplier * valueRange);
            //Debug.Log("Red: " + red); 
        }
        if (timer < 3 && timer > 2) { 
            green = minValue + (timerMultiplier * valueRange);
            //Debug.Log("Green: " + green); 
        }
        if (timer < 2 && timer > 1) { 
            blue = maxValue - (timerMultiplier * valueRange);
            //Debug.Log("Blue: " + blue); 
        }
        if (timer < 1 && timer > 0) { 
            red = minValue + (timerMultiplier * valueRange);
            //Debug.Log("Red: " + red); 
        }

        // This is RGB color slider moves matched to the timer seconds
        //start = Blue high - other two down
        //Green goes up 6
        //Blue comes down 5
        //red goes up 4
        //green goes down 3
        //blue goes up 2
        //red goes down 1
        //back at start

        //Apply the changing color each frame
        Material material = Renderer.material;
        material.color = new Color(red, green, blue, 0.999f);
    }

    void SetRotation() 
    {
        //Flip a coin:
        coinFlip = Random.Range(0, 2);

        //Pick a random rotation value at start
        randomRotationX = Random.Range(40f, 80f);
        randomRotationY = Random.Range(40f, 80f);
        randomRotationZ = Random.Range(40f, 80f);

        //Heads or tails - spin the cube the opposite way on some of the axes
        if (coinFlip > 0)
        {
            randomRotationX = randomRotationX * -1;
            randomRotationZ = randomRotationZ * -1;
        }
    }
}
