using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _credits;

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {
        if (_credits.activeSelf == true)
            _credits.SetActive(false);
        else
            _credits.SetActive(true);
    }
}
