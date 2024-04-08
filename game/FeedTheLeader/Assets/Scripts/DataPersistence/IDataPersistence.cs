//Autor: Edu
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
    void LoadData(GameData data);

    //Se pasa por referencia para que se guarden los cambios
    void SaveData(ref GameData data);
}
