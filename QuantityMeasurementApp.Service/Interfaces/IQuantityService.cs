using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Entities;
using System.Collections.Generic;

namespace QuantityMeasurementApp.Service.Interfaces
{
    public interface IQuantityService
    {
        // Measurement Operations
        QuantityDTO Convert(QuantityDTO source, string targetUnit);
        bool Compare(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2);
        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit);
        double Divide(QuantityDTO q1, QuantityDTO q2);
        bool Validate(QuantityDTO quantity);

        // History Methods 
        List<QuantityHistoryRecord> GetHistory(string UserEmail);
        List<QuantityHistoryRecord> GetHistoryByCategory(string category);
        List<QuantityHistoryRecord> GetHistoryByOperationType(string operationType);
        QuantityHistoryRecord? GetHistoryRecordById(int id);
        bool DeleteHistoryRecord(int id);
        int DeleteAllHistoryRecords();
        int GetHistoryCount(string UserEmail);
        void ClearHistory();

        // Cache Management
        void ClearCache();
        void RefreshCache();
    }
}