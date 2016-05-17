using System;
using System.Collections.Generic;

namespace ClearCode.Data.Entities
{
    public class EVoteEntities : IDisposable
    {
        public List<Stat> StatEntities { get; set; }
        public List<UserEntity> UserEntities { get; set; }
        public List<AnswerEntity> AnswerEntities { get; set; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}