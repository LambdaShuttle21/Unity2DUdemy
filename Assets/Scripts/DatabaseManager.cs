using System;
using System.IO;
using UnityEngine;
using SQLite4Unity3d;
using System.Collections.Generic;
using System.Linq;

public class DatabaseManager : MonoBehaviour
{
    public static DatabaseManager Instance;

    private SQLiteConnection connection;
    private string dbPath;

    private void Awake()
    {

        Debug.Log("AWAKE DATABASE MANAGER");

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        dbPath = Path.Combine(Application.persistentDataPath, "gameData.db");
        connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);

        connection.CreateTable<PartidaData>();

        Debug.Log("Base de datos creada en: " + dbPath);
    }

    public void GuardarPartida(float tiempo, int kills, string resultado)
    {
        PartidaData partida = new PartidaData
        {
            Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Tiempo = tiempo,
            Kills = kills,
            Resultado = resultado
        };

        connection.Insert(partida);
        Debug.Log("Partida guardada correctamente");

        MostrarPartidasEnConsola();
    }
    public List<PartidaData> ObtenerTodasLasPartidas()
    {
        return connection.Table<PartidaData>().OrderByDescending(p => p.Id).ToList();
    }

    public PartidaData ObtenerMejorPartida()
    {
        return connection.Table<PartidaData>()
                         .OrderByDescending(p => p.Kills)
                         .FirstOrDefault();
    }
    // Method testing in console
    public void MostrarPartidasEnConsola()
    {
        List<PartidaData> partidas = ObtenerTodasLasPartidas();

        foreach (PartidaData partida in partidas)
        {
            Debug.Log($"ID: {partida.Id} | Fecha: {partida.Fecha} | Tiempo: {partida.Tiempo} | Kills: {partida.Kills} | Resultado: {partida.Resultado}");
        }
    }
}

