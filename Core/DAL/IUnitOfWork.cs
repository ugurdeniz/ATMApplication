﻿using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DAL
{
    public interface IUnitOfWork
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, new();
        void BeginTran();
        void CommitTran();
        void RollBackTran();
        void Dispose();
    }
}
