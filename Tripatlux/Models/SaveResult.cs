using System;

namespace Tripatlux.Models
{
    public class SaveResult
    {
        public Guid Id { get; set; }
        public bool IsOk { get; set; }
        public Exception Exception { get; set; }

        public SaveResult() { }

        public SaveResult(bool res)
        {
            IsOk = res;
        }

        public SaveResult(bool res, Guid id)
        {
            IsOk = res;
            Id = id;
        }

        public SaveResult(Exception ex)
        {
            IsOk = false;
            Exception = ex;
        }
    }
}