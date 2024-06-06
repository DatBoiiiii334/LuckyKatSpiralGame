using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TODOLIST : MonoBehaviour
{
    /* Level Generation Rules
        - First structure must always not contain a red piece
        - First missing piece should not be directly underneath ball spawn
        - Red pieces should be one per lvl over lvl 3
        - Each structure has a chance to 
    */

    /* STAGE 1  STRUCTURE
    - Make a lvl progression system that adds more spireStructures and failPieces to the Helix with each higher lvl 
    - Make Finite State Machine, to control the flow of the game 
    - Make a POINT system to get points each time you pass an exitPiece 
    - Make a FAIL system to generate pieces that when touched make you fail 
    - Make an event system for the camera control script
    */

    /* STAGE 2 POLISH
    - Make a nice working scalable UI 
    - Make a start page 
    - Make a the Helixball make noice each time it hits a piece 
    - Make an interesting background environment
    - Make background environments change with each level
    - Add background music for each State 
    - Post processing
    */

    /* STAGE 3 EXPORT
    - Export the game and make sure it works
    - Export an SDK version
    */



    /* POWER UPS
        - pick up powerup have different effects
        - red powerup gives you +1 live back
        - Blue powerup gives you +100 points
        - Gold powerup makes your ball superstong and makes it crash through all obstacles 
        - 

        Spawn Walls
    */



}
