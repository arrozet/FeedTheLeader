using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class AleatoryEventsTests
{
    [Test]
    public void OnMouseDown_AddsPoints()
    {
        // Arrange
        var pointsManager = new GameObject().AddComponent<PointsManager>(); // Simulamos PointsManager
        pointsManager.Initialize(); // Inicializamos PointsManager

        var bonusPoints = new GameObject().AddComponent<BonusPoints>();

        // Act
        bonusPoints.OnMouseDown();

        // Assert
        Assert.GreaterOrEqual(pointsManager.getPuntos(), 10); // Verificamos que los puntos se hayan incrementado al menos en 10
    }
}
