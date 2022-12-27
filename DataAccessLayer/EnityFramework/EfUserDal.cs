using DataAccessLayer.Abstract;
using DataAccessLayer.Repository;
using NoteSharingAPI.Models;

namespace DataAccessLayer.EnityFramework
{
    public class EfUserDal : GenericRepository<User>, IUserDal
    {
    }
}
