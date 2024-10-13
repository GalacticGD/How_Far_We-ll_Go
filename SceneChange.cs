using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public GameObject menu1;
    public GameObject menu2;

    private void Start()
    {
        menu1.SetActive(true);
        menu2.SetActive(false);
    }

    public void OnClickPlay()
    {
        menu1.SetActive(false);
        menu2.SetActive(true);
    }

    public void OnClickL1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
