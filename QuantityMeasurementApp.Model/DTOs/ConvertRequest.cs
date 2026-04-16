using QuantityMeasurementApp.Model.DTOs;

public class ConvertRequest
{
    public QuantityDTO Source { get; set; }
    public string TargetUnit { get; set; }
}