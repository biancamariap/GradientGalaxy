using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public static class GameFunctions 
{
    public static void ChangeMenu(GameObject[] menus, int id)
    {
        for (int i=0; i< menus.Length; i++ )
        {
            menus[i].SetActive(i == id ? true : false);
        }
    }

    
  
}
