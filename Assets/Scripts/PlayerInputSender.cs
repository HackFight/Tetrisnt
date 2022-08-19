using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputSender : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInputReciever _playerInputReciever;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += (scene, mode) => LoadInputReciever();

        LoadInputReciever();
    }

    private void LoadInputReciever()
    {
        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        _playerInputReciever = recievers.FirstOrDefault(i => i.PlayerIndex == _playerInput.playerIndex);

        if (recievers.Length <= 0)
        {
            Debug.LogError("There is no PlayerInputRecievers in this scene!");
        }
    }

    public void OnButtonEast(InputValue value)
    {
        _playerInputReciever.ButtonEast = value.Get<float>() == 1;
    }

    public void OnButtonSouth(InputValue value)
    {
        _playerInputReciever.ButtonSouth = value.Get<float>() == 1;
    }

    public void OnButtonWest(InputValue value)
    {
        _playerInputReciever.ButtonWest = value.Get<float>() == 1;
    }

    public void OnButtonNorth(InputValue value)
    {
        _playerInputReciever.ButtonNorth = value.Get<float>() == 1;
    }

    private void OnLeftJoystick(InputValue value)
    {
        _playerInputReciever.Joystick = value.Get<Vector2>();
    }

    private void LateUpdate()
    {
        _playerInputReciever.ButtonEast = false;
        _playerInputReciever.ButtonSouth = false;
        _playerInputReciever.ButtonWest = false;
        _playerInputReciever.ButtonNorth = false;
    }
}