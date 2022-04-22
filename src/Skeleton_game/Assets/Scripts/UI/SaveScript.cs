using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// static is responsible for making it impossible to create multiple instances of this class
public static class SaveScript 
{
    
    public static void SaveSkillTree(SkillTree skillTree)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/skillTree.dfk";
        FileStream stream = new FileStream(path, FileMode.Create);

        SkillTreeData data = new SkillTreeData(skillTree);

        formatter.Serialize(stream, data);
        stream.Close();
    }

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
}
