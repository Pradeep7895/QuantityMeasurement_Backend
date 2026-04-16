using System;

namespace QuantityMeasurementApp.Model.Entities
{
    public class QuantityHistoryRecord
    {
        public int Id { get; set; }
        public string? Category { get; set; }
        public string? OperationType { get; set; }
        public double? FirstValue { get; set; }
        public string? FirstUnit { get; set; }
        public double? SecondValue { get; set; }
        public string? SecondUnit { get; set; }
        public string? TargetUnit { get; set; }
        public double? ResultValue { get; set; }
        public string? ResultUnit { get; set; }
        public string? ErrorMessage { get; set; }
        public int? ExecutionTimeMs { get; set; }  
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }      

        public override string ToString()
        {
            return $"[{Id}] {OperationType}: {FirstValue} {FirstUnit} => {ResultValue} {ResultUnit}";
        }
    }
}