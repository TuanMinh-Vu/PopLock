using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// A MonoBehavior that has caches of the important managers used in the game
/// </summary>
public class GameSystem : MonoBehaviour {


    protected CachedComponent<InputManager> _inputManager = new CachedComponent<InputManager>();
    protected InputManager inputManager
    {
        get
        {
            return _inputManager.instance(this);
        }
    }

    protected CachedComponent<GameManager> _gameManager = new CachedComponent<GameManager>();
    protected GameManager gameManager
    {
        get
        {
            return _gameManager.instance(this);
        }
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        _inputManager.clear();
        _gameManager.clear();

    }
}
