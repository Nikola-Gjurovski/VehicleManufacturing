using Domain;
using Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleReposiotry.Interface
{
    public interface IETLRepository
    {
        IEnumerable<Nadvor> GetAll();
        Nadvor Get(string? id);
        void Insert(Nadvor entity);
        void Update(Nadvor entity);
        void Delete(Nadvor entity);
    }
}
