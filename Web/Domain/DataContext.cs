using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using ClearCode.Web.Domain.Entities;

namespace ClearCode.Web.Domain
{
    [InstancePerDependency]
    public class DataContext : IDataContext
    {
        private readonly Dictionary<Type, List<object>> _data = new Dictionary<Type, List<object>>()
        {
            {
                typeof (PartyPreference),
                new List<object>()
                {
                    new PartyPreference("Palmer", 1, "Xenaphon"),
                    new PartyPreference("Palmer", 2, "Rudd"),
                    new PartyPreference("Palmer", 3, "Turbull"),
                    new PartyPreference("Palmer", 4, "Abbott"),
                    new PartyPreference("Xenaphon", 1, "Rudd"),
                    new PartyPreference("Xenaphon", 2, "Turbull"),
                    new PartyPreference("Xenaphon", 3, "Abbott"),
                    new PartyPreference("Xenaphon", 4, "Palmer"),
                    new PartyPreference("Stott Despoja", 1, "Xenaphon"),
                    new PartyPreference("Stott Despoja", 2, "Rudd"),
                    new PartyPreference("Stott Despoja", 3, "Abbott"),
                    new PartyPreference("Stott Despoja", 4, "Palmer"),
                    new PartyPreference("Rudd", 1, "Xenaphon"),
                    new PartyPreference("Rudd", 2, "Palmer"),
                    new PartyPreference("Rudd", 3, "Turbull"),
                    new PartyPreference("Rudd", 4, "Abbott"),
                    new PartyPreference("Abbott", 1, "Xenaphon"),
                    new PartyPreference("Abbott", 2, "Rudd"),
                    new PartyPreference("Abbott", 3, "Turbull"),
                    new PartyPreference("Abbott", 4, "Palmer")
                }
            },
            {
                typeof (Candidate),
                new List<object>()
                {
                    new Candidate("Palmer"),
                    new Candidate("Xenaphon"),
                    new Candidate("Stott Despoja"),
                    new Candidate("Rudd"),
                    new Candidate("Abbott")
                }
            }
        };

        public IQueryable<T> Table<T>() => _data[typeof (T)].Cast<T>().AsQueryable();
    }
}