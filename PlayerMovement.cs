using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour {

	public CharacterController2D controller;
	private Animator animator;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	bool jump = false;

	public Tilemap tilemap;
	public Grid grid;

	void Start(){
		animator = GetComponent<Animator>();
	}
	
	void Update () {

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if(horizontalMove!=0){
			//animator.SetBool("walking",true);
			animator.enabled=true;
		}
		else{
			//animator.SetBool("walking",false);
			animator.enabled=false;
		}

		
		//animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

		if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			jump = true;
			//animator.SetBool("IsJumping", true);
		}
	}

	public void OnLanding()
    {
		//animator.SetBool("IsJumping", false);
    }

	void FixedUpdate ()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		jump = false;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Pressure Plate"))
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
	}

	private void OnCollisionExit2D(Collision2D other)
    {
		if (other.gameObject.CompareTag("Pressure Plate")){
			FindObjectOfType<AudioManager>().Play("AirStop");
			if (other.gameObject.GetComponent<PressurePlateScript>().amountColliding >= 1)
				other.gameObject.GetComponent<PressurePlateScript>().amountColliding--;
		}
	}
}
