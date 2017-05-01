using System;
using System.Collections.Generic;
using LiteDB;
using MeterKnife.Datas.Base;
using MeterKnife.Datas.Entities;
using MeterKnife.Interfaces.Datas;

namespace MeterKnife.Datas.Dpi
{
    public class ExhibitListRepository : PagingAndSortingRepositoryBase<ExhibitItem, int>
    {
        public ExhibitListRepository(string repositoryPath) 
            : base(repositoryPath)
        {
        }
    }
}