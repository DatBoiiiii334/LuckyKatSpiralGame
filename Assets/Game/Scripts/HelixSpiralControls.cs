using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HelixSpiralControls : MonoBehaviour
{public GameObject spireStructure;
    public GameObject winStructure;
    public GameObject spawnParent;
    public GameObject spirePiece;
    public GameObject spireDamagePiece;
    public GameObject helixBall;
    public CameraFollow MainCamera;
    public Vector3 helixBallVector;

    public int totalPlayerPoints; // make this secure with get 
    
    public List<GameObject> spireStructureList;

    [SerializeField]
    private int totalSpireCount;

    [SerializeField]
    private TMP_Text GameStatePlayerScoreUI;
    [SerializeField]
    private TMP_Text WinStatePlayerScoreUI;
    [SerializeField]
    private TMP_Text LoseStatePlayerScoreUI; 
    [SerializeField]
    private TMP_Text CurrentPlayerLevelUI;            

    public int levelCounter;
    private int randomNum;
    private int randomNum2;
    public int playerLives;
    private Vector3 spireVector;
    private Vector3 ExitPieceSpirevector3;
    private Vector3 BadStructurePart;
    private FSM myFSM;
    private GameObject _helixBall;
    private GameManager _gameManager;

    private void Start() {
        _gameManager = GetComponent<GameManager>();
    }
    

    public void SpawnHelixBall(){
        _helixBall = Instantiate(helixBall, helixBallVector, helixBall.transform.rotation);
        _helixBall.GetComponent<BallScript>().gameManager = _gameManager;
        _helixBall.GetComponent<BallScript>().hasHit = false;
        MainCamera.target = _helixBall.transform;
    }
    
    public void SpawnSpireStructures(){
        for(int i =1; i <= totalSpireCount; i++){
            CreateSpireStructure();
        }
        CreateWinSpireStructure();
        levelCounter += 1;
        CurrentPlayerLevelUI.text = "Lvl: " + levelCounter;
    }

    private void CreateSpireStructure(){  
        spireVector = new Vector3(spireVector.x,  spireVector.y - Random.Range(3f, 5f), spireVector.z);
        GameObject newSpire = Instantiate(spireStructure.gameObject, spireVector, spireStructure.gameObject.transform.rotation, spawnParent.transform);
        DisapearRandomPart(newSpire.transform.GetChild(0).gameObject);
        spireStructureList.Add(newSpire);
    }

    private void DisapearRandomPart(GameObject mySpire){
        randomNum = Random.Range(0,mySpire.transform.childCount);
        mySpire.transform.GetChild(randomNum).gameObject.SetActive(false);
        SpawnExitPiece(mySpire.transform.GetChild(randomNum).gameObject, mySpire);

        //SpawnBadStructureParts(mySpire);
        StartCoroutine(_SpawnRedParts(1f, mySpire, randomNum));
    }

    private void SpawnBadStructureParts(GameObject mySpire){
    //    if(levelCounter >= 3){
    //         randomNum2 = Random.Range(0,mySpire.transform.childCount);
    //         if(randomNum != randomNum2){
    //             mySpire.transform.GetChild(randomNum2).gameObject.SetActive(false);
    //             BadStructurePart = new Vector3(spireVector.x, spireVector.y, spireVector.x);
    //             Instantiate(spireDamagePiece.gameObject, BadStructurePart, mySpire.transform.GetChild(randomNum2).transform.rotation, mySpire.transform);
    //         }else if(randomNum == randomNum2){
    //             SpawnBadStructureParts(mySpire);
    //             Debug.Log("ERROR happend to " + mySpire.name + " num: " + randomNum2);
    //         }
    //     }
    }

    private IEnumerator _SpawnRedParts(float waitTime, GameObject mySpire, int alreadypickedNum){
        if(levelCounter >= 3){
            randomNum2 = Random.Range(0,mySpire.transform.childCount);
            if(randomNum2 != alreadypickedNum){
                mySpire.transform.GetChild(randomNum2).gameObject.SetActive(false);
                BadStructurePart = new Vector3(spireVector.x, spireVector.y, spireVector.x);
                Instantiate(spireDamagePiece.gameObject, BadStructurePart, mySpire.transform.GetChild(randomNum2).transform.rotation, mySpire.transform);
            }
        }
        yield return new WaitForSeconds(waitTime);
    }

    private void SpawnExitPiece(GameObject missingPiece, GameObject _spireParent){
        ExitPieceSpirevector3 = new Vector3(spireVector.x, spireVector.y - 0.6f, spireVector.x);
        Instantiate(spirePiece.gameObject, ExitPieceSpirevector3, missingPiece.transform.rotation, _spireParent.transform);
    }

    private void CreateWinSpireStructure(){
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
        MainCamera.target = spireStructure.transform;
        Destroy(_helixBall.gameObject);
    }
}
