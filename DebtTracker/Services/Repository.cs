using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DebtTracker.Entities;

namespace DebtTracker.Services
{
    public class Repository : IRepository
    {
        private readonly Context _context;

        public Repository(Context context)
        {
            _context = context;
        }

        #region Persons
        public bool PersonExists(Guid id)
        {
            return _context.Persons
                .Any(p => p.Id == id);
        }

        public IEnumerable<Person> GetPersons()
        {
            return _context.Persons
                .OrderBy(p => p.FirstName)
                .ThenBy(p => p.LastName);
        }

        public Person GetPerson(Guid id)
        {
            return _context.Persons
                .FirstOrDefault(p => p.Id == id);
        }

        public void AddPerson(Person person)
        {
            person.Id = Guid.NewGuid();
            _context.Persons.Add(person);

            // the repository fills the id (instead of using identity columns)
            if (person.Debts.Any())
            {
                foreach (var debt in person.Debts)
                {
                    debt.Id = Guid.NewGuid();
                }
            }
        }
        #endregion


        #region Debts
        public IEnumerable<Debt> GetDebts(Guid personId)
        {
            return _context.Debts
                .Where(d => d.PersonId == personId)
                .OrderBy(d => d.Concept);
        }

        public Debt GetDebtForPerson(Guid personId, Guid id)
        {
            return _context.Debts
                .FirstOrDefault(d => d.Id == id
                && d.PersonId == personId);
        } 

        public void AddDebtForPerson(Guid personId, Debt debt)
        {
            var person = GetPerson(personId);

            if(person != null)
            {
                if(debt.Id == Guid.Empty)
                {
                    debt.Id = Guid.NewGuid();
                }

                person.Debts.Add(debt);
            }
        }
        #endregion

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
