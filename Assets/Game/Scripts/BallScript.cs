using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallScript : MonoBehaviour
{
    public GameManager gameManager;


    public bool hasHit;
    public int comboMultiplier = 1;

    [SerializeField]
    private  float bounceForce = 400f;
    private Rigidbody rb;
    private bool isCombo;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision other) {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        

        if(other.gameObject.CompareTag("ground") || other.gameObject.CompareTag("win")){
            isCombo = false;
            comboMultiplier = 1;
            gameManager.myAudioManager.PlaySFX(gameManager.myAudioManager.ballBounce);
            print("Ground");
        }

        if(hasHit == false){
            if(other.gameObject.CompareTag("damage")){
                gameManager.Loselive();
                hasHit = true;
                isCombo = false;
                comboMultiplier = 2;
            }

            if(other.gameObject.CompareTag("win")){
                gameManager.PlayerWonLevel();
                hasHit = true;
                isCombo = false;
                comboMultiplier = 2;
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("points")){
            if(isCombo == true){
                gameManager.AddPlayerPoints(1 * comboMultiplier);
                comboMultiplier += 1;
                gameManager.comboTextUI.text = "Combo x" + comboMultiplier;
                return;
            }
            gameManager.AddPlayerPoints(1);
            isCombo = true;
        }
        if(other.gameObject.CompareTag("RedPowerUp")){
            gameManager.AddPlayerLives(1);
            gameManager.myAudioManager.PlaySFX(gameManager.myAudioManager.powerupTouch);
        }
        if(other.gameObject.CompareTag("BluePowerUp")){
            gameManager.AddPlayerPoints(10);
            gameManager.comboTextUI.text = "Points +" + 10;
            gameManager.myAudioManager.PlaySFX(gameManager.myAudioManager.powerupTouch);
        }
        if(other.gameObject.CompareTag("GoldPowerUp")){
            gameManager.comboTextUI.text = "SuperBall";
            gameManager.myAudioManager.PlaySFX(gameManager.myAudioManager.powerupTouch);
        }
        isCombo = false;
        Destroy(other.gameObject);
    }
}
