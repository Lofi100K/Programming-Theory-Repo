using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MainSceneManager : MonoBehaviour
{
    public Button defaultButton; // Il pulsante da selezionare automaticamente

    void Start()
    {
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton.gameObject);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("TitleScreen");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // Ferma il Play Mode nell'Editor
#else
        Application.Quit(); // Chiude il gioco nella build
#endif       

    }
}
