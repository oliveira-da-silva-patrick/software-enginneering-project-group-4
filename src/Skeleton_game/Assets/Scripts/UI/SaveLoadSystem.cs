using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// static is responsible for making it impossible to create multiple instances of this class
public static class SaveLoadSystem
{

    public static void deleteSaveFile()
    {
        string path = Application.persistentDataPath + "/gameinfo.dfk";
        if (File.Exists(path)) { 
            File.Delete(path);
        }
    }

    // saves the skill tree's current state
    public static void SaveSkillTree()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/skillTree.dfk";
        FileStream stream = new FileStream(path, FileMode.Create);

        SkillTreeData data = new SkillTreeData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    // tries to load the skill tree and returns its data
    public static SkillTreeData LoadSkillTree()
    {
        string path = Application.persistentDataPath + "/skillTree.dfk";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream.Length > 0)
            {
                SkillTreeData data = formatter.Deserialize(stream) as SkillTreeData;
                stream.Close();
                return data;
            }
            stream.Close();
        }
        return null;
    }

    public static void SaveGameInfo()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/gameinfo.dfk";
        FileStream stream = new FileStream(path, FileMode.Create);

        GameData data = new GameData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameData LoadGameInfo()
    {
        string path = Application.persistentDataPath + "/gameinfo.dfk";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream.Length > 0)
            {
                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();
                return data;
            }
            stream.Close();
        }
        return null;
    }

    public static void SaveHealth()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/health.dfk";
        FileStream stream = new FileStream(path, FileMode.Create);

        HealthData data = new HealthData();

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static HealthData LoadHealth()
    {
        string path = Application.persistentDataPath + "/health.dfk";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            if (stream.Length > 0)
            {
                HealthData data = formatter.Deserialize(stream) as HealthData;
                stream.Close();
                return data;
            }
            stream.Close();
        }
        return null;
    }
}
