using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ShipMovement
{

    // Update is called once per frame
    protected override void Update()
    {
        if (GameManager.instance.inMenu)
        {
            UpdateMotor(0);
            return; 
        }

        base.Update();
        UpdateMotor(Input.GetAxisRaw(isPlayer1 ? "Vertical" : "Vertical2"));
    }
}
