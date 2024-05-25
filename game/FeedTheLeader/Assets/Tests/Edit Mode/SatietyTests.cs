using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SatietyManagerTests
{
    private SatietyManager satietyManager;
    private Slider slider;
    private TMP_Text textoPorcentaje;
    private Button boton;

    [SetUp]
    public void Setup()
    {
        GameObject gameObject = new GameObject();
        satietyManager = gameObject.AddComponent<SatietyManager>();
        slider = gameObject.AddComponent<Slider>();
        textoPorcentaje = gameObject.AddComponent<TMP_Text>();
        boton = gameObject.AddComponent<Button>();

        satietyManager.Slider = slider;
        satietyManager.textoPorcentaje = textoPorcentaje;
        satietyManager.boton = boton;
        satietyManager.act = 0;
        satietyManager.max = 100;
    }

    [Test]
    public void Resta_DecreasesClicks()
    {
        // Arrange
        satietyManager.Clics = 5;

        // Act
        satietyManager.Resta();

        // Assert
        Assert.AreEqual(4, satietyManager.Clics);
    }

    [Test]
    public void RestaNegativa_DecreasesClicks()
    {
        // Arrange
        satietyManager.Clics = 0;

        // Act
        satietyManager.Resta();

        // Assert
        Assert.AreEqual(0, satietyManager.Clics);
    }


    [Test]
    public void RegistrarClic_IncreasesClicks()
    {
        // Act
        satietyManager.RegistrarClic();

        // Assert
        Assert.AreEqual(1, satietyManager.Clics);
    }


}