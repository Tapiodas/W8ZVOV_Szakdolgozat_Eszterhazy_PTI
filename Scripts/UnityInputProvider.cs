using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityInputProvider : IInputProvider
{
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }

    public float GetZAxisInput()
    {
        return Input.GetAxis("ZAxis");
    }
}

   

