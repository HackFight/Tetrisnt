using UnityEngine;

public class MenuManager : MonoBehaviour
{

    [SerializeField] private GameObject[] playTurn = new GameObject[2];
    [SerializeField] private GameObject[] optionsTurn = new GameObject[2];
    private bool canTurn = true;

    private void Start()
    {
        canTurn = true;
    }

    public void ColoredSquare(int type)
    {
        if (type == 1 && canTurn)
        {
            canTurn = false;
            foreach (GameObject conv in playTurn)
            {
                if (conv.activeSelf)
                {
                    conv.SetActive(false);
                }
                else
                {
                    conv.SetActive(true);
                }
            }
        }
        else if (type == 2 && canTurn)
        {
            canTurn = false;
            foreach (GameObject conv in optionsTurn)
            {
                if (conv.activeSelf)
                {
                    conv.SetActive(false);
                }
                else
                {
                    conv.SetActive(true);
                }
            }
        }
    }
}
