using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player1;

    [SerializeField]
    private GameObject _player2;

    [SerializeField]
    private Image _winTextBackground;

    public UnityEvent OnGameWin;

    private TimerSystem _player1Timer;
    private TimerSystem _player2Timer;

    private TagSystem _player1TagSystem;
    private TagSystem _player2TagSystem;

    private PlayerController _player1Controller;
    private PlayerController _player2Controller;

    private Rigidbody _player1Rigidbody;
    private Rigidbody _player2Rigidbody;

    private bool _gameWon = false;

    private void Start()
    {
        if (_player1)
        {
            if (!(_player1.TryGetComponent<TimerSystem>(out _player1Timer)))
                Debug.LogError("GameManager: Start, Could not get _player1Timer");
            if (!(_player1.TryGetComponent<TagSystem>(out _player1TagSystem)))
                Debug.LogError("GameManager: Start, Could not get _player1TagSystem");
            if (!(_player1.TryGetComponent<PlayerController>(out _player1Controller)))
                Debug.LogError("GameManager: Start, Could not get _player1Controller");
            if (!(_player1.TryGetComponent<Rigidbody>(out _player1Rigidbody)))
                Debug.LogError("GameManager: Start, Could not get _player1Rigidbody");

            //_player1Timer = _player1.GetComponent<TimerSystem>();
            //_player1TagSystem = _player1TagSystem.GetComponent<TagSystem>();
            //_player1Controller = _player1Controller.GetComponent<PlayerController>();
        }
        else
            Debug.LogError("GameManager: Start, Player1 not assigned");
        if (_player2)
        {
            if (!(_player2.TryGetComponent<TimerSystem>(out _player2Timer)))
                Debug.LogError("GameManager: Start, Could not get _player2Timer");
            if (!(_player2.TryGetComponent<TagSystem>(out _player2TagSystem)))
                Debug.LogError("GameManager: Start, Could not get _player2TagSystem");
            if (!(_player2.TryGetComponent<PlayerController>(out _player2Controller)))
                Debug.LogError("GameManager: Start, Could not get _player2Controller");
            if (!(_player2.TryGetComponent<Rigidbody>(out _player2Rigidbody)))
                Debug.LogError("GameManager: Start, Could not get _player2Rigidbody");

            //_player2Timer = _player2.GetComponent<TimerSystem>();
            //_player2TagSystem = _player2TagSystem.GetComponent<TagSystem>();
            //_player2Controller = _player2Controller.GetComponent<PlayerController>();
        }
        else
            Debug.LogError("GameManager: Start, Player2 not assigned");

        if (!(_winTextBackground))
            Debug.LogWarning("GameManager: Start, _winTextBackground not assigned");
    }

    private void Update()
    {
        //If either timer is not assigned, stop
        if (!(_player1Timer && _player2Timer))
        {
            return;
        }

        //If game has already been won do nothing
        if (_gameWon)
        {
            return;
        }

        //Check if either player's timer is finished
        if (_player1Timer.TimeRemaining <= 0)
        {
            Win("Player 1 Wins!");
        }
        else if (_player2Timer.TimeRemaining <= 0)
        {
            Win("Player 2 Wins!");
        }
    }

    private void Win(string winText)
    {
        //Enable win Screen UI and set the text to be winText
        if (_winTextBackground)
        {
            _winTextBackground.gameObject.SetActive(true);
            TextMeshProUGUI text = _winTextBackground.GetComponentInChildren<TextMeshProUGUI>(true);

            if (text)
            {
                text.text = winText;
            }
        }

        //Turn off player controller and tag system and timer
        if (_player1Timer)
            _player1Timer.enabled = false;
        if (_player1TagSystem)
            _player1TagSystem.enabled = false;
        if (_player1Controller)
            _player1Controller.enabled = false;
        if (_player1Rigidbody)
            _player1Rigidbody.isKinematic = true;

        if (_player2Timer)
            _player2Timer.enabled = false;
        if (_player2TagSystem)
            _player2TagSystem.enabled = false;
        if (_player2Controller)
            _player2Controller.enabled = false;
        if (_player2Rigidbody)
            _player2Rigidbody.isKinematic = true;

        OnGameWin.Invoke();
    }

    public void ResetGame()
    {
        //Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}