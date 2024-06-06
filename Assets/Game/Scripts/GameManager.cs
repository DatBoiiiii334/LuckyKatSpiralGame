using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Helix Structure Creation")]
    public GameObject spireStructure;
    public GameObject winStructure;
    public GameObject spawnParent;
    public GameObject spirePiece;
    public GameObject spireDamagePiece;
    public GameObject helixBall;
    public CameraFollow mainCamera;
    public List<GameObject> powerupList;
    public GameObject textHolder;
    public TMP_Text comboTextUI;
    public TMP_Text lostLiveTextUI;

    [Header("Spire Structure List")]
    public List<GameObject> spireStructureList;

    [Header("AudioManager")]
    public AudioManager myAudioManager;

    public GameObject UIMainMenu;
    public GameObject UIOptionsMenu;
    
    [SerializeField]
    private int totalSpireCount;

    [Header("TMP UI text fields")]
    [SerializeField]
    private TMP_Text gameStatePlayerScoreUI;
    [SerializeField]
    private TMP_Text winStatePlayerScoreUI;
    [SerializeField]
    private TMP_Text loseStatePlayerScoreUI; 
    [SerializeField]
    private TMP_Text currentPlayerLevelUI;
    [SerializeField]
    private TMP_Text playerLivesUI;
    
    private int totalPlayerPoints;
    public int levelCounter;
    private int randomNum;
    private int randomNum2;
    private int randomNum3;
    private int playerLives;
    private Vector3 helixBallVector;
    private Vector3 spireVector;
    private Vector3 ExitPieceSpirevector3;
    private Vector3 BadStructurePart;
    private FSM myFSM;
    private GameObject _helixBall;
    
    private void Start() {
        SetupFSM();
        mainCamera.target = spireStructure.transform;
        playerLives = 3;
        helixBallVector = new Vector3(0f,0f,-1.7f);
    }

    private void SetupFSM(){
        myFSM = GetComponent<FSM>();
        State[] myStatearray = GetComponents<State>();
        foreach (State state in myStatearray)
        {
            myFSM.Add(state.GetType(), state);
        }
        myFSM.SetCurrentState(typeof(MainMenuState));
    }

    public void SpawnHelixBall(){
        _helixBall = Instantiate(helixBall, helixBallVector, helixBall.transform.rotation);
        _helixBall.GetComponent<BallScript>().gameManager = this;
        _helixBall.GetComponent<BallScript>().hasHit = false;
        mainCamera.target = _helixBall.transform;
    }
    
    public void SpawnSpireStructures(){
        /* SpawnSpireStructures
            - Create helix by spawning SpireStructures
            - Create Win SpireStructure at the bottom of helix
        */
        for(int i =1; i <= totalSpireCount; i++){
            CreateSpireStructure();
        }
        CreateWinSpireStructure();
        levelCounter += 1;
        currentPlayerLevelUI.text = "Lvl: " + levelCounter;
    }

    private void CreateSpireStructure(){  
        /* CreateSpireStructure
            - Spawn a SpireStructure
            - Add that SpireStructure to SpireStructure list
            - Assign spawnParent as parent of that SpireStructure
            - Call DisapearRandomPart and assign the spireDisk child of SpireStructure
        */
        spireVector = new Vector3(spireVector.x,  spireVector.y - Random.Range(3f, 5f), spireVector.z);
        GameObject newSpire = Instantiate(spireStructure.gameObject, spireVector, spireStructure.gameObject.transform.rotation, spawnParent.transform);
        DisapearRandomPart(newSpire.transform.GetChild(0).gameObject);
        spireStructureList.Add(newSpire);
    }

    private void DisapearRandomPart(GameObject mySpire){
        /* DisapearRandomPart
            - Pick random child from spireDisk
            - Set random child inactive in scene
            - Call SpawnExitPiece and assign random child
        */
        randomNum = Random.Range(0,mySpire.transform.childCount);
        randomNum2 = Random.Range(0,mySpire.transform.childCount);
        randomNum3 = Random.Range(0,mySpire.transform.parent.childCount);
        mySpire.transform.GetChild(randomNum).gameObject.SetActive(false);
        SpawnExitPiece(mySpire.transform.GetChild(randomNum).gameObject, mySpire);

        if(randomNum != randomNum2){
            SpawnBadStructureParts(mySpire);
        }
        if(randomNum3 != randomNum2 && randomNum3 != randomNum){
            SpawnPowerUp(mySpire.transform.GetChild(randomNum3).gameObject);
        }
    }

    private void SpawnPowerUp(GameObject mySpirePiece){
        int randomStructure = Random.Range(0, 6);
        int randomPowerup = Random.Range(0,powerupList.Count);
        Vector3 PowerupVector = new Vector3(mySpirePiece.transform.position.x, mySpirePiece.transform.position.y + 0.2f, mySpirePiece.transform.position.z + 1.2f);
        
        Instantiate(powerupList[randomPowerup].gameObject, PowerupVector, transform.rotation, mySpirePiece.transform.parent);
    }

    private void SpawnBadStructureParts(GameObject mySpire){
       if(levelCounter >= 3){
            mySpire.transform.GetChild(randomNum2).gameObject.SetActive(false);
            BadStructurePart = new Vector3(spireVector.x, spireVector.y, spireVector.x);
            Instantiate(spireDamagePiece.gameObject, BadStructurePart, mySpire.transform.GetChild(randomNum2).transform.rotation, mySpire.transform);
        }
    }

    private void SpawnExitPiece(GameObject missingPiece, GameObject _spireParent){
        /* SpawnExitPiece
            - Instantiate spirePiece in place of random child
        */
        ExitPieceSpirevector3 = new Vector3(spireVector.x, spireVector.y - 0.6f, spireVector.z);
        Instantiate(spirePiece.gameObject, ExitPieceSpirevector3, missingPiece.transform.rotation, _spireParent.transform);
    }

    private void CreateWinSpireStructure(){
        /* CreateWinSpireStructure
            - Spawn winTile
            - add winTile to spireStructureList
        */
        spireVector = new Vector3(spireVector.x, spireVector.y - 3f, spireVector.z);
        GameObject _winStructure = Instantiate(winStructure.gameObject, spireVector, spireStructure.gameObject.transform.rotation, spawnParent.transform);
        spireStructureList.Add(_winStructure);
    }

    private IEnumerator WipeStructureList(float waitTime){
        foreach(GameObject structure in spireStructureList){
            Destroy(structure.gameObject);
        }
        spireStructureList.Clear();
        yield return new WaitForSeconds(waitTime);
    }

    public void WipeCurrentLevel(){
        StartCoroutine(WipeStructureList(1f));
        spireVector.y = 0f;
        mainCamera.target = spireStructure.transform;
        Destroy(_helixBall.gameObject);
    }

    public void PlayerWonLevel(){
        myFSM.SetCurrentState(typeof(WinState));
        myAudioManager.PlaySFX(myAudioManager.win);
    }

    public void Loselive(){
        playerLives -= 1;
        StartCoroutine(ShowLostLivesText(2f));
        if(playerLives <= 0){
            myFSM.SetCurrentState(typeof(LoseState));
            myAudioManager.PlaySFX(myAudioManager.death);
            myAudioManager.PlayBgMusic();
            return;
        }
        lostLiveTextUI.text = "-1 Live";
        playerLivesUI.text = "Lives: " + playerLives;
        LosePlayerPoints(totalPlayerPoints);
        myAudioManager.PlaySFX(myAudioManager.death);
        mainCamera.target = spireStructure.transform;
        Destroy(_helixBall);
        SpawnHelixBall();
    }

    public void GoToNextLevel(){
        totalSpireCount += 2;
        myFSM.SetCurrentState(typeof(GameState));
    }

    public void ReturnToMainMenu(){
        WipePlayerStats();
        myFSM.SetCurrentState(typeof(MainMenuState));
    }

    public void OpenMainMenu(){
        UIMainMenu.SetActive(true);
        UIOptionsMenu.SetActive(false);
    }

    public void OpenOptionsMenu(){
        UIMainMenu.SetActive(false);
        UIOptionsMenu.SetActive(true);
    }

    public void WipePlayerStats(){
        totalSpireCount = 1;
        playerLives = 3;
        totalPlayerPoints = 0;
        levelCounter = 0;
    }

    public void AddPlayerPoints(int wonPlayerPoints){
        myAudioManager.PlaySFX(myAudioManager.whoosh);
        totalPlayerPoints += wonPlayerPoints;
        StartCoroutine(ShowPointsText(2f, wonPlayerPoints));
        gameStatePlayerScoreUI.text = "Score: " + totalPlayerPoints;
        winStatePlayerScoreUI.text = "Total Score: " + totalPlayerPoints;
        loseStatePlayerScoreUI.text = "Total Score: " + totalPlayerPoints;
    }

    public void LosePlayerPoints(int pointCost){
        totalPlayerPoints -= pointCost;
        gameStatePlayerScoreUI.text = "Score: " + totalPlayerPoints;
        winStatePlayerScoreUI.text = "Total Score: " + totalPlayerPoints;
        loseStatePlayerScoreUI.text = "Total Score: " + totalPlayerPoints;
    }

    public void AddPlayerLives(int extraLives){
        myAudioManager.PlaySFX(myAudioManager.powerupTouch);
        if(playerLives < 2){
            playerLives += extraLives;
        }
        playerLivesUI.text = "Lives: " + playerLives + "/3";
        StartCoroutine(ShowUIText(2f, extraLives));
    }

    private IEnumerator ShowUIText(float time, int num){
        comboTextUI.gameObject.SetActive(true);
        comboTextUI.text = "+" + num + " Lives";
        yield return new WaitForSeconds(time);
        comboTextUI.gameObject.SetActive(false);
    }

    private IEnumerator ShowPointsText(float time, int num){
        comboTextUI.gameObject.SetActive(true);
        comboTextUI.text = "+" + num + " Points";
        yield return new WaitForSeconds(time);
        comboTextUI.gameObject.SetActive(false);
    }

    private IEnumerator ShowLostLivesText(float time){
        lostLiveTextUI.gameObject.SetActive(true);
        lostLiveTextUI.text = "-" + 1 + " Life";
        yield return new WaitForSeconds(time);
        lostLiveTextUI.gameObject.SetActive(false);
    }
}
