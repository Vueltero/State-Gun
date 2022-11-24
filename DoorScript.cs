using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool isDoorOpen = false;
    public int nextLevelID;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
            if (isDoorOpen)
            {
                FindObjectOfType<AudioManager>().Play("win");
                GameManager.instance.NextLevel(nextLevelID);
            }
    }
}
