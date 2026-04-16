using QuantityMeasurementApp.Model.Entities;

namespace QuantityMeasurementApp.Repository.Interfaces
{
    /// <summary>
    /// Repository interface for quantity history operations
    /// </summary>
    public interface IQuantityHistoryRepository
    {
        // CRUD Operations
        void AddRecord(QuantityHistoryRecord record);
        List<QuantityHistoryRecord> GetAllRecords();
        QuantityHistoryRecord GetRecordById(int id);
        List<QuantityHistoryRecord> GetRecordsByCategory(string category);
        List<QuantityHistoryRecord> GetRecordsByOperationType(string operationType);
        bool DeleteRecord(int id);
        int DeleteAllRecords();
        int GetRecordCount();

        // Cache Management
        void ClearCache();
        void RefreshCache();
        
        // Resource Management
        void Close();
    }
}