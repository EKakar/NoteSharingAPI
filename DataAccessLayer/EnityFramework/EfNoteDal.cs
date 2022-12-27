using DataAccessLayer.Abstract;
using DataAccessLayer.Repository;
using NoteSharingAPI.Models;

namespace DataAccessLayer.EnityFramework
{
    public class EfNoteDal : GenericRepository<Note>, INoteDal
    {
    }
}
