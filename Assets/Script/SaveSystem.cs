using UnityEngine;
using System.IO;

public static class SaveSystem
{
    public static void SavePlayer(int lvlToSave)
    {
        string path = Application.persistentDataPath + "/player_data.dat";

        using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
        {
            writer.Write(lvlToSave);
        }

        Debug.Log("Nível " + lvlToSave + " salvo com sucesso!");
    }

    public static int LoadLevel()
    {
        string path = Application.persistentDataPath + "/player_data.dat";

        if (File.Exists(path))
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                return reader.ReadInt32();
            }
        }
        else
        {
            Debug.Log("Arquivo de salvamento não encontrado em: " + path);
            return 0;
        }
    }
}
