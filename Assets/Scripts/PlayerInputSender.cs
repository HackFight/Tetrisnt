using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class PlayerInputSender : MonoBehaviour
{
    private PlayerInput _playerInput;
    private List<PlayerInputReciever> _squareRecievers = new List<PlayerInputReciever>();

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

        if (recievers.Length <= 0)
        {
            Debug.LogError("There is no PlayerInputRecievers in this scene!");
        }
    }

    public void OnButtonEast()
    {
        foreach (PlayerInputReciever reciever in _squareRecievers)
        {
            reciever.ButtonEast = true;
        }
    }

    public void OnButtonSouth()
    {
        foreach (PlayerInputReciever reciever in _squareRecievers)
        {
            reciever.ButtonSouth = true;
        }
    }

    public void OnButtonWest()
    {
        foreach (PlayerInputReciever reciever in _squareRecievers)
        {
            reciever.ButtonWest = true;
        }
    }

    public void OnButtonNorth()
    {
        foreach (PlayerInputReciever reciever in _squareRecievers)
        {
            reciever.ButtonNorth = true;
        }
    }

    public void AddSquareToList(PlayerInputReciever reciever)
    {
        if (reciever.gameObject.tag == "Square")
        {
            _squareRecievers.Add(reciever);
        }
    }
}