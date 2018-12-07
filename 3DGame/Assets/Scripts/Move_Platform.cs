using UnityEngine;
using System.Collections;

public class Move_Platform : MonoBehaviour
{

    public Transform movingPlatform;
    public Transform position1;
    public Transform position2;
    public Vector3 newPosition;
    public string currentState;
    public float smooth;


    private void Start()
    {

        newPosition = position2.position;
    }
    void FixedUpdate()
    {
       
      
        if (IS_Platform1_less_Platform2(position1,position2))
        {
            //Выполняется если позиция1 меньше позиции2 по х
            if (movingPlatform.position.x <= position1.transform.position.x + 0.1f)//Возвращаем с далекого минуса
            {
              
                currentState = "Moving To Position 2";
              //  Debug.Log(currentState);
                newPosition = position2.position;
            }
            else if (movingPlatform.position.x >= position2.transform.position.x - 0.1f)
            {

                currentState = "Moving To Position 1";
              //  Debug.Log(currentState);
                newPosition = position1.position;
            }
        }
        else
        {  //Выполняется если позиция2 меньше позиции1 по х
            if (movingPlatform.position.x >= position1.transform.position.x - 0.1f)
            {
          
                currentState = "Moving To Position 2";
             //   Debug.Log(currentState);
                newPosition = position2.position;
            }
            else if (movingPlatform.position.x <= position2.transform.position.x + 0.1f)
            {

                currentState = "Moving To Position 1";
             //   Debug.Log(currentState);
                newPosition = position1.position;
            }
        }
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
    }
    
    //Метод,который определяет какой платформы координаты больше
    bool IS_Platform1_less_Platform2(Transform platform1, Transform platform2)
    {

        if (platform1.position.x < platform2.position.x)
        {
            return true;
        }
        else
        {
            return false;
        }
    } 


    /*
        if (movingPlatform.position.x >= position1.position.x - diapazon || movingPlatform.position.x <= position1.position.x + diapazon)
        {
            currentState = "Moving To Position 2";
            newPosition = position2.position;
        }
        else if (movingPlatform.position.x >= position2.position.x - diapazon || movingPlatform.position.x <= position2.position.x + diapazon)
        {
            currentState = "Moving To Position 1";
            newPosition = position1.position;
        }*/
}
