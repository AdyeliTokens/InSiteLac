using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KPICollection
{
    public List<KPI> valores;

    public static KPICollection CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<KPICollection>(jsonString);

    }

}

[System.Serializable]
public class KPI
{
    public int Id;
    public string Description;
    public Double YTD;
    public int Mes_Efectivo;
    public int Anio_Efectivo;
    public Double? porcentaje;


    public static KPI CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<KPI>(jsonString);

    }

}