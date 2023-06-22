using Grindar.SaveLoad;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameHandler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Load();
        }
    }

    public void Save()
    {
        //Carregando o ultimo save, caso não exista é criado um save vazio por padrão
        GameSaveObject gameSave = BinarySave.Load<GameSaveObject>(BinarySave.DEFAULT_GAME_SAVE);

        //Configurando uma instância individual
        PlayerSave player = new PlayerSave
        {
            Name = $"Nome {Random.Range(0, 1000)}",
            Life = Random.Range(0, 100),
            LastPosition = BinarySave.SerializeVector(transform.position),
            LastEulerAngles = BinarySave.SerializeVector(transform.eulerAngles),
            LastRotation = BinarySave.SerializeRotation(transform.rotation)
        };
        gameSave.Player = player;


        //Configurando múltiplas instâncias que precisam ser instâncias na cena
        List<PlayerSave> players = new List<PlayerSave>();


        //Configurando múltiplas instâncias que existem na hierarquia 
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        Dictionary<string, CheckPointSave> checkpointSaves = new Dictionary<string, CheckPointSave>();

        foreach (Checkpoint checkpoint in checkpoints)
        {
            string GUID = checkpoint.GetComponent<PersistData>().GUID;
            CheckPointSave checkPointSave = new CheckPointSave();
            checkPointSave.isActived = checkpoint.IsActived;
            checkpointSaves.Add(GUID, checkPointSave);
        }
        gameSave.Checkpoints = checkpointSaves;

        //Salvando os dados modificados
        BinarySave.Save(BinarySave.DEFAULT_GAME_SAVE, gameSave);
    }

    public void Load()
    {
        //Carregando o ultimo save, caso não exista é criado um save vazio por padrão
        GameSaveObject gameSave = BinarySave.Load<GameSaveObject>(BinarySave.DEFAULT_GAME_SAVE);

        //Configurando e aplicando os dados gravados em uma instância simples
        PlayerSave playerSave = gameSave.Player;

        //Configurando e aplicando os dados gravados em múltiplas instãncias que precisam ser criadas   
        foreach (var player in gameSave.Players)
        {
            //Instanciar player adicionado os dados gravados
        }
        
        //Configurando e aplicando os dados gravados nas múltiplas instância que estão na hierarquia
        Checkpoint[] checkpoints = FindObjectsOfType<Checkpoint>();
        for (int i = 0; i < gameSave.Checkpoints.Count; i++)
        {
            if (checkpoints[i].GetComponent<PersistData>().GUID == gameSave.Checkpoints.ElementAt(i).Key)
            {
                checkpoints[i].IsActived = gameSave.Checkpoints.ElementAt(i).Value.isActived;
            }
        }
    }
}
