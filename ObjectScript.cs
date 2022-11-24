using UnityEngine;

public class ObjectScript : MonoBehaviour
{
    private bool moveRight = false, moveLeft = false;

    public float speed = 200f;
    private Rigidbody2D rb;

    private string moviendoA = "";

    void Start() => rb = gameObject.GetComponent<Rigidbody2D>();

    private void FixedUpdate()
    {
        if (moveRight) rb.velocity = new Vector3(1f, 0f, 0f) * speed * Time.fixedDeltaTime;
        if (moveLeft) rb.velocity = new Vector3(-1f, 0f, 0f) * speed * Time.fixedDeltaTime;
    }

    public void OnMouseDown()
    {
        switch (GameManager.instance.estadoActual)
        {
            case GameManager.estado.solido:
                FindObjectOfType<AudioManager>().Play("fireSolid");
                FindObjectOfType<AudioManager>().Play("turnSolid");
                gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.solido;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 2;
                gameObject.layer = LayerMask.NameToLayer("Solido");
                moveRight = false;
                moveLeft = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;

                //para arreglar un bug que si esta liquido sobre un boton y pasas a solido, no detecta que esta sobre el boton
                gameObject.transform.position = gameObject.transform.position + new Vector3(0f, 0.01f, 0f);
                break;
            case GameManager.estado.liquido:
                gameObject.transform.position = gameObject.transform.position + new Vector3(0f, 0.01f, 0f);
                FindObjectOfType<AudioManager>().Play("fireLiquid");
                FindObjectOfType<AudioManager>().Play("turnLiquid");
                gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.liquido;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0.5f;
                gameObject.layer = LayerMask.NameToLayer("Liquido");
                moveRight = false;
                moveLeft = false;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
                break;
            case GameManager.estado.gaseoso:
                FindObjectOfType<AudioManager>().Play("fireGas");
                FindObjectOfType<AudioManager>().Play("turnGas");
                gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.gaseoso;
                gameObject.GetComponent<Rigidbody2D>().gravityScale = -0.1f;
                gameObject.layer = LayerMask.NameToLayer("Gaseoso");
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //si el objeto esta en estado solido y pisa una pressure plate
        if (gameObject.layer == LayerMask.NameToLayer("Solido") && other.gameObject.CompareTag("Pressure Plate"))
        {
            FindObjectOfType<AudioManager>().Play("pressPressurePlate");
            FindObjectOfType<AudioManager>().Play("AirStart");
            other.gameObject.GetComponent<SpriteRenderer>().sprite = GameManager.instance.botonPrendido;
            if (!other.gameObject.GetComponent<PressurePlateScript>().activaPuerta) //si activa un ventilador
                other.gameObject.GetComponent<PressurePlateScript>().queActiva.SetActive(true);
            else
            {
                other.gameObject.GetComponent<PressurePlateScript>().queActiva.GetComponent<SpriteRenderer>().sprite = GameManager.instance.puertaAbierta;
                other.gameObject.GetComponent<PressurePlateScript>().queActiva.GetComponent<DoorScript>().isDoorOpen = true;
            }
            other.gameObject.GetComponent<PressurePlateScript>().amountColliding++;
        }

        //si el objeto esta en estado gaseoso y pasa por aire
        if (gameObject.layer == LayerMask.NameToLayer("Gaseoso") && (other.gameObject.CompareTag("AireIzq") || other.gameObject.CompareTag("AireDer")))
        {
            //no lo deja mover vertical, y activa su horizontal
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            if (other.gameObject.CompareTag("AireDer"))
            {
                moveRight = true;
                moviendoA = "der";
            }
            if (other.gameObject.CompareTag("AireIzq"))
            {
                moveLeft = true;
                moviendoA = "izq";
            }
        }

        //si el objeto esta en estado gaseoso y se choca on pared
        if (other.gameObject.CompareTag("Pared"))
        {
            if (gameObject.layer == LayerMask.NameToLayer("Gaseoso"))
            {
                moveRight = false;
                moveLeft = false;
            }
            if (moviendoA == "der")
                gameObject.transform.position = gameObject.transform.position + new Vector3(-0.02f, 0f, 0f);
            if (moviendoA == "izq")
                gameObject.transform.position = gameObject.transform.position + new Vector3(0.02f, 0f, 0f);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Pressure Plate"))
        {
            FindObjectOfType<AudioManager>().Play("AirStop");
            if (other.gameObject.GetComponent<PressurePlateScript>().amountColliding >= 1)
                other.gameObject.GetComponent<PressurePlateScript>().amountColliding--;
        }

        if (gameObject.layer == LayerMask.NameToLayer("Gaseoso") && (other.gameObject.CompareTag("AireIzq") || other.gameObject.CompareTag("AireDer")))
        {
            moveRight = false;
            moveLeft = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
    }
}
