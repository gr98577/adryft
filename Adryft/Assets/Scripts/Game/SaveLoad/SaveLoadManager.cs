using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager {

	public static void SavePlayer(playerController pc)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/timestamp.asav", FileMode.Create);

        PlayerData Pdata = new PlayerData(pc);
        bf.Serialize(stream, Pdata);

        stream.Close();
    }

    public static void LoadPlayer(playerController pc)
    {
        if (File.Exists(Application.persistentDataPath + "/timestamp.asav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/timestamp.asav", FileMode.Open);

            PlayerData data = bf.Deserialize(stream) as PlayerData;

            stream.Close();
            data.LoadPlayer(pc);
        }
    }
}

[Serializable]
public class PlayerData
{
    public int p_ammo;
    public int p_maxAmmo;
    public float p_maxStamina;

    public int d_healh;
    public int d_maxHealth;

    public PlayerData(playerController pc)
    {
        pc.SaveTo(this);
    }

    public void LoadPlayer(playerController pc)
    {
        pc.LoadFrom(this);
    }
}