using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StatsTests
{
    private WriteStats writeStats;

    [SetUp]
    public void SetUp()
    {
        // Inicializar la clase que será testeada
        writeStats = new WriteStats();
    }

    [Test]
    public void TestInicializacion()
    {
        // Verificar que al inicio todos los valores están en cero
        Assert.Zero(writeStats.dias);
        Assert.Zero(writeStats.horas);
        Assert.Zero(writeStats.minutos);
        Assert.Zero(writeStats.segundosRestantes);

        Assert.Zero(writeStats.PuntosActuales);
        Assert.Zero(writeStats.PuntosAcumulados);
        Assert.Zero(writeStats.PuntosPorSegundo);
        Assert.Zero(writeStats.PuntosPorClick);
        Assert.Zero(writeStats.EventosClicados);
        Assert.Zero(writeStats.TiempoJugado);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void ConvertirTiempo_CorrectlyConvertsSeconds()
    {
        // Arrange
        double segundos = 90061; // 1 day, 1 hour, 1 minute, 1 second

        // Act
        writeStats.ConvertirTiempo(segundos);

        // Assert
        Assert.AreEqual(1, writeStats.dias);
        Assert.AreEqual(1, writeStats.horas);
        Assert.AreEqual(1, writeStats.minutos);
        Assert.AreEqual(1, writeStats.segundosRestantes);
    }

    [Test]
    public void FormatScore_LessThanMillion()
    {
        // Arrange
        double numero = 999999;

        // Act
        var result = WriteStats.formatScore(numero);

        // Assert
        Assert.AreEqual("999.999", result.Item1);
        Assert.AreEqual("", result.Item2);
    }

    [Test]
    public void FormatScore_MoreThanMillion()
    {
        // Arrange
        double numero = 1234567890;

        // Act
        var result = WriteStats.formatScore(numero);

        // Assert
        Assert.AreEqual("1.234,568 ", result.Item1);
        Assert.AreEqual("millones", result.Item2);
    }

    [Test]
    public void AnalizarNumero_ZeroDecimals()
    {
        // Arrange
        double numero = 123.000;

        // Act
        int result = WriteStats.AnalizarNumero(numero);

        // Assert
        Assert.AreEqual(0, result);
    }

    [Test]
    public void AnalizarNumero_OneDecimal()
    {
        // Arrange
        double numero = 123.400;

        // Act
        int result = WriteStats.AnalizarNumero(numero);

        // Assert
        Assert.AreEqual(1, result);
    }

    [Test]
    public void AnalizarNumero_TwoDecimals()
    {
        // Arrange
        double numero = 123.450;

        // Act
        int result = WriteStats.AnalizarNumero(numero);

        // Assert
        Assert.AreEqual(2, result);
    }

    [Test]
    public void AnalizarNumero_ThreeDecimals()
    {
        // Arrange
        double numero = 123.456;

        // Act
        int result = WriteStats.AnalizarNumero(numero);

        // Assert
        Assert.AreEqual(3, result);
    }
}
