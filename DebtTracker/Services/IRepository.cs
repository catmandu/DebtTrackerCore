using DebtTracker.Entities;
using System;
using System.Collections.Generic;

namespace DebtTracker.Services
{
    public interface IRepository
    {
        #region Persons
        bool PersonExists(Guid id);
        IEnumerable<Person> GetPersons();
        Person GetPerson(Guid id);
        void AddPerson(Person person);
        #endregion

        #region Debts
        IEnumerable<Debt> GetDebts(Guid personId);
        Debt GetDebtForPerson(Guid personId, Guid id);
        void AddDebtForPerson(Guid personId, Debt debt);
        #endregion

        bool Save();
    }
}
