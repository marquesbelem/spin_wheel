using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormController : MonoBehaviour
{
    [SerializeField] Button _btnGame;
    private void OnEnable()
    {
        _btnGame.onClick.AddListener(() => LoadScene(1));
    }

    private void OnDisable()
    {
        _btnGame.onClick.RemoveListener(() => LoadScene(1));
    }

    private void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
