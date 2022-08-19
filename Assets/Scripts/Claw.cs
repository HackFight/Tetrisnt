using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Claw : MonoBehaviour
{
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [SerializeField][Range(0, 10)] float speed;

    private const int PLAYER_INDEX1 = 0;
    private PlayerInputReciever playerInputReciever1;

    public GameObject _followClaw;

    private void Awake()
    {
        PlayerInputReciever[] recievers = FindObjectsOfType<PlayerInputReciever>();
        playerInputReciever1 = recievers.First(i => i.PlayerIndex == PLAYER_INDEX1);
    }

    private void Update()
    {
        if (transform.position.x >= minX && transform.position.x <= maxX)
        {
            transform.Translate(Vector3.right * playerInputReciever1.Joystick.x * speed * Time.deltaTime);
        }

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, 0);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, 0);
        }

        _followClaw.transform.position = new Vector3(Mathf.RoundToInt(transform.position.x), _followClaw.transform.position.y, 0);
    }
}
