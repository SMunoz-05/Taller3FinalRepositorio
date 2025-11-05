using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject pausePanel;

    private bool isGamePause = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isGamePause = !isGamePause; 
            //si es true lo cambia a lo contrario y vicebersa, activando y desactivanmdo el menu de pausa
            PauseGame();    
        }
    }

    public void PauseGame()
    {
        if (!isGamePause)
        {
            Time.timeScale = 0;

            pausePanel.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;

            pausePanel.SetActive(false);
        }
    }
}
