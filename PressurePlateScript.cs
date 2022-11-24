using UnityEngine;

public class PressurePlateScript : MonoBehaviour
{
    public GameObject queActiva;
    public bool activaPuerta;

    [HideInInspector]
    public int amountColliding = 0;

    private void Update()
    {
        if (amountColliding == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.botonApagado;
            if (!activaPuerta){ //si activa un ventilador
                queActiva.SetActive(false);
            }
            else //si activa la puerta de salida
            {
                queActiva.GetComponent<SpriteRenderer>().sprite = GameManager.instance.puertaCerrada;
                queActiva.GetComponent<DoorScript>().isDoorOpen = false;
            }
        }
    }
}
