using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpUiImage : MonoBehaviour
{
   public void TurnOff()
    {
        transform.GetComponent<Image>().enabled = false;
    }
}
