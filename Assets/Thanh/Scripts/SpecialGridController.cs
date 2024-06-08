using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialGridController : MonoBehaviour
{

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            //if()
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

        }
    }

    void CheckForMoves(UnitParent unit)
    {
        if (unit.UnitType == UnitType.Rock)
            return;
        //if()
    }

    //void MoveState()
}
