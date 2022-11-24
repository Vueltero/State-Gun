using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum estado {solido = 1, liquido, gaseoso};
    public estado estadoActual = estado.solido;

    private bool inLevel = false;

    public static GameManager instance;
    
    public GameObject menu, game, level1, level2, level3, level4, level5, credits, levelSelect;

    public Sprite botonApagado, botonPrendido, solido, liquido, gaseoso, puertaAbierta, puertaCerrada;

    public int levelActual = 0;

    public int levelUnlocked;
    public Sprite lvlLocked, lvl2Unlocked, lvl3Unlocked, lvl4Unlocked, lvl5Unlocked;
    public GameObject lvl2Obj, lvl3Obj, lvl4Obj, lvl5Obj;

    public GameObject level1Container, level2Container, level3Container, level4Container, level5Container;

    public TextMeshProUGUI txtarma;

    private void SaveData() => PlayerPrefs.SetInt("levelUnlocked", levelUnlocked);

    private void LoadData() => levelUnlocked = PlayerPrefs.GetInt("levelUnlocked", 1);

    public void SelectLevel(int id)
    {
        levelSelect.SetActive(false);
        game.SetActive(true);
        inLevel = true;
        levelActual = id;

        level1Container.SetActive(false);
        level2Container.SetActive(false);
        level3Container.SetActive(false);
        level4Container.SetActive(false);
        level5Container.SetActive(false);

        switch (id)
        {
            case 1: level1Container.SetActive(true); break;
            case 2: level2Container.SetActive(true); break;
            case 3: level3Container.SetActive(true); break;
            case 4: level4Container.SetActive(true); break;
            case 5: level5Container.SetActive(true); break;
        }
    }

    public void NextLevel(int id)
    {
        //sfx de q paso el nivel?

        RestartCurrentLevel();

        levelUnlocked = id;
        SaveData();

        levelActual = id;
        
        switch (id)
        {
            case 2:
                level1Container.SetActive(false);
                level2Container.SetActive(true);
                break;
            case 3:
                level2Container.SetActive(false);
                level3Container.SetActive(true);
                break;
            case 4:
                level3Container.SetActive(false);
                level4Container.SetActive(true);
                break;
            case 5:
                level4Container.SetActive(false);
                level5Container.SetActive(true);
                break;
            case 6:
                //Gano el juego
                GoToMenu();
                break;
        }
    }

    void Start()
    {
        instance = this;
        FindObjectOfType<AudioManager>().Play("song1");
        FindObjectOfType<AudioManager>().Loop("song1", true);
        LoadData();
    }

    public void EnterGame()
    {
        FindObjectOfType<AudioManager>().Play("jugarClick");
        menu.SetActive(false);
        game.SetActive(true);
        inLevel = true;
        levelActual = 1;
    }

    public void GoToMenu()
    {
        FindObjectOfType<AudioManager>().Play("jugarClick");
        RestartCurrentLevel();
        level1Container.SetActive(true);
        level2Container.SetActive(false);
        level3Container.SetActive(false);
        level3Container.SetActive(false);
        level5Container.SetActive(false);
        game.SetActive(false);
        menu.SetActive(true);
        inLevel = false;
    }

    public void GoBackFromLevelSelect()
    {
        FindObjectOfType<AudioManager>().Play("jugarClick");
        levelSelect.SetActive(false);
        menu.SetActive(true);
    }

    public void GoToLevelSelect()
    {
        FindObjectOfType<AudioManager>().Play("jugarClick");
        menu.SetActive(false);
        levelSelect.SetActive(true);
        switch (levelUnlocked)
        {
            case 2:
                lvl2Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl2Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                break;
            case 3:
                lvl2Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl2Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl3Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl3Unlocked;
                lvl3Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl3Unlocked;
                break;
            case 4:
                lvl2Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl2Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl3Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl3Unlocked;
                lvl3Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl3Unlocked;
                lvl4Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl4Unlocked;
                lvl4Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl4Unlocked;
                break;
            case 5:
                lvl2Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl2Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl2Unlocked;
                lvl3Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl3Unlocked;
                lvl3Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl3Unlocked;
                lvl4Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl4Unlocked;
                lvl4Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl4Unlocked;
                lvl5Obj.transform.Find("Source/Normal/Icon").gameObject.GetComponent<Image>().sprite = lvl5Unlocked;
                lvl5Obj.transform.Find("Source/Active/Icon").gameObject.GetComponent<Image>().sprite = lvl5Unlocked;
                break;
        }
    }

    public void ExitGame()
    {
        FindObjectOfType<AudioManager>().Play("jugarClick");
        Debug.Log("Exiting");
        Application.Quit();
    }

    void Update()
    {
        if (inLevel)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) //1
            {
                FindObjectOfType<AudioManager>().Play("switchLiquid");
                estadoActual = estado.solido;
                txtarma.text="Arma Solida";
            }

            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) //2
            {
                FindObjectOfType<AudioManager>().Play("switchLiquid");
                estadoActual = estado.liquido;
                txtarma.text="Arma Liquida";
            }

            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) //3
            {
                FindObjectOfType<AudioManager>().Play("switchLiquid");
                estadoActual = estado.gaseoso;
                txtarma.text="Arma Gaseosa";
            }
        }
        if (levelActual > 0)
            if (Input.GetKeyDown(KeyCode.R))
                RestartCurrentLevel();
    }

    public void RestartCurrentLevel()
    {
        GameObject newLevel;
        switch (levelActual)
        {
            case 1:
                Destroy(GameObject.Find("Level 1"));
                newLevel = Instantiate(level1, new Vector3(0f, 0f, 0f), transform.rotation);
                newLevel.transform.SetParent(GameObject.Find("Game/level" + levelActual + "Container").transform, false);
                newLevel.name = "Level 1";
                break;
            case 2:
                Destroy(GameObject.Find("Level 2"));
                newLevel = Instantiate(level2, new Vector3(0f, 0f, 0f), transform.rotation);
                newLevel.transform.SetParent(GameObject.Find("Game/level" + levelActual + "Container").transform, false);
                newLevel.name = "Level 2";
                break;
            case 3:
                Destroy(GameObject.Find("Level 3"));
                newLevel = Instantiate(level3, new Vector3(0f, 0f, 0f), transform.rotation);
                newLevel.transform.SetParent(GameObject.Find("Game/level" + levelActual + "Container").transform, false);
                newLevel.name = "Level 3";
                break;
            case 4:
                Destroy(GameObject.Find("Level 4"));
                newLevel = Instantiate(level4, new Vector3(0f, 0f, 0f), transform.rotation);
                newLevel.transform.SetParent(GameObject.Find("Game/level" + levelActual + "Container").transform, false);
                newLevel.name = "Level 4";
                break;
            case 5:
                Destroy(GameObject.Find("Level 5"));
                newLevel = Instantiate(level5, new Vector3(0f, 0f, 0f), transform.rotation);
                newLevel.transform.SetParent(GameObject.Find("Game/level" + levelActual + "Container").transform, false);
                newLevel.name = "Level 5";
                break;
        }
    }
}
