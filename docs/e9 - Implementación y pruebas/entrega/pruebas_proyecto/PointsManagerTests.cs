using NUnit.Framework;
using UnityEngine;

public class PointsManagerTests
{
    private PointsManager pointsManager;

    [SetUp]
    public void Setup()
    {
        // Crea un nuevo GameObject simulado para PointsManager
        var gameObject = new GameObject();
        // Agrega el componente PointsManager al GameObject simulado
        pointsManager = gameObject.AddComponent<PointsManager>();
        // Inicializa el PointsManager
        pointsManager.Initialize();
    }

    [TearDown]
    public void Teardown()
    {
        // Destruye el GameObject simulado después de cada test
        Object.DestroyImmediate(pointsManager.gameObject);
    }

    [Test]
    public void AddPPs_IncreasesPointsPerSecond()
    {
        // Act
        pointsManager.AddPPs(10);

        // Assert
        Assert.AreEqual(10, pointsManager.getPointsPerSecond());
    }


    [Test]
    public void RestarPuntos_DecreasesCurrentScore()
    {
        // Arrange
        var pointsManager = new GameObject().AddComponent<PointsManager>(); // Simulamos PointsManager
        pointsManager.Initialize(); // Llamamos Awake manualmente para inicializar PointsManager
        pointsManager.SumarPuntos(100); // Agregamos puntos previamente

        // Act
        pointsManager.RestarPuntos(50);

        // Assert
        Assert.AreEqual(50, pointsManager.getPuntos()); // Verificamos que los puntos se hayan reducido a 50 después de restar 50
    }

    [Test]
    public void SumarPuntos_IncreasesCurrentScore()
    {
        // Arrange
        double initialScore = pointsManager.getPuntos();
        double puntosToAdd = 100;

        // Act
        pointsManager.SumarPuntos(puntosToAdd);

        // Assert
        Assert.AreEqual(initialScore + puntosToAdd, pointsManager.getPuntos());
    }

    [Test]
    public void RestarPuntos_ReturnsFalseIfScoreIsInsufficient()
    {
        // Arrange
        double initialScore = pointsManager.getPuntos();
        double puntosToSubtract = initialScore + 100;

        // Act
        bool result = pointsManager.RestarPuntos(puntosToSubtract);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(initialScore, pointsManager.getPuntos());
    }

    [Test]
    public void UpdateEventsClicked_IncreasesEventsClicked()
    {
        // Arrange
        double initialEventsClicked = pointsManager.getEventsClicked();
        int eventsToAdd = 1;

        // Act
        pointsManager.UpdateEventsClicked(eventsToAdd);

        // Assert
        Assert.AreEqual(initialEventsClicked + eventsToAdd, pointsManager.getEventsClicked());
    }

    [Test]
    public void ResetPoints_ResetsCurrentScoreAndPointsPerSecond()
    {
        // Arrange
        pointsManager.SumarPuntos(100);
        pointsManager.AddPPs(10);

        // Act
        pointsManager.ResetPoints();

        // Assert
        Assert.AreEqual(0, pointsManager.getPuntos());
        Assert.AreEqual(0, pointsManager.getPointsPerSecond());
    }

    [Test]
    public void Prestige_MultipliesPrestigeMultiplierAndResetsPoints()
    {
        // Arrange
        double initialScore = pointsManager.getPuntos();
        double initialMultiplier = pointsManager.getPrestigeMultiplier();
        double multiplierToMultiply = 2;

        // Act
        pointsManager.Prestige(multiplierToMultiply);

        // Assert
        Assert.AreEqual(initialMultiplier * multiplierToMultiply, pointsManager.getPrestigeMultiplier());
        Assert.AreEqual(0, pointsManager.getPuntos()); // Score should be reset after prestige
    }

    [Test]
    public void ResetPrestigeMultiplier_ResetsMultiplierToOne()
    {
        // Arrange
        double initialMultiplier = pointsManager.getPrestigeMultiplier();
        pointsManager.setPrestigeMultiplier(2);

        // Act
        pointsManager.ResetPrestigeMultiplier();

        // Assert
        Assert.AreEqual(1, pointsManager.getPrestigeMultiplier());
    }

    [Test]
    public void AddAlot_MultipliesCurrentScoreByOneHundred()
    {
        // Arrange
        double initialScore = pointsManager.getPuntos();
        double expectedScore = initialScore * 100;

        // Act
        pointsManager.AddAlot();

        // Assert
        Assert.AreEqual(expectedScore, pointsManager.getPuntos());
    }

}