using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TableManager _tableManager;

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowBestMove()
    {
        _tableManager.ShowBestMove();
    }
}
