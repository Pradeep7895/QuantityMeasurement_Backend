using QuantityMeasurementApp.Model.DTOs;

public class ArithmeticRequest
{
    public QuantityDTO Q1 { get; set; }
    public QuantityDTO Q2 { get; set; }
    public string? TargetUnit { get; set; }
}