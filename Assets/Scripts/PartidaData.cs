using SQLite4Unity3d;

public class PartidaData
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Fecha { get; set; }
    public float Tiempo { get; set; }
    public int Kills { get; set; }
    public string Resultado { get; set; }
}