using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string menu_name;

    public void Menu_Button ()
    {
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Default_Active();
        GameObject.FindGameObjectWithTag("Data").GetComponent<MSG_Transitioner>().Save_Data();
        SceneManager.LoadScene(menu_name);
    }
}
