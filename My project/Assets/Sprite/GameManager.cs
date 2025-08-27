using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Player player;
    public int skillpoints = 0;
    public string savePlayerPath;
    public string saveBagPath;
    public bool isATKUP = false;
    public bool isHPUP = false;
    public bool isSpeedUP = false;
    public bool isSkillUP = false;
    public bool isEffectUP = false;

    public bool isCanSkill1 = false;
    public bool isCanSkill2 = false;
    public bool isCanSkill3 = false;
    public bool isCanSkill4 = false;
    public bool isCanSkill5 = false;

    public Dictionary<string, int> bagItems = new Dictionary<string, int>();
    public int HPBottleNum = 5;
    public int HHPBottleNum = 5;

    public bool handsisEquipment = false;
    public string handsEquipmentName = "";
    public int Sword1Num = 1;
    public int Sword2Num = 1;

    public bool isGetTask1 = false;
    public bool task1IsFinished = false;

    public Vector3 playerPos = new Vector3(-45.7f, 0.27f, 0f);

    public int coin = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        savePlayerPath = Path.Combine(Application.persistentDataPath, "RPGGamePlayerSave.txt");
        saveBagPath = Path.Combine(Application.persistentDataPath, "RPGGameBagSave.txt");
    }

    public void SaveGame()
    {
        playerPos = player.transform.position;
        string saveData = $"{skillpoints},{isATKUP},{isHPUP},{isSpeedUP},{isSkillUP}," +
            $"{isEffectUP},{isCanSkill1},{isCanSkill2},{isCanSkill3},{isCanSkill4},{isCanSkill5}," +
            $"{handsisEquipment},{handsEquipmentName},{playerPos.x},{playerPos.y},{playerPos.z},{coin}";
        File.WriteAllText(savePlayerPath, saveData);
        SaveBag();
    }

    public void LoadGame() 
    {
        string saveData = File.ReadAllText(savePlayerPath);
        string[] data = saveData.Split(',');
        skillpoints = int.Parse(data[0]);
        isATKUP = bool.Parse(data[1]);
        isHPUP = bool.Parse(data[2]);
        isSpeedUP = bool.Parse(data[3]);
        isSkillUP = bool.Parse(data[4]);
        isEffectUP = bool.Parse(data[5]);
        isCanSkill1 = bool.Parse(data[6]);
        isCanSkill2 = bool.Parse(data[7]);
        isCanSkill3 = bool.Parse(data[8]);
        isCanSkill4 = bool.Parse(data[9]);
        isCanSkill5 = bool.Parse(data[10]);
        handsisEquipment = bool.Parse(data[11]);
        handsEquipmentName = data[12];
        playerPos.x = float.Parse(data[13]);
        playerPos.y = float.Parse(data[14]);
        playerPos.z = float.Parse(data[15]);
        coin = int.Parse(data[16]);
        LoadBag();
    }

    public void SaveBag()
    {
        string saveData = $"{HPBottleNum},{HHPBottleNum},{Sword1Num},{Sword2Num}";
        File.WriteAllText(saveBagPath, saveData);
    }

    public void LoadBag()
    {
        string saveData = File.ReadAllText(saveBagPath);
        string[] data = saveData.Split(',');
        HPBottleNum = int.Parse(data[0]);
        HHPBottleNum = int.Parse(data[1]);
        Sword1Num = int.Parse(data[2]);
    }
}
