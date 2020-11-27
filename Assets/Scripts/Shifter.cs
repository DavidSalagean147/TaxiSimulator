using UnityEngine;

public class Shifter : MonoBehaviour
{
    public GameObject shifterR;
    public GameObject shifterD;
    public static bool isInDrive = true;

    private void Start()
    {
        shifterR.SetActive(false);
    }
    public void Shift()
    {
        if(isInDrive == true)
        {
            shifterD.SetActive(false);
            shifterR.SetActive(true);
            isInDrive = false;
        }
        else
        {
            shifterD.SetActive(true);
            shifterR.SetActive(false);
            isInDrive = true;
        }
    }
}
